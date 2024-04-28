using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004D7 RID: 1239
	[CreateAssetMenu(menuName = "RoR2/DropTables/BasicPickupDropTable")]
	public class BasicPickupDropTable : PickupDropTable
	{
		// Token: 0x0600167E RID: 5758 RVA: 0x00063110 File Offset: 0x00061310
		protected override void Regenerate(Run run)
		{
			this.GenerateWeightedSelection(run);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00063119 File Offset: 0x00061319
		private bool IsFilterRequired()
		{
			return this.requiredItemTags.Length != 0 || this.bannedItemTags.Length != 0;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00063130 File Offset: 0x00061330
		private bool PassesFilter(PickupIndex pickupIndex)
		{
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			if (pickupDef.itemIndex != ItemIndex.None)
			{
				ItemDef itemDef = ItemCatalog.GetItemDef(pickupDef.itemIndex);
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

		// Token: 0x06001681 RID: 5761 RVA: 0x000631B4 File Offset: 0x000613B4
		private void Add(List<PickupIndex> sourceDropList, float chance)
		{
			if (chance <= 0f || sourceDropList.Count == 0)
			{
				return;
			}
			foreach (PickupIndex pickupIndex in sourceDropList)
			{
				if (!this.IsFilterRequired() || this.PassesFilter(pickupIndex))
				{
					this.selector.AddChoice(pickupIndex, chance);
				}
			}
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0006322C File Offset: 0x0006142C
		private void GenerateWeightedSelection(Run run)
		{
			this.selector.Clear();
			this.Add(run.availableTier1DropList, this.tier1Weight);
			this.Add(run.availableTier2DropList, this.tier2Weight);
			this.Add(run.availableTier3DropList, this.tier3Weight);
			this.Add(run.availableBossDropList, this.bossWeight);
			this.Add(run.availableLunarItemDropList, this.lunarItemWeight);
			this.Add(run.availableLunarEquipmentDropList, this.lunarEquipmentWeight);
			this.Add(run.availableLunarCombinedDropList, this.lunarCombinedWeight);
			this.Add(run.availableEquipmentDropList, this.equipmentWeight);
			this.Add(run.availableVoidTier1DropList, this.voidTier1Weight);
			this.Add(run.availableVoidTier2DropList, this.voidTier2Weight);
			this.Add(run.availableVoidTier3DropList, this.voidTier3Weight);
			this.Add(run.availableVoidBossDropList, this.voidBossWeight);
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x0006331C File Offset: 0x0006151C
		protected override PickupIndex GenerateDropPreReplacement(Xoroshiro128Plus rng)
		{
			return PickupDropTable.GenerateDropFromWeightedSelection(rng, this.selector);
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0006332A File Offset: 0x0006152A
		public override int GetPickupCount()
		{
			return this.selector.Count;
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00063337 File Offset: 0x00061537
		protected override PickupIndex[] GenerateUniqueDropsPreReplacement(int maxDrops, Xoroshiro128Plus rng)
		{
			return PickupDropTable.GenerateUniqueDropsFromWeightedSelection(maxDrops, rng, this.selector);
		}

		// Token: 0x04001C2E RID: 7214
		public ItemTag[] requiredItemTags = Array.Empty<ItemTag>();

		// Token: 0x04001C2F RID: 7215
		public ItemTag[] bannedItemTags = Array.Empty<ItemTag>();

		// Token: 0x04001C30 RID: 7216
		public float tier1Weight = 0.8f;

		// Token: 0x04001C31 RID: 7217
		public float tier2Weight = 0.2f;

		// Token: 0x04001C32 RID: 7218
		public float tier3Weight = 0.01f;

		// Token: 0x04001C33 RID: 7219
		public float bossWeight;

		// Token: 0x04001C34 RID: 7220
		public float lunarEquipmentWeight;

		// Token: 0x04001C35 RID: 7221
		public float lunarItemWeight;

		// Token: 0x04001C36 RID: 7222
		public float lunarCombinedWeight;

		// Token: 0x04001C37 RID: 7223
		public float equipmentWeight;

		// Token: 0x04001C38 RID: 7224
		public float voidTier1Weight;

		// Token: 0x04001C39 RID: 7225
		public float voidTier2Weight;

		// Token: 0x04001C3A RID: 7226
		public float voidTier3Weight;

		// Token: 0x04001C3B RID: 7227
		public float voidBossWeight;

		// Token: 0x04001C3C RID: 7228
		private readonly WeightedSelection<PickupIndex> selector = new WeightedSelection<PickupIndex>(8);
	}
}
