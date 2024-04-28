using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RoR2.Items
{
	// Token: 0x02000BE4 RID: 3044
	public class RoboBallBuddyBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x06004511 RID: 17681 RVA: 0x0011F94A File Offset: 0x0011DB4A
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = false)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.RoboBallBuddy;
		}

		// Token: 0x06004512 RID: 17682 RVA: 0x0011F951 File Offset: 0x0011DB51
		private void FixedUpdate()
		{
			if (this.redBuddySpawner == null && base.isActiveAndEnabled)
			{
				this.CreateSpawners();
			}
		}

		// Token: 0x06004513 RID: 17683 RVA: 0x0011F969 File Offset: 0x0011DB69
		private void OnDisable()
		{
			this.DestroySpawners();
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x0011F974 File Offset: 0x0011DB74
		private void CreateSpawners()
		{
			RoboBallBuddyBodyBehavior.<>c__DisplayClass5_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.rng = new Xoroshiro128Plus(Run.instance.seed ^ (ulong)((long)base.GetInstanceID()));
			this.<CreateSpawners>g__CreateSpawner|5_0(ref this.redBuddySpawner, DeployableSlot.RoboBallRedBuddy, LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallRedBuddy"), ref CS$<>8__locals1);
			this.<CreateSpawners>g__CreateSpawner|5_0(ref this.greenBuddySpawner, DeployableSlot.RoboBallGreenBuddy, LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallGreenBuddy"), ref CS$<>8__locals1);
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x0011F9DB File Offset: 0x0011DBDB
		private void DestroySpawners()
		{
			DeployableMinionSpawner deployableMinionSpawner = this.redBuddySpawner;
			if (deployableMinionSpawner != null)
			{
				deployableMinionSpawner.Dispose();
			}
			this.redBuddySpawner = null;
			DeployableMinionSpawner deployableMinionSpawner2 = this.greenBuddySpawner;
			if (deployableMinionSpawner2 != null)
			{
				deployableMinionSpawner2.Dispose();
			}
			this.greenBuddySpawner = null;
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x0011FA10 File Offset: 0x0011DC10
		private void OnMinionSpawnedServer(SpawnCard.SpawnResult spawnResult)
		{
			GameObject spawnedInstance = spawnResult.spawnedInstance;
			if (spawnedInstance)
			{
				CharacterMaster component = spawnedInstance.GetComponent<CharacterMaster>();
				if (component)
				{
					Inventory inventory = base.body.inventory;
					Inventory inventory2 = component.inventory;
					if (inventory)
					{
						RoboBallBuddyBodyBehavior.InventorySync inventorySync = spawnedInstance.AddComponent<RoboBallBuddyBodyBehavior.InventorySync>();
						inventorySync.srcInventory = inventory;
						inventorySync.destInventory = inventory2;
					}
				}
			}
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x0011FA6C File Offset: 0x0011DC6C
		[CompilerGenerated]
		private void <CreateSpawners>g__CreateSpawner|5_0(ref DeployableMinionSpawner buddySpawner, DeployableSlot deployableSlot, SpawnCard spawnCard, ref RoboBallBuddyBodyBehavior.<>c__DisplayClass5_0 A_4)
		{
			buddySpawner = new DeployableMinionSpawner(base.body.master, deployableSlot, A_4.rng)
			{
				respawnInterval = 30f,
				spawnCard = spawnCard
			};
			buddySpawner.onMinionSpawnedServer += this.OnMinionSpawnedServer;
		}

		// Token: 0x04004373 RID: 17267
		private DeployableMinionSpawner redBuddySpawner;

		// Token: 0x04004374 RID: 17268
		private DeployableMinionSpawner greenBuddySpawner;

		// Token: 0x02000BE5 RID: 3045
		private class InventorySync : MonoBehaviour
		{
			// Token: 0x06004519 RID: 17689 RVA: 0x0011FAB8 File Offset: 0x0011DCB8
			private void FixedUpdate()
			{
				if (this.srcInventory && this.destInventory)
				{
					int itemCount = this.srcInventory.GetItemCount(RoR2Content.Items.RoboBallBuddy);
					int num = itemCount - this.granted;
					if (num != 0)
					{
						this.destInventory.GiveItem(RoR2Content.Items.TeamSizeDamageBonus, num);
						this.granted = itemCount;
					}
				}
			}

			// Token: 0x04004375 RID: 17269
			public Inventory srcInventory;

			// Token: 0x04004376 RID: 17270
			public Inventory destInventory;

			// Token: 0x04004377 RID: 17271
			private int granted;
		}
	}
}
