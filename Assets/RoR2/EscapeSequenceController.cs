using System;
using System.Collections.Generic;
using EntityStates;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006C6 RID: 1734
	public class EscapeSequenceController : NetworkBehaviour
	{
		// Token: 0x06002217 RID: 8727 RVA: 0x00093A18 File Offset: 0x00091C18
		public void BeginEscapeSequence()
		{
			if (Util.HasEffectiveAuthority(base.gameObject))
			{
				this.mainStateMachine.SetNextState(new EscapeSequenceController.EscapeSequenceMainState());
			}
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x00093A37 File Offset: 0x00091C37
		public void CompleteEscapeSequence()
		{
			if (Util.HasEffectiveAuthority(base.gameObject))
			{
				this.mainStateMachine.SetNextState(new EscapeSequenceController.EscapeSequenceSuccessState());
			}
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x00093A58 File Offset: 0x00091C58
		private void UpdateScheduledEvents(float secondsRemaining)
		{
			for (int i = 0; i < this.scheduledEvents.Length; i++)
			{
				ref EscapeSequenceController.ScheduledEvent ptr = ref this.scheduledEvents[i];
				bool flag = ptr.minSecondsRemaining <= secondsRemaining && secondsRemaining <= ptr.maxSecondsRemaining;
				if (flag != ptr.inEvent)
				{
					if (flag)
					{
						UnityEvent onEnter = ptr.onEnter;
						if (onEnter != null)
						{
							onEnter.Invoke();
						}
					}
					else
					{
						UnityEvent onExit = ptr.onExit;
						if (onExit != null)
						{
							onExit.Invoke();
						}
					}
					ptr.inEvent = flag;
				}
			}
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x00093AD8 File Offset: 0x00091CD8
		private void SetHudCountdownEnabled(HUD hud, bool shouldEnableCountdownPanel)
		{
			shouldEnableCountdownPanel &= base.enabled;
			GameObject gameObject;
			this.hudPanels.TryGetValue(hud, out gameObject);
			if (gameObject != shouldEnableCountdownPanel)
			{
				if (shouldEnableCountdownPanel)
				{
					RectTransform rectTransform = hud.GetComponent<ChildLocator>().FindChild("TopCenterCluster") as RectTransform;
					if (rectTransform)
					{
						GameObject value = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/HudModules/HudCountdownPanel"), rectTransform);
						this.hudPanels[hud] = value;
						return;
					}
				}
				else
				{
					UnityEngine.Object.Destroy(gameObject);
					this.hudPanels.Remove(hud);
				}
			}
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x00093B5C File Offset: 0x00091D5C
		private void SetCountdownTime(double secondsRemaining)
		{
			foreach (KeyValuePair<HUD, GameObject> keyValuePair in this.hudPanels)
			{
				keyValuePair.Value.GetComponent<TimerText>().seconds = secondsRemaining;
			}
			AkSoundEngine.SetRTPCValue("EscapeTimer", Util.Remap((float)secondsRemaining, 0f, this.countdownDuration, 0f, 100f));
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x00093BE4 File Offset: 0x00091DE4
		private void OnEnable()
		{
			this.hudPanels = new Dictionary<HUD, GameObject>();
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x00093BF4 File Offset: 0x00091DF4
		private void OnDisable()
		{
			foreach (HUD hud in HUD.readOnlyInstanceList)
			{
				this.SetHudCountdownEnabled(hud, false);
			}
			this.hudPanels = null;
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x00093C48 File Offset: 0x00091E48
		public void DestroyAllBodies()
		{
			List<CharacterBody> list = new List<CharacterBody>(CharacterBody.readOnlyInstancesList);
			for (int i = 0; i < list.Count; i++)
			{
				CharacterBody characterBody = list[i];
				if (characterBody)
				{
					UnityEngine.Object.Destroy(characterBody.gameObject);
				}
			}
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x00093C8C File Offset: 0x00091E8C
		public void KillAllCharacters()
		{
			List<CharacterMaster> list = new List<CharacterMaster>(CharacterMaster.readOnlyInstancesList);
			for (int i = 0; i < list.Count; i++)
			{
				CharacterMaster characterMaster = list[i];
				if (characterMaster)
				{
					characterMaster.TrueKill(null, null, DamageType.Silent | DamageType.VoidDeath);
				}
			}
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x00093CD4 File Offset: 0x00091ED4
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002742 RID: 10050
		public EntityStateMachine mainStateMachine;

		// Token: 0x04002743 RID: 10051
		[Tooltip("How long the player has to escape, in seconds.")]
		public float countdownDuration;

		// Token: 0x04002744 RID: 10052
		public UnityEvent onEnterMainEscapeSequence;

		// Token: 0x04002745 RID: 10053
		public UnityEvent onCompleteEscapeSequenceServer;

		// Token: 0x04002746 RID: 10054
		public UnityEvent onFailEscapeSequenceServer;

		// Token: 0x04002747 RID: 10055
		public EscapeSequenceController.ScheduledEvent[] scheduledEvents;

		// Token: 0x04002748 RID: 10056
		private Dictionary<HUD, GameObject> hudPanels;

		// Token: 0x020006C7 RID: 1735
		[Serializable]
		public struct ScheduledEvent
		{
			// Token: 0x04002749 RID: 10057
			public float minSecondsRemaining;

			// Token: 0x0400274A RID: 10058
			public float maxSecondsRemaining;

			// Token: 0x0400274B RID: 10059
			public UnityEvent onEnter;

			// Token: 0x0400274C RID: 10060
			public UnityEvent onExit;

			// Token: 0x0400274D RID: 10061
			[NonSerialized]
			public bool inEvent;
		}

		// Token: 0x020006C8 RID: 1736
		public class EscapeSequenceBaseState : BaseState
		{
			// Token: 0x170002BB RID: 699
			// (get) Token: 0x06002225 RID: 8741 RVA: 0x00093CE2 File Offset: 0x00091EE2
			// (set) Token: 0x06002226 RID: 8742 RVA: 0x00093CEA File Offset: 0x00091EEA
			private protected EscapeSequenceController escapeSequenceController { protected get; private set; }

			// Token: 0x06002227 RID: 8743 RVA: 0x00093CF3 File Offset: 0x00091EF3
			public override void OnEnter()
			{
				base.OnEnter();
				this.escapeSequenceController = base.GetComponent<EscapeSequenceController>();
			}
		}

		// Token: 0x020006C9 RID: 1737
		public class EscapeSequenceMainState : EscapeSequenceController.EscapeSequenceBaseState
		{
			// Token: 0x06002229 RID: 8745 RVA: 0x00093D08 File Offset: 0x00091F08
			public override void OnEnter()
			{
				base.OnEnter();
				if (base.isAuthority)
				{
					this.startTime = Run.FixedTimeStamp.now;
					this.endTime = this.startTime + base.escapeSequenceController.countdownDuration;
				}
				UnityEvent onEnterMainEscapeSequence = base.escapeSequenceController.onEnterMainEscapeSequence;
				if (onEnterMainEscapeSequence == null)
				{
					return;
				}
				onEnterMainEscapeSequence.Invoke();
			}

			// Token: 0x0600222A RID: 8746 RVA: 0x00093D60 File Offset: 0x00091F60
			public override void OnExit()
			{
				foreach (HUD hud in HUD.readOnlyInstanceList)
				{
					base.escapeSequenceController.SetHudCountdownEnabled(hud, false);
				}
				base.OnExit();
			}

			// Token: 0x0600222B RID: 8747 RVA: 0x00093DB8 File Offset: 0x00091FB8
			public override void Update()
			{
				base.Update();
				foreach (HUD hud in HUD.readOnlyInstanceList)
				{
					base.escapeSequenceController.SetHudCountdownEnabled(hud, hud.targetBodyObject);
				}
				base.escapeSequenceController.SetCountdownTime((double)this.endTime.timeUntilClamped);
			}

			// Token: 0x0600222C RID: 8748 RVA: 0x00093E34 File Offset: 0x00092034
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				base.escapeSequenceController.UpdateScheduledEvents(this.endTime.timeUntil);
				if (base.isAuthority && this.endTime.hasPassed && !SceneExitController.isRunning)
				{
					this.outer.SetNextState(new EscapeSequenceController.EscapeSequenceFailureState());
				}
			}

			// Token: 0x0600222D RID: 8749 RVA: 0x00093E89 File Offset: 0x00092089
			public override void OnSerialize(NetworkWriter writer)
			{
				base.OnSerialize(writer);
				writer.Write(this.startTime);
				writer.Write(this.endTime);
			}

			// Token: 0x0600222E RID: 8750 RVA: 0x00093EAA File Offset: 0x000920AA
			public override void OnDeserialize(NetworkReader reader)
			{
				base.OnDeserialize(reader);
				this.startTime = reader.ReadFixedTimeStamp();
				this.endTime = reader.ReadFixedTimeStamp();
			}

			// Token: 0x0400274F RID: 10063
			private Run.FixedTimeStamp startTime;

			// Token: 0x04002750 RID: 10064
			private Run.FixedTimeStamp endTime;
		}

		// Token: 0x020006CA RID: 1738
		public class EscapeSequenceFailureState : EscapeSequenceController.EscapeSequenceBaseState
		{
			// Token: 0x06002230 RID: 8752 RVA: 0x00093ED3 File Offset: 0x000920D3
			public override void OnEnter()
			{
				base.OnEnter();
				if (NetworkServer.active)
				{
					UnityEvent onFailEscapeSequenceServer = base.escapeSequenceController.onFailEscapeSequenceServer;
					if (onFailEscapeSequenceServer == null)
					{
						return;
					}
					onFailEscapeSequenceServer.Invoke();
				}
			}
		}

		// Token: 0x020006CB RID: 1739
		public class EscapeSequenceSuccessState : EscapeSequenceController.EscapeSequenceBaseState
		{
			// Token: 0x06002232 RID: 8754 RVA: 0x00093EF7 File Offset: 0x000920F7
			public override void OnEnter()
			{
				base.OnEnter();
				if (NetworkServer.active)
				{
					UnityEvent onCompleteEscapeSequenceServer = base.escapeSequenceController.onCompleteEscapeSequenceServer;
					if (onCompleteEscapeSequenceServer == null)
					{
						return;
					}
					onCompleteEscapeSequenceServer.Invoke();
				}
			}
		}
	}
}
