using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Paladin.PaladinWeapon
{
	// Token: 0x02000231 RID: 561
	public class FireRocket : BaseState
	{
		// Token: 0x060009E9 RID: 2537 RVA: 0x00029178 File Offset: 0x00027378
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(FireRocket.soundEffectString, base.gameObject);
			this.duration = FireRocket.baseDuration / this.attackSpeedStat;
			base.characterBody.AddSpreadBloom(0.3f);
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			string muzzleName = "MuzzleCenter";
			if (FireRocket.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireRocket.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				ProjectileManager.instance.FireProjectile(FireRocket.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * FireRocket.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00029254 File Offset: 0x00027454
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000B88 RID: 2952
		public static GameObject projectilePrefab;

		// Token: 0x04000B89 RID: 2953
		public static GameObject effectPrefab;

		// Token: 0x04000B8A RID: 2954
		public static string soundEffectString;

		// Token: 0x04000B8B RID: 2955
		public static float damageCoefficient;

		// Token: 0x04000B8C RID: 2956
		public static float force;

		// Token: 0x04000B8D RID: 2957
		public static float baseDuration = 2f;

		// Token: 0x04000B8E RID: 2958
		private float duration;

		// Token: 0x04000B8F RID: 2959
		public int bulletCountCurrent = 1;
	}
}
