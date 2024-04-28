using System;
using System.Collections.Generic;
using RoR2.Items;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200098C RID: 2444
	public abstract class PickupDropTable : ScriptableObject
	{
		// Token: 0x06003777 RID: 14199 RVA: 0x000E9F3C File Offset: 0x000E813C
		protected static PickupIndex[] GenerateUniqueDropsFromWeightedSelection(int maxDrops, Xoroshiro128Plus rng, WeightedSelection<PickupIndex> weightedSelection)
		{
			int num = Math.Min(maxDrops, weightedSelection.Count);
			int[] array = Array.Empty<int>();
			PickupIndex[] array2 = new PickupIndex[num];
			for (int i = 0; i < num; i++)
			{
				int num2 = weightedSelection.EvaluateToChoiceIndex(rng.nextNormalizedFloat, array);
				WeightedSelection<PickupIndex>.ChoiceInfo choice = weightedSelection.GetChoice(num2);
				array2[i] = choice.value;
				Array.Resize<int>(ref array, i + 1);
				array[i] = num2;
			}
			return array2;
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x000E9FA6 File Offset: 0x000E81A6
		protected static PickupIndex GenerateDropFromWeightedSelection(Xoroshiro128Plus rng, WeightedSelection<PickupIndex> weightedSelection)
		{
			if (weightedSelection.Count > 0)
			{
				return weightedSelection.Evaluate(rng.nextNormalizedFloat);
			}
			return PickupIndex.none;
		}

		// Token: 0x06003779 RID: 14201
		public abstract int GetPickupCount();

		// Token: 0x0600377A RID: 14202
		protected abstract PickupIndex GenerateDropPreReplacement(Xoroshiro128Plus rng);

		// Token: 0x0600377B RID: 14203 RVA: 0x000E9FC4 File Offset: 0x000E81C4
		public PickupIndex GenerateDrop(Xoroshiro128Plus rng)
		{
			PickupIndex pickupIndex = this.GenerateDropPreReplacement(rng);
			if (pickupIndex == PickupIndex.none)
			{
				Debug.LogError("Could not generate pickup index from droptable.");
			}
			if (!pickupIndex.isValid)
			{
				Debug.LogError("Pickup index from droptable is invalid.");
			}
			if (this.canDropBeReplaced)
			{
				return RandomlyLunarUtils.CheckForLunarReplacement(pickupIndex, rng);
			}
			return pickupIndex;
		}

		// Token: 0x0600377C RID: 14204
		protected abstract PickupIndex[] GenerateUniqueDropsPreReplacement(int maxDrops, Xoroshiro128Plus rng);

		// Token: 0x0600377D RID: 14205 RVA: 0x000EA014 File Offset: 0x000E8214
		public PickupIndex[] GenerateUniqueDrops(int maxDrops, Xoroshiro128Plus rng)
		{
			PickupIndex[] array = this.GenerateUniqueDropsPreReplacement(maxDrops, rng);
			if (this.canDropBeReplaced)
			{
				RandomlyLunarUtils.CheckForLunarReplacementUniqueArray(array, rng);
			}
			return array;
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void Regenerate(Run run)
		{
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000EA03A File Offset: 0x000E823A
		protected virtual void OnEnable()
		{
			PickupDropTable.instancesList.Add(this);
			if (Run.instance)
			{
				Debug.Log("PickupDropTable '" + base.name + "' has been loaded after the Run started.  This might be an issue with asset duplication across bundles, or it might be fine.  Regenerating...");
				this.Regenerate(Run.instance);
			}
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x000EA078 File Offset: 0x000E8278
		protected virtual void OnDisable()
		{
			PickupDropTable.instancesList.Remove(this);
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000EA086 File Offset: 0x000E8286
		static PickupDropTable()
		{
			Run.onRunStartGlobal += PickupDropTable.RegenerateAll;
			Run.onAvailablePickupsModified += PickupDropTable.RegenerateAll;
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000EA0B4 File Offset: 0x000E82B4
		private static void RegenerateAll(Run run)
		{
			for (int i = 0; i < PickupDropTable.instancesList.Count; i++)
			{
				PickupDropTable.instancesList[i].Regenerate(run);
			}
		}

		// Token: 0x040037D9 RID: 14297
		public bool canDropBeReplaced = true;

		// Token: 0x040037DA RID: 14298
		private static readonly List<PickupDropTable> instancesList = new List<PickupDropTable>();
	}
}
