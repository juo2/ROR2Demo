using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B0C RID: 2828
	public class DelayedHitOrb : GenericDamageOrb
	{
		// Token: 0x060040A6 RID: 16550 RVA: 0x0010B68D File Offset: 0x0010988D
		public override void Begin()
		{
			base.Begin();
			base.duration = this.delay;
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x0010B6A1 File Offset: 0x001098A1
		protected override GameObject GetOrbEffect()
		{
			return this.orbEffect;
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x0010B6AC File Offset: 0x001098AC
		public override void OnArrival()
		{
			if (this.target && this.target.transform)
			{
				EffectManager.SpawnEffect(this.delayedEffectPrefab, new EffectData
				{
					origin = this.target.transform.position
				}, true);
				base.OnArrival();
			}
		}

		// Token: 0x04003EFD RID: 16125
		public float delay;

		// Token: 0x04003EFE RID: 16126
		public GameObject delayedEffectPrefab;

		// Token: 0x04003EFF RID: 16127
		public GameObject orbEffect;
	}
}
