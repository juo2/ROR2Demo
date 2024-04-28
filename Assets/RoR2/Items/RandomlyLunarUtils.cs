using System;
using System.Collections.Generic;

namespace RoR2.Items
{
	// Token: 0x02000BE2 RID: 3042
	public static class RandomlyLunarUtils
	{
		// Token: 0x06004509 RID: 17673 RVA: 0x0011F6DC File Offset: 0x0011D8DC
		public static PickupIndex CheckForLunarReplacement(PickupIndex pickupIndex, Xoroshiro128Plus rng)
		{
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			if (RandomlyLunarUtils.CanReplace(pickupDef))
			{
				int itemCountGlobal = Util.GetItemCountGlobal(DLC1Content.Items.RandomlyLunar.itemIndex, false, false);
				if (itemCountGlobal > 0)
				{
					List<PickupIndex> list = null;
					if (pickupDef.itemIndex != ItemIndex.None)
					{
						list = Run.instance.availableLunarItemDropList;
					}
					else if (pickupDef.equipmentIndex != EquipmentIndex.None)
					{
						list = Run.instance.availableLunarEquipmentDropList;
					}
					if (list != null && list.Count > 0 && rng.nextNormalizedFloat < 0.05f * (float)itemCountGlobal)
					{
						int index = rng.RangeInt(0, list.Count);
						return list[index];
					}
				}
			}
			return pickupIndex;
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x0011F770 File Offset: 0x0011D970
		public static void CheckForLunarReplacementUniqueArray(PickupIndex[] pickupIndices, Xoroshiro128Plus rng)
		{
			int itemCountGlobal = Util.GetItemCountGlobal(DLC1Content.Items.RandomlyLunar.itemIndex, false, false);
			if (itemCountGlobal > 0)
			{
				List<PickupIndex> list = null;
				List<PickupIndex> list2 = null;
				for (int i = 0; i < pickupIndices.Length; i++)
				{
					PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndices[i]);
					if (RandomlyLunarUtils.CanReplace(pickupDef) && rng.nextNormalizedFloat < 0.05f * (float)itemCountGlobal)
					{
						List<PickupIndex> list3 = null;
						if (pickupDef.itemIndex != ItemIndex.None)
						{
							if (list == null)
							{
								list = new List<PickupIndex>(Run.instance.availableLunarItemDropList);
								Util.ShuffleList<PickupIndex>(list, rng);
							}
							list3 = list;
						}
						else if (pickupDef.equipmentIndex != EquipmentIndex.None)
						{
							if (list2 == null)
							{
								list2 = new List<PickupIndex>(Run.instance.availableLunarEquipmentDropList);
								Util.ShuffleList<PickupIndex>(list2, rng);
							}
							list3 = list2;
						}
						if (list3 != null && list3.Count > 0)
						{
							pickupIndices[i] = list3[i % list3.Count];
						}
					}
				}
			}
		}

		// Token: 0x0600450B RID: 17675 RVA: 0x0011F850 File Offset: 0x0011DA50
		public static bool CanReplace(PickupDef pickupDef)
		{
			return pickupDef != null && !pickupDef.isLunar;
		}

		// Token: 0x04004371 RID: 17265
		private const float replacePercentagePerStack = 0.05f;
	}
}
