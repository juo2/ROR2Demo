using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005B2 RID: 1458
	[CreateAssetMenu(menuName = "RoR2/DropTables/ExplicitPickupDropTable")]
	public class ExplicitPickupDropTable : PickupDropTable
	{
		// Token: 0x06001A6E RID: 6766 RVA: 0x0007193D File Offset: 0x0006FB3D
		protected override void Regenerate(Run run)
		{
			this.GenerateWeightedSelection();
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00071948 File Offset: 0x0006FB48
		private void GenerateWeightedSelection()
		{
			this.weightedSelection.Clear();
			foreach (ExplicitPickupDropTable.StringEntry stringEntry in this.entries)
			{
				this.weightedSelection.AddChoice(PickupCatalog.FindPickupIndex(stringEntry.pickupName), stringEntry.pickupWeight);
			}
			ExplicitPickupDropTable.PickupDefEntry[] array2 = this.pickupEntries;
			int i = 0;
			while (i < array2.Length)
			{
				ExplicitPickupDropTable.PickupDefEntry pickupDefEntry = array2[i];
				PickupIndex pickupIndex = PickupIndex.none;
				UnityEngine.Object pickupDef = pickupDefEntry.pickupDef;
				if (pickupDef == null)
				{
					goto IL_A9;
				}
				ItemDef itemDef;
				if ((itemDef = (pickupDef as ItemDef)) == null)
				{
					EquipmentDef equipmentDef;
					if ((equipmentDef = (pickupDef as EquipmentDef)) == null)
					{
						goto IL_A9;
					}
					pickupIndex = PickupCatalog.FindPickupIndex(equipmentDef.equipmentIndex);
				}
				else
				{
					pickupIndex = PickupCatalog.FindPickupIndex(itemDef.itemIndex);
				}
				IL_CF:
				if (pickupIndex != PickupIndex.none)
				{
					this.weightedSelection.AddChoice(pickupIndex, pickupDefEntry.pickupWeight);
				}
				i++;
				continue;
				IL_A9:
				MiscPickupDef miscPickupDef = pickupDefEntry.pickupDef as MiscPickupDef;
				if (miscPickupDef != null)
				{
					pickupIndex = PickupCatalog.FindPickupIndex(miscPickupDef.miscPickupIndex);
					goto IL_CF;
				}
				goto IL_CF;
			}
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x00071A53 File Offset: 0x0006FC53
		protected override PickupIndex GenerateDropPreReplacement(Xoroshiro128Plus rng)
		{
			return PickupDropTable.GenerateDropFromWeightedSelection(rng, this.weightedSelection);
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00071A61 File Offset: 0x0006FC61
		public override int GetPickupCount()
		{
			return this.weightedSelection.Count;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00071A6E File Offset: 0x0006FC6E
		protected override PickupIndex[] GenerateUniqueDropsPreReplacement(int maxDrops, Xoroshiro128Plus rng)
		{
			return PickupDropTable.GenerateUniqueDropsFromWeightedSelection(maxDrops, rng, this.weightedSelection);
		}

		// Token: 0x04002086 RID: 8326
		public ExplicitPickupDropTable.PickupDefEntry[] pickupEntries = Array.Empty<ExplicitPickupDropTable.PickupDefEntry>();

		// Token: 0x04002087 RID: 8327
		[Obsolete("Use pickupEntries instead.", false)]
		[Header("Deprecated")]
		public ExplicitPickupDropTable.StringEntry[] entries = Array.Empty<ExplicitPickupDropTable.StringEntry>();

		// Token: 0x04002088 RID: 8328
		private readonly WeightedSelection<PickupIndex> weightedSelection = new WeightedSelection<PickupIndex>(8);

		// Token: 0x020005B3 RID: 1459
		[Serializable]
		public struct StringEntry
		{
			// Token: 0x04002089 RID: 8329
			public string pickupName;

			// Token: 0x0400208A RID: 8330
			public float pickupWeight;
		}

		// Token: 0x020005B4 RID: 1460
		[Serializable]
		public struct PickupDefEntry
		{
			// Token: 0x0400208B RID: 8331
			[TypeRestrictedReference(new Type[]
			{
				typeof(ItemDef),
				typeof(EquipmentDef),
				typeof(MiscPickupDef)
			})]
			public UnityEngine.Object pickupDef;

			// Token: 0x0400208C RID: 8332
			public float pickupWeight;
		}
	}
}
