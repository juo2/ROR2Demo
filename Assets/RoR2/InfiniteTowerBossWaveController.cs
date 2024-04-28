using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000769 RID: 1897
	public class InfiniteTowerBossWaveController : InfiniteTowerWaveController
	{
		// Token: 0x06002736 RID: 10038 RVA: 0x000AA758 File Offset: 0x000A8958
		[Server]
		public override void Initialize(int waveIndex, Inventory enemyInventory, GameObject spawnTarget)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerBossWaveController::Initialize(System.Int32,RoR2.Inventory,UnityEngine.GameObject)' called on client");
				return;
			}
			base.Initialize(waveIndex, enemyInventory, spawnTarget);
			this.combatDirector.SetNextSpawnAsBoss();
			if (this.guaranteeInitialChampion && this.combatDirector.monsterCredit < (float)this.combatDirector.lastAttemptedMonsterCard.cost)
			{
				this.combatDirector.monsterCredit = (float)this.combatDirector.lastAttemptedMonsterCard.cost;
			}
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x000AA7D0 File Offset: 0x000A89D0
		protected override void OnAllEnemiesDefeatedServer()
		{
			base.OnAllEnemiesDefeatedServer();
			if (this.rewardInteractableSpawner)
			{
				this.rewardInteractableSpawner.Spawn(new Xoroshiro128Plus(Run.instance.seed ^ (ulong)((long)this.waveIndex)));
			}
			InfiniteTowerRun infiniteTowerRun = Run.instance as InfiniteTowerRun;
			if (infiniteTowerRun && !infiniteTowerRun.IsStageTransitionWave())
			{
				infiniteTowerRun.MoveSafeWard();
			}
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000AA834 File Offset: 0x000A8A34
		protected override void OnFinishedServer()
		{
			base.OnFinishedServer();
			if (this.rewardInteractableSpawner)
			{
				this.rewardInteractableSpawner.DestroySpawnedInteractables();
			}
			if (this.clearPickupsOnFinish)
			{
				foreach (GenericPickupController genericPickupController in new List<GenericPickupController>(InstanceTracker.GetInstancesList<GenericPickupController>()))
				{
					PickupDef pickupDef = PickupCatalog.GetPickupDef(genericPickupController.pickupIndex);
					if (pickupDef.itemIndex != ItemIndex.None || pickupDef.equipmentIndex != EquipmentIndex.None)
					{
						UnityEngine.Object.Destroy(genericPickupController.gameObject);
					}
				}
			}
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void OnTimerExpire()
		{
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000AA8DC File Offset: 0x000A8ADC
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool flag = base.OnSerialize(writer, forceAll);
			bool flag2;
			return flag2 || flag;
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000AA8F5 File Offset: 0x000A8AF5
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			base.OnDeserialize(reader, initialState);
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000AA8FF File Offset: 0x000A8AFF
		public override void PreStartClient()
		{
			base.PreStartClient();
		}

		// Token: 0x04002B2E RID: 11054
		[SerializeField]
		[Tooltip("The interactable spawner to activate when the player defeats all enemies")]
		private InteractableSpawner rewardInteractableSpawner;

		// Token: 0x04002B2F RID: 11055
		[SerializeField]
		[Tooltip("If true, it ensures that the combat director gets enough credits to spawn the initially selected champion spawn card.")]
		private bool guaranteeInitialChampion;

		// Token: 0x04002B30 RID: 11056
		[SerializeField]
		[Tooltip("If true, clear the pickups when this wave finishes")]
		private bool clearPickupsOnFinish;
	}
}
