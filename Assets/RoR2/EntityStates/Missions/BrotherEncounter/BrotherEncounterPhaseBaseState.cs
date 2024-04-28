using System;
using RoR2;
using RoR2.CharacterSpeech;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Missions.BrotherEncounter
{
	// Token: 0x0200024F RID: 591
	public abstract class BrotherEncounterPhaseBaseState : BrotherEncounterBaseState
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000A79 RID: 2681
		protected abstract EntityState nextState { get; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000A7A RID: 2682
		protected abstract string phaseControllerChildString { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x000137EE File Offset: 0x000119EE
		protected virtual float healthBarShowDelay
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0002B654 File Offset: 0x00029854
		public override void OnEnter()
		{
			base.OnEnter();
			if (PhaseCounter.instance)
			{
				PhaseCounter.instance.GoToNextPhase();
			}
			if (this.childLocator)
			{
				this.phaseControllerObject = this.childLocator.FindChild(this.phaseControllerChildString).gameObject;
				if (this.phaseControllerObject)
				{
					this.phaseScriptedCombatEncounter = this.phaseControllerObject.GetComponent<ScriptedCombatEncounter>();
					this.phaseBossGroup = this.phaseControllerObject.GetComponent<BossGroup>();
					this.phaseControllerSubObjectContainer = this.phaseControllerObject.transform.Find("PhaseObjects").gameObject;
					this.phaseControllerSubObjectContainer.SetActive(true);
				}
				GameObject gameObject = this.childLocator.FindChild("AllPhases").gameObject;
				if (gameObject)
				{
					gameObject.SetActive(true);
				}
			}
			this.healthBarShowTime = Run.FixedTimeStamp.now + this.healthBarShowDelay;
			if (DirectorCore.instance)
			{
				CombatDirector[] components = DirectorCore.instance.GetComponents<CombatDirector>();
				for (int i = 0; i < components.Length; i++)
				{
					components[i].enabled = false;
				}
			}
			if (NetworkServer.active && this.phaseScriptedCombatEncounter != null)
			{
				this.phaseScriptedCombatEncounter.combatSquad.onMemberAddedServer += this.OnMemberAddedServer;
			}
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002B79C File Offset: 0x0002999C
		public override void OnExit()
		{
			if (this.phaseScriptedCombatEncounter != null)
			{
				this.phaseScriptedCombatEncounter.combatSquad.onMemberAddedServer -= this.OnMemberAddedServer;
			}
			if (this.phaseControllerSubObjectContainer)
			{
				this.phaseControllerSubObjectContainer.SetActive(false);
			}
			if (this.phaseBossGroup)
			{
				this.phaseBossGroup.shouldDisplayHealthBarOnHud = false;
			}
			base.OnExit();
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002B808 File Offset: 0x00029A08
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.phaseBossGroup.shouldDisplayHealthBarOnHud = this.healthBarShowTime.hasPassed;
			if (!this.hasSpawned)
			{
				if (base.fixedAge > this.durationBeforeEnablingCombatEncounter)
				{
					this.BeginEncounter();
					return;
				}
			}
			else if (NetworkServer.active && !this.finishedServer && base.fixedAge > 2f + this.durationBeforeEnablingCombatEncounter && this.phaseScriptedCombatEncounter && this.phaseScriptedCombatEncounter.combatSquad.memberCount == 0)
			{
				this.finishedServer = true;
				this.outer.SetNextState(this.nextState);
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002B8A8 File Offset: 0x00029AA8
		protected void BeginEncounter()
		{
			this.hasSpawned = true;
			this.PreEncounterBegin();
			if (NetworkServer.active)
			{
				this.phaseScriptedCombatEncounter.BeginEncounter();
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void PreEncounterBegin()
		{
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002B8C9 File Offset: 0x00029AC9
		protected virtual void OnMemberAddedServer(CharacterMaster master)
		{
			if (this.speechControllerPrefab)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.speechControllerPrefab, master.transform).GetComponent<CharacterSpeechController>().characterMaster = master;
			}
		}

		// Token: 0x04000C2A RID: 3114
		[SerializeField]
		public float durationBeforeEnablingCombatEncounter;

		// Token: 0x04000C2B RID: 3115
		[SerializeField]
		public GameObject speechControllerPrefab;

		// Token: 0x04000C2C RID: 3116
		protected ScriptedCombatEncounter phaseScriptedCombatEncounter;

		// Token: 0x04000C2D RID: 3117
		protected GameObject phaseControllerObject;

		// Token: 0x04000C2E RID: 3118
		protected GameObject phaseControllerSubObjectContainer;

		// Token: 0x04000C2F RID: 3119
		protected BossGroup phaseBossGroup;

		// Token: 0x04000C30 RID: 3120
		private bool hasSpawned;

		// Token: 0x04000C31 RID: 3121
		private bool finishedServer;

		// Token: 0x04000C32 RID: 3122
		private const float minimumDurationPerPhase = 2f;

		// Token: 0x04000C33 RID: 3123
		private Run.FixedTimeStamp healthBarShowTime = Run.FixedTimeStamp.positiveInfinity;
	}
}
