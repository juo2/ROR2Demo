using System;
using RoR2.Orbs;
using UnityEngine;

namespace EntityStates.Huntress.HuntressWeapon
{
	// Token: 0x02000323 RID: 803
	public class FireFlurrySeekingArrow : FireSeekingArrow
	{
		// Token: 0x06000E62 RID: 3682 RVA: 0x0003E0C1 File Offset: 0x0003C2C1
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.isCrit)
			{
				this.muzzleflashEffectPrefab = FireFlurrySeekingArrow.critMuzzleflashEffectPrefab;
				this.maxArrowCount = FireFlurrySeekingArrow.critMaxArrowCount;
				this.arrowReloadDuration = FireFlurrySeekingArrow.critBaseArrowReloadDuration / this.attackSpeedStat;
			}
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0003E0F9 File Offset: 0x0003C2F9
		protected override GenericDamageOrb CreateArrowOrb()
		{
			return new HuntressFlurryArrowOrb();
		}

		// Token: 0x04001202 RID: 4610
		public static GameObject critMuzzleflashEffectPrefab;

		// Token: 0x04001203 RID: 4611
		public static int critMaxArrowCount;

		// Token: 0x04001204 RID: 4612
		public static float critBaseArrowReloadDuration;
	}
}
