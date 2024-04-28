using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Loader
{
	// Token: 0x020002CD RID: 717
	public class ThrowPylon : BaseState
	{
		// Token: 0x06000CBB RID: 3259 RVA: 0x00035908 File Offset: 0x00033B08
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ThrowPylon.baseDuration / this.attackSpeedStat;
			if (base.isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
				{
					crit = base.RollCrit(),
					damage = this.damageStat * ThrowPylon.damageCoefficient,
					damageColorIndex = DamageColorIndex.Default,
					force = 0f,
					owner = base.gameObject,
					position = aimRay.origin,
					procChainMask = default(ProcChainMask),
					projectilePrefab = ThrowPylon.projectilePrefab,
					rotation = Quaternion.LookRotation(aimRay.direction),
					target = null
				};
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
			EffectManager.SimpleMuzzleFlash(ThrowPylon.muzzleflashObject, base.gameObject, ThrowPylon.muzzleString, false);
			Util.PlaySound(ThrowPylon.soundString, base.gameObject);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x000359FE File Offset: 0x00033BFE
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && this.duration <= base.age)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}

		// Token: 0x04000F83 RID: 3971
		public static GameObject projectilePrefab;

		// Token: 0x04000F84 RID: 3972
		public static float baseDuration;

		// Token: 0x04000F85 RID: 3973
		public static float damageCoefficient;

		// Token: 0x04000F86 RID: 3974
		public static string muzzleString;

		// Token: 0x04000F87 RID: 3975
		public static GameObject muzzleflashObject;

		// Token: 0x04000F88 RID: 3976
		public static string soundString;

		// Token: 0x04000F89 RID: 3977
		private float duration;
	}
}
