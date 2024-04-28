using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005B9 RID: 1465
	[CreateAssetMenu(menuName = "RoR2/DropTables/FreeChestDropTable")]
	public class FreeChestDropTable : PickupDropTable
	{
		// Token: 0x06001A87 RID: 6791 RVA: 0x00071DE0 File Offset: 0x0006FFE0
		private void Add(List<PickupIndex> sourceDropList, float listWeight)
		{
			if (listWeight <= 0f || sourceDropList.Count == 0)
			{
				return;
			}
			float weight = listWeight / (float)sourceDropList.Count;
			foreach (PickupIndex value in sourceDropList)
			{
				this.selector.AddChoice(value, weight);
			}
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00071E50 File Offset: 0x00070050
		protected override PickupIndex GenerateDropPreReplacement(Xoroshiro128Plus rng)
		{
			int num = 0;
			foreach (CharacterMaster characterMaster in CharacterMaster.readOnlyInstancesList)
			{
				int itemCount = characterMaster.inventory.GetItemCount(DLC1Content.Items.FreeChest);
				num += itemCount;
			}
			this.selector.Clear();
			this.Add(Run.instance.availableTier1DropList, this.tier1Weight);
			this.Add(Run.instance.availableTier2DropList, this.tier2Weight * (float)num);
			this.Add(Run.instance.availableTier3DropList, this.tier3Weight * Mathf.Pow((float)num, 2f));
			return PickupDropTable.GenerateDropFromWeightedSelection(rng, this.selector);
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00071F14 File Offset: 0x00070114
		public override int GetPickupCount()
		{
			return this.selector.Count;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00071F21 File Offset: 0x00070121
		protected override PickupIndex[] GenerateUniqueDropsPreReplacement(int maxDrops, Xoroshiro128Plus rng)
		{
			return PickupDropTable.GenerateUniqueDropsFromWeightedSelection(maxDrops, rng, this.selector);
		}

		// Token: 0x04002098 RID: 8344
		[SerializeField]
		private float tier1Weight = 0.79f;

		// Token: 0x04002099 RID: 8345
		[SerializeField]
		private float tier2Weight = 0.2f;

		// Token: 0x0400209A RID: 8346
		[SerializeField]
		private float tier3Weight = 0.01f;

		// Token: 0x0400209B RID: 8347
		private readonly WeightedSelection<PickupIndex> selector = new WeightedSelection<PickupIndex>(8);
	}
}
