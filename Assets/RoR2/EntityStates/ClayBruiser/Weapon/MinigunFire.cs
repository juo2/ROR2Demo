using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ClayBruiser.Weapon
{
	// Token: 0x02000402 RID: 1026
	public class MinigunFire : MinigunState
	{
		// Token: 0x0600126F RID: 4719 RVA: 0x00052478 File Offset: 0x00050678
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.muzzleTransform && MinigunFire.muzzleVfxPrefab)
			{
				this.muzzleVfxTransform = UnityEngine.Object.Instantiate<GameObject>(MinigunFire.muzzleVfxPrefab, this.muzzleTransform).transform;
			}
			this.baseFireRate = 1f / MinigunFire.baseFireInterval;
			this.baseBulletsPerSecond = (float)MinigunFire.baseBulletCount * this.baseFireRate;
			this.critEndTime = Run.FixedTimeStamp.negativeInfinity;
			this.lastCritCheck = Run.FixedTimeStamp.negativeInfinity;
			Util.PlaySound(MinigunFire.startSound, base.gameObject);
			base.PlayCrossfade("Gesture, Additive", "FireMinigun", 0.2f);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0005251F File Offset: 0x0005071F
		private void UpdateCrits()
		{
			if (this.lastCritCheck.timeSince >= 1f)
			{
				this.lastCritCheck = Run.FixedTimeStamp.now;
				if (base.RollCrit())
				{
					this.critEndTime = Run.FixedTimeStamp.now + 2f;
				}
			}
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0005255C File Offset: 0x0005075C
		public override void OnExit()
		{
			Util.PlaySound(MinigunFire.endSound, base.gameObject);
			if (this.muzzleVfxTransform)
			{
				EntityState.Destroy(this.muzzleVfxTransform.gameObject);
				this.muzzleVfxTransform = null;
			}
			base.PlayCrossfade("Gesture, Additive", "BufferEmpty", 0.2f);
			base.OnExit();
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x000525B9 File Offset: 0x000507B9
		private void OnFireShared()
		{
			Util.PlaySound(MinigunFire.fireSound, base.gameObject);
			if (base.isAuthority)
			{
				this.OnFireAuthority();
			}
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x000525DC File Offset: 0x000507DC
		private void OnFireAuthority()
		{
			this.UpdateCrits();
			bool isCrit = !this.critEndTime.hasPassed;
			float damage = MinigunFire.baseDamagePerSecondCoefficient / this.baseBulletsPerSecond * this.damageStat;
			float force = MinigunFire.baseForcePerSecond / this.baseBulletsPerSecond;
			float procCoefficient = MinigunFire.baseProcCoefficientPerSecond / this.baseBulletsPerSecond;
			Ray aimRay = base.GetAimRay();
			new BulletAttack
			{
				bulletCount = (uint)MinigunFire.baseBulletCount,
				aimVector = aimRay.direction,
				origin = aimRay.origin,
				damage = damage,
				damageColorIndex = DamageColorIndex.Default,
				damageType = DamageType.Generic,
				falloffModel = BulletAttack.FalloffModel.None,
				maxDistance = MinigunFire.bulletMaxDistance,
				force = force,
				hitMask = LayerIndex.CommonMasks.bullet,
				minSpread = MinigunFire.bulletMinSpread,
				maxSpread = MinigunFire.bulletMaxSpread,
				isCrit = isCrit,
				owner = base.gameObject,
				muzzleName = MinigunState.muzzleName,
				smartCollision = false,
				procChainMask = default(ProcChainMask),
				procCoefficient = procCoefficient,
				radius = 0f,
				sniper = false,
				stopperMask = LayerIndex.CommonMasks.bullet,
				weapon = null,
				tracerEffectPrefab = MinigunFire.bulletTracerEffectPrefab,
				spreadPitchScale = 1f,
				spreadYawScale = 1f,
				queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
				hitEffectPrefab = MinigunFire.bulletHitEffectPrefab,
				HitEffectNormal = MinigunFire.bulletHitEffectNormal
			}.Fire();
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0005274C File Offset: 0x0005094C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.fireTimer -= Time.fixedDeltaTime;
			if (this.fireTimer <= 0f)
			{
				float num = MinigunFire.baseFireInterval / this.attackSpeedStat;
				this.fireTimer += num;
				this.OnFireShared();
			}
			if (base.isAuthority && !base.skillButtonState.down)
			{
				this.outer.SetNextState(new MinigunSpinDown());
				return;
			}
		}

		// Token: 0x040017B6 RID: 6070
		public static GameObject muzzleVfxPrefab;

		// Token: 0x040017B7 RID: 6071
		public static float baseFireInterval;

		// Token: 0x040017B8 RID: 6072
		public static int baseBulletCount;

		// Token: 0x040017B9 RID: 6073
		public static float baseDamagePerSecondCoefficient;

		// Token: 0x040017BA RID: 6074
		public static float baseForcePerSecond;

		// Token: 0x040017BB RID: 6075
		public static float baseProcCoefficientPerSecond;

		// Token: 0x040017BC RID: 6076
		public static float bulletMinSpread;

		// Token: 0x040017BD RID: 6077
		public static float bulletMaxSpread;

		// Token: 0x040017BE RID: 6078
		public static GameObject bulletTracerEffectPrefab;

		// Token: 0x040017BF RID: 6079
		public static GameObject bulletHitEffectPrefab;

		// Token: 0x040017C0 RID: 6080
		public static bool bulletHitEffectNormal;

		// Token: 0x040017C1 RID: 6081
		public static float bulletMaxDistance;

		// Token: 0x040017C2 RID: 6082
		public static string fireSound;

		// Token: 0x040017C3 RID: 6083
		public static string startSound;

		// Token: 0x040017C4 RID: 6084
		public static string endSound;

		// Token: 0x040017C5 RID: 6085
		private float fireTimer;

		// Token: 0x040017C6 RID: 6086
		private Transform muzzleVfxTransform;

		// Token: 0x040017C7 RID: 6087
		private float baseFireRate;

		// Token: 0x040017C8 RID: 6088
		private float baseBulletsPerSecond;

		// Token: 0x040017C9 RID: 6089
		private Run.FixedTimeStamp critEndTime;

		// Token: 0x040017CA RID: 6090
		private Run.FixedTimeStamp lastCritCheck;
	}
}
