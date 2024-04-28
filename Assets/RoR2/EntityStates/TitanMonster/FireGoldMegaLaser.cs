using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.TitanMonster
{
	// Token: 0x02000365 RID: 869
	public class FireGoldMegaLaser : FireMegaLaser
	{
		// Token: 0x06000F9C RID: 3996 RVA: 0x00044A9C File Offset: 0x00042C9C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.projectileStopwatch += Time.fixedDeltaTime * this.attackSpeedStat;
				if (this.projectileStopwatch >= 1f / FireGoldMegaLaser.projectileFireFrequency)
				{
					Ray aimRay = base.GetAimRay();
					if (this.muzzleTransform)
					{
						aimRay.origin = this.muzzleTransform.transform.position;
					}
					aimRay.direction = Util.ApplySpread(aimRay.direction, FireGoldMegaLaser.projectileMinSpread, FireGoldMegaLaser.projectileMaxSpread, 1f, 1f, 0f, 0f);
					this.projectileStopwatch -= 1f / FireGoldMegaLaser.projectileFireFrequency;
					ProjectileManager.instance.FireProjectile(FireGoldMegaLaser.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * FireMegaLaser.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
				}
			}
		}

		// Token: 0x040013D8 RID: 5080
		public static GameObject projectilePrefab;

		// Token: 0x040013D9 RID: 5081
		public static float projectileFireFrequency;

		// Token: 0x040013DA RID: 5082
		public static float projectileDamageCoefficient;

		// Token: 0x040013DB RID: 5083
		public static float projectileMinSpread;

		// Token: 0x040013DC RID: 5084
		public static float projectileMaxSpread;

		// Token: 0x040013DD RID: 5085
		private float projectileStopwatch;
	}
}
