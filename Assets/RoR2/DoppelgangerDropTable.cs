using System;
using RoR2.Artifacts;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200058C RID: 1420
	[CreateAssetMenu(menuName = "RoR2/DropTables/DoppelgangerDropTable")]
	public class DoppelgangerDropTable : PickupDropTable
	{
		// Token: 0x06001973 RID: 6515 RVA: 0x0006E1EC File Offset: 0x0006C3EC
		protected override void OnEnable()
		{
			base.OnEnable();
			DoppelgangerInvasionManager.onDoppelgangerDeath += this.OnDoppelgangerDeath;
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0006E205 File Offset: 0x0006C405
		protected override void OnDisable()
		{
			DoppelgangerInvasionManager.onDoppelgangerDeath -= this.OnDoppelgangerDeath;
			base.OnDisable();
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0006E21E File Offset: 0x0006C41E
		protected override void Regenerate(Run run)
		{
			this.GenerateWeightedSelection();
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0006E228 File Offset: 0x0006C428
		private bool CanSelectItem(ItemDef itemDef)
		{
			foreach (ItemTag value in this.requiredItemTags)
			{
				if (Array.IndexOf<ItemTag>(itemDef.tags, value) == -1)
				{
					return false;
				}
			}
			foreach (ItemTag value2 in this.bannedItemTags)
			{
				if (Array.IndexOf<ItemTag>(itemDef.tags, value2) != -1)
				{
					return false;
				}
			}
			return itemDef.canRemove;
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0006E290 File Offset: 0x0006C490
		private void GenerateWeightedSelection()
		{
			this.selector.Clear();
			if (this.doppelgangerInventory)
			{
				foreach (ItemIndex itemIndex in this.doppelgangerInventory.itemAcquisitionOrder)
				{
					ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
					PickupIndex pickupIndex = PickupCatalog.FindPickupIndex(itemIndex);
					if (this.CanSelectItem(itemDef))
					{
						float num = 0f;
						switch (itemDef.tier)
						{
						case ItemTier.Tier1:
							if (Run.instance.availableTier1DropList.Contains(pickupIndex))
							{
								num = this.tier1Weight;
							}
							break;
						case ItemTier.Tier2:
							if (Run.instance.availableTier2DropList.Contains(pickupIndex))
							{
								num = this.tier2Weight;
							}
							break;
						case ItemTier.Tier3:
							if (Run.instance.availableTier3DropList.Contains(pickupIndex))
							{
								num = this.tier3Weight;
							}
							break;
						case ItemTier.Lunar:
							if (Run.instance.availableLunarItemDropList.Contains(pickupIndex))
							{
								num = this.lunarItemWeight;
							}
							break;
						case ItemTier.Boss:
							if (Run.instance.availableBossDropList.Contains(pickupIndex))
							{
								num = this.bossWeight;
							}
							break;
						case ItemTier.VoidTier1:
							if (Run.instance.availableVoidTier1DropList.Contains(pickupIndex))
							{
								num = this.voidTier1Weight;
							}
							break;
						case ItemTier.VoidTier2:
							if (Run.instance.availableVoidTier2DropList.Contains(pickupIndex))
							{
								num = this.voidTier2Weight;
							}
							break;
						case ItemTier.VoidTier3:
							if (Run.instance.availableVoidTier3DropList.Contains(pickupIndex))
							{
								num = this.voidTier3Weight;
							}
							break;
						case ItemTier.VoidBoss:
							if (Run.instance.availableVoidTier3DropList.Contains(pickupIndex))
							{
								num = this.voidBossWeight;
							}
							break;
						}
						if (num > 0f)
						{
							this.selector.AddChoice(pickupIndex, num);
						}
					}
				}
			}
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0006E488 File Offset: 0x0006C688
		protected override PickupIndex GenerateDropPreReplacement(Xoroshiro128Plus rng)
		{
			return PickupDropTable.GenerateDropFromWeightedSelection(rng, this.selector);
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0006E496 File Offset: 0x0006C696
		private void OnDoppelgangerDeath(DamageReport damageReport)
		{
			CharacterMaster victimMaster = damageReport.victimMaster;
			this.doppelgangerInventory = ((victimMaster != null) ? victimMaster.inventory : null);
			this.GenerateWeightedSelection();
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0006E4B6 File Offset: 0x0006C6B6
		public override int GetPickupCount()
		{
			return this.selector.Count;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0006E4C3 File Offset: 0x0006C6C3
		protected override PickupIndex[] GenerateUniqueDropsPreReplacement(int maxDrops, Xoroshiro128Plus rng)
		{
			return PickupDropTable.GenerateUniqueDropsFromWeightedSelection(maxDrops, rng, this.selector);
		}

		// Token: 0x04001FE1 RID: 8161
		public ItemTag[] requiredItemTags = Array.Empty<ItemTag>();

		// Token: 0x04001FE2 RID: 8162
		public ItemTag[] bannedItemTags = Array.Empty<ItemTag>();

		// Token: 0x04001FE3 RID: 8163
		public float tier1Weight = 0.8f;

		// Token: 0x04001FE4 RID: 8164
		public float tier2Weight = 0.2f;

		// Token: 0x04001FE5 RID: 8165
		public float tier3Weight = 0.01f;

		// Token: 0x04001FE6 RID: 8166
		public float bossWeight;

		// Token: 0x04001FE7 RID: 8167
		public float lunarItemWeight;

		// Token: 0x04001FE8 RID: 8168
		public float voidTier1Weight;

		// Token: 0x04001FE9 RID: 8169
		public float voidTier2Weight;

		// Token: 0x04001FEA RID: 8170
		public float voidTier3Weight;

		// Token: 0x04001FEB RID: 8171
		public float voidBossWeight;

		// Token: 0x04001FEC RID: 8172
		private readonly WeightedSelection<PickupIndex> selector = new WeightedSelection<PickupIndex>(8);

		// Token: 0x04001FED RID: 8173
		private Inventory doppelgangerInventory;
	}
}
