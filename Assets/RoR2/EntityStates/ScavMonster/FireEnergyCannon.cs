using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.ScavMonster
{
	// Token: 0x020001D7 RID: 471
	public class FireEnergyCannon : EnergyCannonState
	{
		// Token: 0x0600086C RID: 2156 RVA: 0x00023920 File Offset: 0x00021B20
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireEnergyCannon.baseDuration / this.attackSpeedStat;
			this.refireDuration = FireEnergyCannon.baseRefireDuration / this.attackSpeedStat;
			Util.PlayAttackSpeedSound(FireEnergyCannon.sound, base.gameObject, this.attackSpeedStat);
			base.PlayCrossfade("Body", "FireEnergyCannon", "FireEnergyCannon.playbackRate", this.duration, 0.1f);
			base.AddRecoil(-2f * FireEnergyCannon.recoilAmplitude, -3f * FireEnergyCannon.recoilAmplitude, -1f * FireEnergyCannon.recoilAmplitude, 1f * FireEnergyCannon.recoilAmplitude);
			if (FireEnergyCannon.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireEnergyCannon.effectPrefab, base.gameObject, EnergyCannonState.muzzleName, false);
			}
			if (base.isAuthority)
			{
				float num = (float)((this.currentRefire % 2 == 0) ? 1 : -1);
				float num2 = Mathf.Ceil((float)this.currentRefire / 2f) * FireEnergyCannon.projectileYawBonusPerRefire;
				for (int i = 0; i < FireEnergyCannon.projectileCount; i++)
				{
					Ray aimRay = base.GetAimRay();
					aimRay.direction = Util.ApplySpread(aimRay.direction, FireEnergyCannon.minSpread, FireEnergyCannon.maxSpread, 1f, 1f, num * num2, FireEnergyCannon.projectilePitchBonus);
					ProjectileManager.instance.FireProjectile(FireEnergyCannon.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * FireEnergyCannon.damageCoefficient, FireEnergyCannon.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
				}
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00023AB8 File Offset: 0x00021CB8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.refireDuration && this.currentRefire + 1 < FireEnergyCannon.maxRefireCount && base.isAuthority)
			{
				FireEnergyCannon fireEnergyCannon = new FireEnergyCannon();
				fireEnergyCannon.currentRefire = this.currentRefire + 1;
				this.outer.SetNextState(fireEnergyCannon);
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x040009DB RID: 2523
		public static float baseDuration;

		// Token: 0x040009DC RID: 2524
		public static float baseRefireDuration;

		// Token: 0x040009DD RID: 2525
		public static string sound;

		// Token: 0x040009DE RID: 2526
		public static GameObject effectPrefab;

		// Token: 0x040009DF RID: 2527
		public static GameObject projectilePrefab;

		// Token: 0x040009E0 RID: 2528
		public static float damageCoefficient;

		// Token: 0x040009E1 RID: 2529
		public static float force;

		// Token: 0x040009E2 RID: 2530
		public static float minSpread;

		// Token: 0x040009E3 RID: 2531
		public static float maxSpread;

		// Token: 0x040009E4 RID: 2532
		public static float recoilAmplitude = 1f;

		// Token: 0x040009E5 RID: 2533
		public static float projectilePitchBonus;

		// Token: 0x040009E6 RID: 2534
		public static float projectileYawBonusPerRefire;

		// Token: 0x040009E7 RID: 2535
		public static int projectileCount;

		// Token: 0x040009E8 RID: 2536
		public static int maxRefireCount;

		// Token: 0x040009E9 RID: 2537
		public int currentRefire;

		// Token: 0x040009EA RID: 2538
		private float duration;

		// Token: 0x040009EB RID: 2539
		private float refireDuration;
	}
}
