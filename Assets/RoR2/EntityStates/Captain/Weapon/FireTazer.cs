using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Captain.Weapon
{
	// Token: 0x02000430 RID: 1072
	public class FireTazer : BaseState
	{
		// Token: 0x06001336 RID: 4918 RVA: 0x00055768 File Offset: 0x00053968
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireTazer.baseDuration / this.attackSpeedStat;
			this.durationUntilPriorityLowers = FireTazer.baseDurationUntilPriorityLowers / this.attackSpeedStat;
			this.delay = FireTazer.baseDelay / this.attackSpeedStat;
			base.StartAimMode(this.duration + 2f, false);
			if (FireTazer.chargeEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireTazer.chargeEffectPrefab, base.gameObject, FireTazer.targetMuzzle, false);
			}
			Util.PlayAttackSpeedSound(FireTazer.enterSoundString, base.gameObject, this.attackSpeedStat);
			base.PlayAnimation("Gesture, Additive", "FireTazer", "FireTazer.playbackRate", this.duration);
			base.PlayAnimation("Gesture, Override", "FireTazer", "FireTazer.playbackRate", this.duration);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00055834 File Offset: 0x00053A34
		private void Fire()
		{
			this.hasFired = true;
			Util.PlaySound(FireTazer.attackString, base.gameObject);
			base.AddRecoil(-1f * FireTazer.recoilAmplitude, -1.5f * FireTazer.recoilAmplitude, -0.25f * FireTazer.recoilAmplitude, 0.25f * FireTazer.recoilAmplitude);
			base.characterBody.AddSpreadBloom(FireTazer.bloom);
			Ray aimRay = base.GetAimRay();
			if (FireTazer.muzzleflashEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireTazer.muzzleflashEffectPrefab, base.gameObject, FireTazer.targetMuzzle, false);
			}
			if (base.isAuthority)
			{
				FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
				fireProjectileInfo.projectilePrefab = FireTazer.projectilePrefab;
				fireProjectileInfo.position = aimRay.origin;
				fireProjectileInfo.rotation = Util.QuaternionSafeLookRotation(aimRay.direction);
				fireProjectileInfo.owner = base.gameObject;
				fireProjectileInfo.damage = this.damageStat * FireTazer.damageCoefficient;
				fireProjectileInfo.force = FireTazer.force;
				fireProjectileInfo.crit = Util.CheckRoll(this.critStat, base.characterBody.master);
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0005595C File Offset: 0x00053B5C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.delay && !this.hasFired)
			{
				this.Fire();
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000559AD File Offset: 0x00053BAD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			if (base.fixedAge <= this.durationUntilPriorityLowers)
			{
				return InterruptPriority.PrioritySkill;
			}
			return InterruptPriority.Any;
		}

		// Token: 0x040018A2 RID: 6306
		public static GameObject projectilePrefab;

		// Token: 0x040018A3 RID: 6307
		public static GameObject muzzleflashEffectPrefab;

		// Token: 0x040018A4 RID: 6308
		public static GameObject chargeEffectPrefab;

		// Token: 0x040018A5 RID: 6309
		public static float baseDelay = 0.1f;

		// Token: 0x040018A6 RID: 6310
		public static float baseDuration = 2f;

		// Token: 0x040018A7 RID: 6311
		public static float baseDurationUntilPriorityLowers = 1f;

		// Token: 0x040018A8 RID: 6312
		public static float damageCoefficient = 1.2f;

		// Token: 0x040018A9 RID: 6313
		public static float force = 20f;

		// Token: 0x040018AA RID: 6314
		public static string enterSoundString;

		// Token: 0x040018AB RID: 6315
		public static string attackString;

		// Token: 0x040018AC RID: 6316
		public static float recoilAmplitude;

		// Token: 0x040018AD RID: 6317
		public static float bloom;

		// Token: 0x040018AE RID: 6318
		public static string targetMuzzle;

		// Token: 0x040018AF RID: 6319
		private float duration;

		// Token: 0x040018B0 RID: 6320
		private float delay;

		// Token: 0x040018B1 RID: 6321
		private float durationUntilPriorityLowers;

		// Token: 0x040018B2 RID: 6322
		private bool hasFired;
	}
}
