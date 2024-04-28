using System;
using EntityStates.Interactables.GoldBeacon;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Missions.Goldshores
{
	// Token: 0x0200024D RID: 589
	public class GoldshoresBossfight : EntityState
	{
		// Token: 0x06000A67 RID: 2663 RVA: 0x0002B209 File Offset: 0x00029409
		public override void OnEnter()
		{
			base.OnEnter();
			this.missionController = base.GetComponent<GoldshoresMissionController>();
			this.bossInvulnerabilityStartTime = Run.FixedTimeStamp.negativeInfinity;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0002B228 File Offset: 0x00029428
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				this.ServerFixedUpdate();
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0002B23D File Offset: 0x0002943D
		private bool bossShouldBeInvulnerable
		{
			get
			{
				return this.missionController.beaconsActive < this.missionController.beaconsToSpawnOnMap;
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002B258 File Offset: 0x00029458
		private void SetBossImmunity(bool newBossImmunity)
		{
			if (!this.scriptedCombatEncounter)
			{
				return;
			}
			if (newBossImmunity == this.bossImmunity)
			{
				return;
			}
			this.bossImmunity = newBossImmunity;
			foreach (CharacterMaster characterMaster in this.scriptedCombatEncounter.combatSquad.readOnlyMembersList)
			{
				CharacterBody body = characterMaster.GetBody();
				if (body)
				{
					if (this.bossImmunity)
					{
						body.AddBuff(RoR2Content.Buffs.Immune);
					}
					else
					{
						EffectManager.SpawnEffect(GoldshoresBossfight.shieldRemovalEffectPrefab, new EffectData
						{
							origin = body.coreTransform.position
						}, true);
						body.RemoveBuff(RoR2Content.Buffs.Immune);
					}
				}
			}
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002B318 File Offset: 0x00029518
		private void ExtinguishBeacons()
		{
			foreach (GameObject gameObject in this.missionController.beaconInstanceList)
			{
				gameObject.GetComponent<EntityStateMachine>().SetNextState(new NotReady());
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0002B378 File Offset: 0x00029578
		private void ServerFixedUpdate()
		{
			if (base.fixedAge >= GoldshoresBossfight.transitionDuration)
			{
				this.missionController.ExitTransitionIntoBossfight();
				if (!this.hasSpawnedBoss)
				{
					this.SpawnBoss();
				}
				else if (this.scriptedCombatEncounter.combatSquad.readOnlyMembersList.Count == 0)
				{
					this.outer.SetNextState(new Exit());
					if (this.serverCycleCount < 1)
					{
						Action action = GoldshoresBossfight.onOneCycleGoldTitanKill;
						if (action == null)
						{
							return;
						}
						action();
					}
					return;
				}
			}
			if (this.scriptedCombatEncounter)
			{
				if (!this.bossImmunity)
				{
					if (this.bossInvulnerabilityStartTime.hasPassed)
					{
						this.ExtinguishBeacons();
						this.SetBossImmunity(true);
						this.serverCycleCount++;
						return;
					}
				}
				else if (this.missionController.beaconsActive >= this.missionController.beaconsToSpawnOnMap)
				{
					this.SetBossImmunity(false);
					this.bossInvulnerabilityStartTime = Run.FixedTimeStamp.now + GoldshoresBossfight.shieldRemovalDuration;
				}
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0002B464 File Offset: 0x00029664
		private void SpawnBoss()
		{
			if (this.hasSpawnedBoss)
			{
				return;
			}
			if (!this.scriptedCombatEncounter)
			{
				this.scriptedCombatEncounter = UnityEngine.Object.Instantiate<GameObject>(GoldshoresBossfight.combatEncounterPrefab).GetComponent<ScriptedCombatEncounter>();
				this.scriptedCombatEncounter.GetComponent<BossGroup>().dropPosition = this.missionController.bossSpawnPosition;
				NetworkServer.Spawn(this.scriptedCombatEncounter.gameObject);
			}
			this.scriptedCombatEncounter.BeginEncounter();
			this.hasSpawnedBoss = this.scriptedCombatEncounter.hasSpawnedServer;
			if (this.hasSpawnedBoss)
			{
				this.SetBossImmunity(true);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000A6F RID: 2671 RVA: 0x0002B4F4 File Offset: 0x000296F4
		// (remove) Token: 0x06000A70 RID: 2672 RVA: 0x0002B528 File Offset: 0x00029728
		public static event Action onOneCycleGoldTitanKill;

		// Token: 0x04000C1D RID: 3101
		private GoldshoresMissionController missionController;

		// Token: 0x04000C1E RID: 3102
		public static float shieldRemovalDuration;

		// Token: 0x04000C1F RID: 3103
		public static GameObject shieldRemovalEffectPrefab;

		// Token: 0x04000C20 RID: 3104
		public static GameObject shieldRegenerationEffectPrefab;

		// Token: 0x04000C21 RID: 3105
		public static GameObject combatEncounterPrefab;

		// Token: 0x04000C22 RID: 3106
		private static float transitionDuration = 3f;

		// Token: 0x04000C23 RID: 3107
		private bool hasSpawnedBoss;

		// Token: 0x04000C24 RID: 3108
		private int serverCycleCount;

		// Token: 0x04000C25 RID: 3109
		private Run.FixedTimeStamp bossInvulnerabilityStartTime;

		// Token: 0x04000C26 RID: 3110
		private ScriptedCombatEncounter scriptedCombatEncounter;

		// Token: 0x04000C27 RID: 3111
		private bool bossImmunity;
	}
}
