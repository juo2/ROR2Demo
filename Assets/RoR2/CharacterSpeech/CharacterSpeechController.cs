using System;
using System.Collections.Generic;
using RoR2.ConVar;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.CharacterSpeech
{
	// Token: 0x02000C2A RID: 3114
	public class CharacterSpeechController : MonoBehaviour
	{
		// Token: 0x0600464E RID: 17998 RVA: 0x00122F13 File Offset: 0x00121113
		private void Awake()
		{
			if (!NetworkServer.active)
			{
				base.enabled = false;
				return;
			}
			this.speechRequestQueue = new List<CharacterSpeechController.SpeechRequest>();
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x00122F2F File Offset: 0x0012112F
		private void Start()
		{
			if (this.characterMaster == null)
			{
				this.characterMaster = this.initialCharacterMaster;
			}
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x00122F45 File Offset: 0x00121145
		private void FixedUpdate()
		{
			this.Process(Time.fixedDeltaTime);
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x00122F52 File Offset: 0x00121152
		private void OnDestroy()
		{
			if (NetworkServer.active)
			{
				this.characterMaster = null;
			}
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x00122F64 File Offset: 0x00121164
		public void SpeakNow(in CharacterSpeechController.SpeechInfo speechInfo)
		{
			if (!NetworkServer.active)
			{
				return;
			}
			if (CharacterSpeechController.cvEnableLogging.value)
			{
				this.Log(string.Format("Playing speech: {0}", speechInfo));
			}
			this.nextSpeakTime = this.localTime + speechInfo.duration;
			GameObject sender = null;
			SfxLocator sfxLocator = null;
			if (this.characterMaster)
			{
				CharacterBody body = this.characterMaster.GetBody();
				if (body)
				{
					sfxLocator = body.GetComponent<SfxLocator>();
				}
			}
			Chat.SendBroadcastChat(new Chat.NpcChatMessage
			{
				baseToken = speechInfo.token,
				formatStringToken = this.chatFormatToken,
				sender = sender,
				sound = ((sfxLocator != null) ? sfxLocator.barkSound : null)
			});
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x0012301C File Offset: 0x0012121C
		public void EnqueueSpeech(in CharacterSpeechController.SpeechInfo speechInfo)
		{
			if (!NetworkServer.active)
			{
				return;
			}
			CharacterSpeechController.SpeechRequest speechRequest = new CharacterSpeechController.SpeechRequest
			{
				speechInfo = speechInfo,
				submitTime = this.localTime
			};
			this.speechRequestQueue.Add(speechRequest);
			if (CharacterSpeechController.cvEnableLogging.value)
			{
				this.Log(string.Format("Enqueued speech: {0}", speechRequest));
			}
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x00123084 File Offset: 0x00121284
		private void Process(float deltaTime)
		{
			this.localTime += deltaTime;
			if (this.nextSpeakTime <= this.localTime)
			{
				int bestNextRequestIndex = this.GetBestNextRequestIndex();
				if (bestNextRequestIndex != -1)
				{
					CharacterSpeechController.SpeechRequest speechRequest = this.speechRequestQueue[bestNextRequestIndex];
					if (CharacterSpeechController.cvEnableLogging.value)
					{
						this.Log(string.Format("Found best request: bestNextRequestIndex={0} bestNextRequest={1}", bestNextRequestIndex, speechRequest));
						for (int i = 0; i < bestNextRequestIndex; i++)
						{
							this.Log(string.Format("Dropping request: i={0} request={1}", i, this.speechRequestQueue[i]));
						}
					}
					this.speechRequestQueue.RemoveRange(0, bestNextRequestIndex + 1);
					this.SpeakNow(speechRequest.speechInfo);
				}
			}
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x00123144 File Offset: 0x00121344
		private int GetBestNextRequestIndex()
		{
			if (this.speechRequestQueue.Count == 0)
			{
				return -1;
			}
			for (int i = 0; i < this.speechRequestQueue.Count; i++)
			{
				CharacterSpeechController.SpeechRequest speechRequest = this.speechRequestQueue[i];
				CharacterSpeechController.SpeechInfo speechInfo = speechRequest.speechInfo;
				if (speechInfo.mustPlay)
				{
					return i;
				}
				float num = speechRequest.submitTime + speechInfo.maxWait;
				if (this.localTime <= num)
				{
					float num2 = this.localTime + speechInfo.duration;
					bool flag = false;
					for (int j = i + 1; j < this.speechRequestQueue.Count; j++)
					{
						CharacterSpeechController.SpeechRequest speechRequest2 = this.speechRequestQueue[j];
						CharacterSpeechController.SpeechInfo speechInfo2 = speechRequest2.speechInfo;
						if (speechInfo.priority < speechInfo2.priority || speechInfo2.mustPlay)
						{
							if (speechRequest2.submitTime + speechInfo2.maxWait < num2)
							{
								flag = true;
								break;
							}
							num2 += speechInfo2.duration;
						}
					}
					if (!flag)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06004656 RID: 18006 RVA: 0x00123243 File Offset: 0x00121443
		// (set) Token: 0x06004657 RID: 18007 RVA: 0x0012324B File Offset: 0x0012144B
		public CharacterMaster characterMaster
		{
			get
			{
				return this._characterMaster;
			}
			set
			{
				if (this._characterMaster == value)
				{
					return;
				}
				if (this._characterMaster != null)
				{
					this.OnCharacterMasterLost(this._characterMaster);
				}
				this._characterMaster = value;
				if (this._characterMaster != null)
				{
					this.OnCharacterMasterDiscovered(this._characterMaster);
				}
			}
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x00123286 File Offset: 0x00121486
		private void OnCharacterMasterDiscovered(CharacterMaster characterMaster)
		{
			characterMaster.onBodyStart += this.OnCharacterMasterBodyStart;
			Action<CharacterMaster> action = this.onCharacterMasterDiscovered;
			if (action != null)
			{
				action(characterMaster);
			}
			this.currentCharacterBody = characterMaster.GetBody();
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x001232B8 File Offset: 0x001214B8
		private void OnCharacterMasterLost(CharacterMaster characterMaster)
		{
			this.currentCharacterBody = null;
			Action<CharacterMaster> action = this.onCharacterMasterLost;
			if (action != null)
			{
				action(characterMaster);
			}
			characterMaster.onBodyDestroyed -= this.OnCharacterMasterBodyDestroyed;
		}

		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x0600465A RID: 18010 RVA: 0x001232E8 File Offset: 0x001214E8
		// (remove) Token: 0x0600465B RID: 18011 RVA: 0x00123320 File Offset: 0x00121520
		public event Action<CharacterMaster> onCharacterMasterDiscovered;

		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x0600465C RID: 18012 RVA: 0x00123358 File Offset: 0x00121558
		// (remove) Token: 0x0600465D RID: 18013 RVA: 0x00123390 File Offset: 0x00121590
		public event Action<CharacterMaster> onCharacterMasterLost;

		// Token: 0x0600465E RID: 18014 RVA: 0x001233C5 File Offset: 0x001215C5
		private void OnCharacterMasterBodyStart(CharacterBody characterBody)
		{
			this.currentCharacterBody = characterBody;
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x001233CE File Offset: 0x001215CE
		private void OnCharacterMasterBodyDestroyed(CharacterBody characterBody)
		{
			if (characterBody == this.currentCharacterBody)
			{
				this.currentCharacterBody = null;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06004660 RID: 18016 RVA: 0x001233E0 File Offset: 0x001215E0
		// (set) Token: 0x06004661 RID: 18017 RVA: 0x001233E8 File Offset: 0x001215E8
		public CharacterBody currentCharacterBody
		{
			get
			{
				return this._currentCharacterBody;
			}
			private set
			{
				if (this._currentCharacterBody == value)
				{
					return;
				}
				if (this._currentCharacterBody != null)
				{
					this.OnCharacterBodyLost(this._currentCharacterBody);
				}
				this._currentCharacterBody = value;
				if (this._currentCharacterBody != null)
				{
					this.OnCharacterBodyDiscovered(this._currentCharacterBody);
				}
			}
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x00123423 File Offset: 0x00121623
		private void OnCharacterBodyDiscovered(CharacterBody characterBody)
		{
			Action<CharacterBody> action = this.onCharacterBodyDiscovered;
			if (action == null)
			{
				return;
			}
			action(characterBody);
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x00123436 File Offset: 0x00121636
		private void OnCharacterBodyLost(CharacterBody characterBody)
		{
			Action<CharacterBody> action = this.onCharacterBodyLost;
			if (action == null)
			{
				return;
			}
			action(characterBody);
		}

		// Token: 0x140000E3 RID: 227
		// (add) Token: 0x06004664 RID: 18020 RVA: 0x0012344C File Offset: 0x0012164C
		// (remove) Token: 0x06004665 RID: 18021 RVA: 0x00123484 File Offset: 0x00121684
		public event Action<CharacterBody> onCharacterBodyDiscovered;

		// Token: 0x140000E4 RID: 228
		// (add) Token: 0x06004666 RID: 18022 RVA: 0x001234BC File Offset: 0x001216BC
		// (remove) Token: 0x06004667 RID: 18023 RVA: 0x001234F4 File Offset: 0x001216F4
		public event Action<CharacterBody> onCharacterBodyLost;

		// Token: 0x06004668 RID: 18024 RVA: 0x00123529 File Offset: 0x00121729
		private void Log(string str)
		{
			Debug.Log(string.Format("CharacterSpeechController (gameObject={0} instanceId={1}): {2}", base.gameObject, base.GetInstanceID(), str), this);
		}

		// Token: 0x04004434 RID: 17460
		public CharacterMaster initialCharacterMaster;

		// Token: 0x04004435 RID: 17461
		public string chatFormatToken;

		// Token: 0x04004436 RID: 17462
		private float localTime;

		// Token: 0x04004437 RID: 17463
		private float nextSpeakTime;

		// Token: 0x04004438 RID: 17464
		private List<CharacterSpeechController.SpeechRequest> speechRequestQueue;

		// Token: 0x04004439 RID: 17465
		private CharacterMaster _characterMaster;

		// Token: 0x0400443C RID: 17468
		private CharacterBody _currentCharacterBody;

		// Token: 0x0400443F RID: 17471
		private static readonly BoolConVar cvEnableLogging = new BoolConVar("character_speech_debug", ConVarFlags.None, "0", "Enables/disables debug logging for CharacterSpeechController");

		// Token: 0x02000C2B RID: 3115
		[Serializable]
		public struct SpeechInfo
		{
			// Token: 0x0600466B RID: 18027 RVA: 0x0012356C File Offset: 0x0012176C
			public override string ToString()
			{
				return string.Format("{{ token=\"{0}\" duration={1} maxWait={2} priority={3} mustPlay={4} }}", new object[]
				{
					this.token,
					this.duration,
					this.maxWait,
					this.priority,
					this.mustPlay
				});
			}

			// Token: 0x04004440 RID: 17472
			public string token;

			// Token: 0x04004441 RID: 17473
			public float duration;

			// Token: 0x04004442 RID: 17474
			public float maxWait;

			// Token: 0x04004443 RID: 17475
			public float priority;

			// Token: 0x04004444 RID: 17476
			public bool mustPlay;
		}

		// Token: 0x02000C2C RID: 3116
		[Serializable]
		private struct SpeechRequest
		{
			// Token: 0x0600466C RID: 18028 RVA: 0x001235CA File Offset: 0x001217CA
			public override string ToString()
			{
				return string.Format("{{ speechInfo={0} submitTime={1} }}", this.speechInfo, this.submitTime);
			}

			// Token: 0x04004445 RID: 17477
			public CharacterSpeechController.SpeechInfo speechInfo;

			// Token: 0x04004446 RID: 17478
			public float submitTime;
		}
	}
}
