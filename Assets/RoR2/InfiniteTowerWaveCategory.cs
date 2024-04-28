using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200053D RID: 1341
	[CreateAssetMenu(menuName = "RoR2/Infinite Tower/Infinite Tower Wave Category")]
	public class InfiniteTowerWaveCategory : ScriptableObject
	{
		// Token: 0x06001873 RID: 6259 RVA: 0x0006AAF8 File Offset: 0x00068CF8
		public bool IsAvailable(InfiniteTowerRun run)
		{
			bool flag = this.availabilityPeriod > 0 && run.waveIndex % this.availabilityPeriod == 0;
			bool flag2 = run.waveIndex >= this.minWaveIndex;
			bool flag3 = this.wavePrefabs.Length != 0;
			if (flag && flag2 && flag3)
			{
				foreach (InfiniteTowerWaveCategory.WeightedWave weightedWave in this.wavePrefabs)
				{
					if (!weightedWave.prerequisites || weightedWave.prerequisites.AreMet(run))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x0006AB84 File Offset: 0x00068D84
		public GameObject SelectWavePrefab(InfiniteTowerRun run, Xoroshiro128Plus rng)
		{
			if (this.weightedSelection.Count < this.wavePrefabs.Length)
			{
				this.GenerateWeightedSelection();
			}
			foreach (InfiniteTowerWaveCategory.WeightedWave weightedWave in this.wavePrefabs)
			{
				if (weightedWave.prerequisites && !weightedWave.prerequisites.AreMet(run))
				{
					for (int j = 0; j < this.weightedSelection.Count; j++)
					{
						if (this.weightedSelection.choices[j].value == weightedWave.wavePrefab)
						{
							this.weightedSelection.RemoveChoice(j);
							break;
						}
					}
				}
			}
			return this.weightedSelection.Evaluate(rng.nextNormalizedFloat);
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x0006AC38 File Offset: 0x00068E38
		private void GenerateWeightedSelection()
		{
			this.weightedSelection.Clear();
			foreach (InfiniteTowerWaveCategory.WeightedWave weightedWave in this.wavePrefabs)
			{
				this.weightedSelection.AddChoice(weightedWave.wavePrefab, weightedWave.weight);
			}
		}

		// Token: 0x04001E05 RID: 7685
		private readonly WeightedSelection<GameObject> weightedSelection = new WeightedSelection<GameObject>(8);

		// Token: 0x04001E06 RID: 7686
		[Tooltip("The weighted selection for which wave prefab to use")]
		[SerializeField]
		private InfiniteTowerWaveCategory.WeightedWave[] wavePrefabs;

		// Token: 0x04001E07 RID: 7687
		[Tooltip("The period of how often this category is available, e.g.\"every N waves\".")]
		[SerializeField]
		private int availabilityPeriod;

		// Token: 0x04001E08 RID: 7688
		[Tooltip("The minimum wave index where this category is available (inclusive)")]
		[SerializeField]
		private int minWaveIndex;

		// Token: 0x0200053E RID: 1342
		[Serializable]
		public struct WeightedWave
		{
			// Token: 0x04001E09 RID: 7689
			public GameObject wavePrefab;

			// Token: 0x04001E0A RID: 7690
			public InfiniteTowerWavePrerequisites prerequisites;

			// Token: 0x04001E0B RID: 7691
			public float weight;
		}
	}
}
