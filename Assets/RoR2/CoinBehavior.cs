using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000661 RID: 1633
	[RequireComponent(typeof(EffectComponent))]
	public class CoinBehavior : MonoBehaviour
	{
		// Token: 0x06001FA5 RID: 8101 RVA: 0x00087B94 File Offset: 0x00085D94
		private void Start()
		{
			this.originalCoinCount = (int)base.GetComponent<EffectComponent>().effectData.genericFloat;
			int i = this.originalCoinCount;
			for (int j = 0; j < this.coinTiers.Length; j++)
			{
				CoinBehavior.CoinTier coinTier = this.coinTiers[j];
				int num = 0;
				while (i >= coinTier.valuePerCoin)
				{
					i -= coinTier.valuePerCoin;
					num++;
				}
				if (num > 0)
				{
					ParticleSystem.EmissionModule emission = coinTier.particleSystem.emission;
					emission.enabled = true;
					emission.SetBursts(new ParticleSystem.Burst[]
					{
						new ParticleSystem.Burst(0f, (float)num)
					});
					coinTier.particleSystem.gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x04002524 RID: 9508
		public int originalCoinCount;

		// Token: 0x04002525 RID: 9509
		public CoinBehavior.CoinTier[] coinTiers;

		// Token: 0x02000662 RID: 1634
		[Serializable]
		public struct CoinTier
		{
			// Token: 0x04002526 RID: 9510
			public ParticleSystem particleSystem;

			// Token: 0x04002527 RID: 9511
			public int valuePerCoin;
		}
	}
}
