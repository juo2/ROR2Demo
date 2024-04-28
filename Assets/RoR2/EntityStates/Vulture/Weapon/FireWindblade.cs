using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Vulture.Weapon
{
	// Token: 0x020000E5 RID: 229
	public class FireWindblade : BaseSkillState
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x00011080 File Offset: 0x0000F280
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireWindblade.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("Gesture, Additive", "FireWindblade", "FireWindblade.playbackRate", this.duration);
			Util.PlaySound(FireWindblade.soundString, base.gameObject);
			if (FireWindblade.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireWindblade.muzzleEffectPrefab, base.gameObject, FireWindblade.muzzleString, false);
			}
			Ray aimRay = base.GetAimRay();
			if (base.isAuthority)
			{
				Quaternion rhs = Util.QuaternionSafeLookRotation(aimRay.direction);
				Quaternion lhs = Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), aimRay.direction);
				ProjectileManager.instance.FireProjectile(FireWindblade.projectilePrefab, aimRay.origin, lhs * rhs, base.gameObject, this.damageStat * FireWindblade.damageCoefficient, FireWindblade.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001117C File Offset: 0x0000F37C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x04000420 RID: 1056
		public static float baseDuration;

		// Token: 0x04000421 RID: 1057
		public static string muzzleString;

		// Token: 0x04000422 RID: 1058
		public static GameObject muzzleEffectPrefab;

		// Token: 0x04000423 RID: 1059
		public static GameObject projectilePrefab;

		// Token: 0x04000424 RID: 1060
		public static float damageCoefficient = 1.2f;

		// Token: 0x04000425 RID: 1061
		public static float force = 20f;

		// Token: 0x04000426 RID: 1062
		public static string soundString;

		// Token: 0x04000427 RID: 1063
		private float duration;
	}
}
