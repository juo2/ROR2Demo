using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.ImpMonster
{
	// Token: 0x02000311 RID: 785
	public class FireSpines : BaseState
	{
		// Token: 0x06000E04 RID: 3588 RVA: 0x0003BEDF File Offset: 0x0003A0DF
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireSpines.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("Gesture", "FireSpines", "FireSpines.playbackRate", this.duration);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0003BF14 File Offset: 0x0003A114
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.spineTimer += Time.fixedDeltaTime;
			if (this.spineTimer >= FireSpines.durationBetweenThrows / this.attackSpeedStat && this.spineCount < FireSpines.spineCountMax)
			{
				this.spineCount++;
				Ray aimRay = base.GetAimRay();
				string muzzleName = "MuzzleMouth";
				this.spineTimer -= FireSpines.durationBetweenThrows / this.attackSpeedStat;
				if (FireSpines.effectPrefab)
				{
					EffectManager.SimpleMuzzleFlash(FireSpines.effectPrefab, base.gameObject, muzzleName, false);
				}
				if (base.isAuthority)
				{
					ProjectileManager.instance.FireProjectile(FireSpines.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * FireSpines.damageCoefficient, FireSpines.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
				}
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x0400116B RID: 4459
		public static GameObject projectilePrefab;

		// Token: 0x0400116C RID: 4460
		public static GameObject effectPrefab;

		// Token: 0x0400116D RID: 4461
		public static float baseDuration = 2f;

		// Token: 0x0400116E RID: 4462
		public static float durationBetweenThrows = 0.1f;

		// Token: 0x0400116F RID: 4463
		public static int spineCountMax = 3;

		// Token: 0x04001170 RID: 4464
		public static float damageCoefficient = 1.2f;

		// Token: 0x04001171 RID: 4465
		public static float force = 20f;

		// Token: 0x04001172 RID: 4466
		private int spineCount;

		// Token: 0x04001173 RID: 4467
		private float spineTimer;

		// Token: 0x04001174 RID: 4468
		private float duration;
	}
}
