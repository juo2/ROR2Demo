using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Paladin.PaladinWeapon
{
	// Token: 0x02000230 RID: 560
	public class FireBigRocket : BaseState
	{
		// Token: 0x060009E3 RID: 2531 RVA: 0x00029064 File Offset: 0x00027264
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(FireBigRocket.soundEffectString, base.gameObject);
			this.duration = FireBigRocket.baseDuration / this.attackSpeedStat;
			base.characterBody.AddSpreadBloom(1f);
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			string muzzleName = "MuzzleCenter";
			if (FireBigRocket.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireBigRocket.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				ProjectileManager.instance.FireProjectile(FireBigRocket.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * FireBigRocket.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00029140 File Offset: 0x00027340
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000B81 RID: 2945
		public static GameObject projectilePrefab;

		// Token: 0x04000B82 RID: 2946
		public static GameObject effectPrefab;

		// Token: 0x04000B83 RID: 2947
		public static string soundEffectString;

		// Token: 0x04000B84 RID: 2948
		public static float damageCoefficient;

		// Token: 0x04000B85 RID: 2949
		public static float force;

		// Token: 0x04000B86 RID: 2950
		public static float baseDuration = 2f;

		// Token: 0x04000B87 RID: 2951
		private float duration;
	}
}
