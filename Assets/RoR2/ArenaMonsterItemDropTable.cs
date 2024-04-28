using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004C8 RID: 1224
	[CreateAssetMenu(menuName = "RoR2/DropTables/ArenaMonsterItemDropTable")]
	public class ArenaMonsterItemDropTable : PickupDropTable
	{
		// Token: 0x0600162F RID: 5679 RVA: 0x000621C4 File Offset: 0x000603C4
		private bool PassesFilter(PickupIndex pickupIndex)
		{
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			if (pickupDef.itemIndex != ItemIndex.None)
			{
				ItemDef itemDef = ItemCatalog.GetItemDef(pickupDef.itemIndex);
				if (ArenaMissionController.instance.inventory.GetItemCount(itemDef.itemIndex) > 0)
				{
					return false;
				}
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
			}
			return true;
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x00062260 File Offset: 0x00060460
		private void Add(List<PickupIndex> sourceDropList, float chance)
		{
			if (chance <= 0f || sourceDropList.Count == 0)
			{
				return;
			}
			foreach (PickupIndex pickupIndex in sourceDropList)
			{
				if (this.PassesFilter(pickupIndex))
				{
					this.selector.AddChoice(pickupIndex, chance);
				}
			}
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x000622D0 File Offset: 0x000604D0
		private void GenerateWeightedSelection(Run run)
		{
			this.selector.Clear();
			this.Add(run.availableTier1DropList, this.tier1Weight);
			this.Add(run.availableTier2DropList, this.tier2Weight);
			this.Add(run.availableTier3DropList, this.tier3Weight);
			this.Add(run.availableBossDropList, this.bossWeight);
			this.Add(run.availableLunarItemDropList, this.lunarItemWeight);
			this.Add(run.availableVoidTier1DropList, this.voidTier1Weight);
			this.Add(run.availableVoidTier2DropList, this.voidTier2Weight);
			this.Add(run.availableVoidTier3DropList, this.voidTier3Weight);
			this.Add(run.availableVoidBossDropList, this.voidBossWeight);
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0006238A File Offset: 0x0006058A
		protected override PickupIndex GenerateDropPreReplacement(Xoroshiro128Plus rng)
		{
			this.GenerateWeightedSelection(Run.instance);
			return PickupDropTable.GenerateDropFromWeightedSelection(rng, this.selector);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x000623A3 File Offset: 0x000605A3
		public override int GetPickupCount()
		{
			return this.selector.Count;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x000623B0 File Offset: 0x000605B0
		protected override PickupIndex[] GenerateUniqueDropsPreReplacement(int maxDrops, Xoroshiro128Plus rng)
		{
			return PickupDropTable.GenerateUniqueDropsFromWeightedSelection(maxDrops, rng, this.selector);
		}

		// Token: 0x04001BF0 RID: 7152
		public ItemTag[] requiredItemTags;

		// Token: 0x04001BF1 RID: 7153
		public ItemTag[] bannedItemTags;

		// Token: 0x04001BF2 RID: 7154
		public float tier1Weight = 0.8f;

		// Token: 0x04001BF3 RID: 7155
		public float tier2Weight = 0.2f;

		// Token: 0x04001BF4 RID: 7156
		public float tier3Weight = 0.01f;

		// Token: 0x04001BF5 RID: 7157
		public float bossWeight;

		// Token: 0x04001BF6 RID: 7158
		public float lunarItemWeight;

		// Token: 0x04001BF7 RID: 7159
		public float voidTier1Weight;

		// Token: 0x04001BF8 RID: 7160
		public float voidTier2Weight;

		// Token: 0x04001BF9 RID: 7161
		public float voidTier3Weight;

		// Token: 0x04001BFA RID: 7162
		public float voidBossWeight;

		// Token: 0x04001BFB RID: 7163
		private readonly WeightedSelection<PickupIndex> selector = new WeightedSelection<PickupIndex>(8);
	}
}
