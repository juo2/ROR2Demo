using System;
using System.Reflection;
using RoR2.ConVar;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005E3 RID: 1507
	[RequireComponent(typeof(AkGameObj))]
	public class AudioManager : MonoBehaviour
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x00075239 File Offset: 0x00073439
		// (set) Token: 0x06001B5D RID: 7005 RVA: 0x00075240 File Offset: 0x00073440
		public static AudioManager instance { get; private set; }

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001B5E RID: 7006 RVA: 0x00075248 File Offset: 0x00073448
		// (remove) Token: 0x06001B5F RID: 7007 RVA: 0x0007527C File Offset: 0x0007347C
		public static event Action<AudioManager> onAwakeGlobal;

		// Token: 0x06001B60 RID: 7008 RVA: 0x000752AF File Offset: 0x000734AF
		private void Awake()
		{
			AudioManager.instance = this;
			this.akGameObj = base.GetComponent<AkGameObj>();
			Action<AudioManager> action = AudioManager.onAwakeGlobal;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x000752D4 File Offset: 0x000734D4
		static AudioManager()
		{
			PauseManager.onPauseStartGlobal = (Action)Delegate.Combine(PauseManager.onPauseStartGlobal, new Action(delegate()
			{
				AkSoundEngine.PostEvent("Pause_All", null);
			}));
			PauseManager.onPauseEndGlobal = (Action)Delegate.Combine(PauseManager.onPauseEndGlobal, new Action(delegate()
			{
				AkSoundEngine.PostEvent("Unpause_All", null);
			}));
		}

		// Token: 0x0400215F RID: 8543
		private AkGameObj akGameObj;

		// Token: 0x04002162 RID: 8546
		private static AudioManager.VolumeConVar cvVolumeMaster = new AudioManager.VolumeConVar("volume_master", ConVarFlags.Archive | ConVarFlags.Engine, "100", "The master volume of the game audio, from 0 to 100.", "Volume_Master");

		// Token: 0x04002163 RID: 8547
		private static AudioManager.VolumeConVar cvVolumeSfx = new AudioManager.VolumeConVar("volume_sfx", ConVarFlags.Archive | ConVarFlags.Engine, "100", "The volume of sound effects, from 0 to 100.", "Volume_SFX");

		// Token: 0x04002164 RID: 8548
		private static AudioManager.VolumeConVar cvVolumeMsx = new AudioManager.VolumeConVar("volume_music", ConVarFlags.Archive | ConVarFlags.Engine, "100", "The music volume, from 0 to 100.", "Volume_MSX");

		// Token: 0x04002165 RID: 8549
		private static readonly FieldInfo akInitializerMsInstanceField = typeof(AkInitializer).GetField("ms_Instance", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x020005E4 RID: 1508
		private class VolumeConVar : BaseConVar
		{
			// Token: 0x06001B63 RID: 7011 RVA: 0x000753A4 File Offset: 0x000735A4
			public VolumeConVar(string name, ConVarFlags flags, string defaultValue, string helpText, string rtpcName) : base(name, flags, defaultValue, helpText)
			{
				this.rtpcName = rtpcName;
			}

			// Token: 0x06001B64 RID: 7012 RVA: 0x000753BC File Offset: 0x000735BC
			public override void SetString(string newValue)
			{
				if (AkSoundEngine.IsInitialized())
				{
					this.fallbackString = newValue;
					float value;
					if (TextSerialization.TryParseInvariant(newValue, out value))
					{
						AkSoundEngine.SetRTPCValue(this.rtpcName, Mathf.Clamp(value, 0f, 100f));
					}
				}
			}

			// Token: 0x06001B65 RID: 7013 RVA: 0x00075400 File Offset: 0x00073600
			public override string GetString()
			{
				int num = 1;
				float value;
				if (AkSoundEngine.GetRTPCValue(this.rtpcName, null, 0U, out value, ref num) == AKRESULT.AK_Success)
				{
					return TextSerialization.ToStringInvariant(value);
				}
				return this.fallbackString;
			}

			// Token: 0x04002166 RID: 8550
			private readonly string rtpcName;

			// Token: 0x04002167 RID: 8551
			private string fallbackString;
		}

		// Token: 0x020005E5 RID: 1509
		private class AudioFocusedOnlyConVar : BaseConVar
		{
			// Token: 0x06001B66 RID: 7014 RVA: 0x00075430 File Offset: 0x00073630
			public AudioFocusedOnlyConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
				this.isFocused = Application.isFocused;
				RoR2Application.onUpdate += this.SearchForAkInitializer;
			}

			// Token: 0x06001B67 RID: 7015 RVA: 0x0007545C File Offset: 0x0007365C
			public override void SetString(string newValue)
			{
				int num;
				if (TextSerialization.TryParseInvariant(newValue, out num))
				{
					this.onlyPlayWhenFocused = (num != 0);
					this.Refresh();
				}
			}

			// Token: 0x06001B68 RID: 7016 RVA: 0x00075483 File Offset: 0x00073683
			public override string GetString()
			{
				if (!this.onlyPlayWhenFocused)
				{
					return "0";
				}
				return "1";
			}

			// Token: 0x06001B69 RID: 7017 RVA: 0x00075498 File Offset: 0x00073698
			private void OnApplicationFocus(bool focus)
			{
				this.isFocused = focus;
				this.Refresh();
			}

			// Token: 0x06001B6A RID: 7018 RVA: 0x000754A8 File Offset: 0x000736A8
			private void Refresh()
			{
				bool flag = false;
				bool flag2 = !this.isFocused && this.onlyPlayWhenFocused;
				bool flag3 = flag;
				AkSoundEngineController akSoundEngineController = AkSoundEngineController.Instance;
				if (akSoundEngineController != null)
				{
					AudioManager.AudioFocusedOnlyConVar.AkSoundEngineController_ActivateAudio(akSoundEngineController, !flag2, !flag3);
				}
			}

			// Token: 0x06001B6B RID: 7019 RVA: 0x000754E0 File Offset: 0x000736E0
			private static void AkSoundEngineController_ActivateAudio(AkSoundEngineController akSoundEngineController, bool activate, bool renderAnyway)
			{
				AudioManager.AudioFocusedOnlyConVar.akSoundEngineController_ActivateAudio_methodInfo.Invoke(akSoundEngineController, new object[]
				{
					activate,
					renderAnyway
				});
			}

			// Token: 0x06001B6C RID: 7020 RVA: 0x00075508 File Offset: 0x00073708
			private void SearchForAkInitializer()
			{
				AkInitializer akInitializer = (AkInitializer)AudioManager.akInitializerMsInstanceField.GetValue(null);
				if (!akInitializer)
				{
					return;
				}
				RoR2Application.onUpdate -= this.SearchForAkInitializer;
				AudioManager.AudioFocusedOnlyConVar.ApplicationFocusListener applicationFocusListener = akInitializer.gameObject.AddComponent<AudioManager.AudioFocusedOnlyConVar.ApplicationFocusListener>();
				applicationFocusListener.onApplicationFocus = (Action<bool>)Delegate.Combine(applicationFocusListener.onApplicationFocus, new Action<bool>(this.OnApplicationFocus));
				this.Refresh();
			}

			// Token: 0x04002168 RID: 8552
			private static AudioManager.AudioFocusedOnlyConVar instance = new AudioManager.AudioFocusedOnlyConVar("audio_focused_only", ConVarFlags.Archive | ConVarFlags.Engine, null, "Whether or not audio should mute when focus is lost.");

			// Token: 0x04002169 RID: 8553
			private bool onlyPlayWhenFocused;

			// Token: 0x0400216A RID: 8554
			private bool isFocused;

			// Token: 0x0400216B RID: 8555
			private static MethodInfo akSoundEngineController_ActivateAudio_methodInfo = typeof(AkSoundEngineController).GetMethod("ActivateAudio", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			// Token: 0x020005E6 RID: 1510
			private class ApplicationFocusListener : MonoBehaviour
			{
				// Token: 0x06001B6E RID: 7022 RVA: 0x000755A6 File Offset: 0x000737A6
				private void OnApplicationFocus(bool focus)
				{
					Action<bool> action = this.onApplicationFocus;
					if (action == null)
					{
						return;
					}
					action(focus);
				}

				// Token: 0x0400216C RID: 8556
				public Action<bool> onApplicationFocus;
			}
		}

		// Token: 0x020005E7 RID: 1511
		private class WwiseLogEnabledConVar : BaseConVar
		{
			// Token: 0x06001B70 RID: 7024 RVA: 0x00009F73 File Offset: 0x00008173
			private WwiseLogEnabledConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06001B71 RID: 7025 RVA: 0x000755BC File Offset: 0x000737BC
			public override void SetString(string newValue)
			{
				int num;
				if (TextSerialization.TryParseInvariant(newValue, out num))
				{
					AkInitializer akInitializer = AudioManager.akInitializerMsInstanceField.GetValue(null) as AkInitializer;
					if (akInitializer)
					{
						AkWwiseInitializationSettings initializationSettings = akInitializer.InitializationSettings;
						AkCallbackManager.InitializationSettings initializationSettings2 = (initializationSettings != null) ? initializationSettings.CallbackManagerInitializationSettings : null;
						if (initializationSettings2 != null)
						{
							initializationSettings2.IsLoggingEnabled = (num != 0);
							return;
						}
						Debug.Log("Cannot set value. callbackManagerInitializationSettings is null.");
					}
				}
			}

			// Token: 0x06001B72 RID: 7026 RVA: 0x00075618 File Offset: 0x00073818
			public override string GetString()
			{
				AkInitializer akInitializer = AudioManager.akInitializerMsInstanceField.GetValue(null) as AkInitializer;
				if (akInitializer)
				{
					AkWwiseInitializationSettings initializationSettings = akInitializer.InitializationSettings;
					if (((initializationSettings != null) ? initializationSettings.CallbackManagerInitializationSettings : null) != null)
					{
						if (!akInitializer.InitializationSettings.CallbackManagerInitializationSettings.IsLoggingEnabled)
						{
							return "0";
						}
						return "1";
					}
					else
					{
						Debug.Log("Cannot retrieve value. callbackManagerInitializationSettings is null.");
					}
				}
				return "1";
			}

			// Token: 0x0400216D RID: 8557
			private static AudioManager.WwiseLogEnabledConVar instance = new AudioManager.WwiseLogEnabledConVar("wwise_log_enabled", ConVarFlags.Archive | ConVarFlags.Engine, null, "Wwise logging. 0 = disabled 1 = enabled");
		}
	}
}
