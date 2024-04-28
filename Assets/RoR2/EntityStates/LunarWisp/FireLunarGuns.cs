using System;
using RoR2;
using UnityEngine;

namespace EntityStates.LunarWisp
{
	// Token: 0x020002AD RID: 685
	public class FireLunarGuns : BaseState
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0001B6BB File Offset: 0x000198BB
		protected ref InputBankTest.ButtonState skillButtonState
		{
			get
			{
				return ref base.inputBank.skill1;
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00032EE0 File Offset: 0x000310E0
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireLunarGuns.baseDuration / this.attackSpeedStat;
			this.muzzleTransformOne = base.FindModelChild(this.muzzleNameOne);
			this.muzzleTransformTwo = base.FindModelChild(this.muzzleNameTwo);
			if (this.muzzleTransformOne && this.muzzleTransformTwo && FireLunarGuns.muzzleVfxPrefab)
			{
				this.muzzleVFXInstanceOne = UnityEngine.Object.Instantiate<GameObject>(FireLunarGuns.muzzleVfxPrefab, this.muzzleTransformOne.position, this.muzzleTransformOne.rotation);
				this.muzzleVFXInstanceOne.transform.parent = this.muzzleTransformOne;
				this.muzzleVFXInstanceTwo = UnityEngine.Object.Instantiate<GameObject>(FireLunarGuns.muzzleVfxPrefab, this.muzzleTransformTwo.position, this.muzzleTransformTwo.rotation);
				this.muzzleVFXInstanceTwo.transform.parent = this.muzzleTransformTwo;
			}
			this.baseFireRate = 1f / FireLunarGuns.baseFireInterval;
			this.baseBulletsPerSecond = (float)FireLunarGuns.baseBulletCount * this.baseFireRate;
			this.critEndTime = Run.FixedTimeStamp.negativeInfinity;
			this.lastCritCheck = Run.FixedTimeStamp.negativeInfinity;
			this.windLoopSoundID = Util.PlaySound(FireLunarGuns.windLoopSound, base.gameObject);
			this.shootLoopSoundID = Util.PlaySound(FireLunarGuns.shootLoopSound, base.gameObject);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00033033 File Offset: 0x00031233
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

		// Token: 0x06000C23 RID: 3107 RVA: 0x00033070 File Offset: 0x00031270
		public override void OnExit()
		{
			Util.PlaySound(FireLunarGuns.windDownSound, base.gameObject);
			if (this.muzzleVFXInstanceOne)
			{
				EntityState.Destroy(this.muzzleVFXInstanceOne.gameObject);
				this.muzzleVFXInstanceOne = null;
			}
			if (this.muzzleVFXInstanceTwo)
			{
				EntityState.Destroy(this.muzzleVFXInstanceTwo.gameObject);
				this.muzzleVFXInstanceTwo = null;
			}
			AkSoundEngine.StopPlayingID(this.windLoopSoundID);
			AkSoundEngine.StopPlayingID(this.shootLoopSoundID);
			base.PlayCrossfade("Gesture", "MinigunSpinDown", 0.2f);
			base.OnExit();
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00033107 File Offset: 0x00031307
		private void OnFireShared()
		{
			Util.PlaySound(FireLunarGuns.fireSound, base.gameObject);
			if (base.isAuthority)
			{
				this.OnFireAuthority();
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00033128 File Offset: 0x00031328
		private void OnFireAuthority()
		{
			this.UpdateCrits();
			bool isCrit = !this.critEndTime.hasPassed;
			float damage = FireLunarGuns.baseDamagePerSecondCoefficient / this.baseBulletsPerSecond * this.damageStat;
			float force = FireLunarGuns.baseForcePerSecond / this.baseBulletsPerSecond;
			float procCoefficient = FireLunarGuns.baseProcCoefficientPerSecond / this.baseBulletsPerSecond;
			base.StartAimMode(0.5f, false);
			Ray aimRay = base.GetAimRay();
			new BulletAttack
			{
				bulletCount = (uint)(FireLunarGuns.baseBulletCount / 2),
				aimVector = aimRay.direction,
				origin = aimRay.origin,
				damage = damage,
				damageColorIndex = DamageColorIndex.Default,
				damageType = DamageType.Generic,
				falloffModel = BulletAttack.FalloffModel.None,
				maxDistance = FireLunarGuns.bulletMaxDistance,
				force = force,
				hitMask = LayerIndex.CommonMasks.bullet,
				minSpread = FireLunarGuns.bulletMinSpread,
				maxSpread = FireLunarGuns.bulletMaxSpread,
				isCrit = isCrit,
				owner = base.gameObject,
				muzzleName = this.muzzleNameOne,
				smartCollision = false,
				procChainMask = default(ProcChainMask),
				procCoefficient = procCoefficient,
				radius = 0f,
				sniper = false,
				stopperMask = LayerIndex.CommonMasks.bullet,
				weapon = null,
				tracerEffectPrefab = FireLunarGuns.bulletTracerEffectPrefab,
				spreadPitchScale = 1f,
				spreadYawScale = 1f,
				queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
				hitEffectPrefab = FireLunarGuns.bulletHitEffectPrefab,
				HitEffectNormal = FireLunarGuns.bulletHitEffectNormal
			}.Fire();
			new BulletAttack
			{
				bulletCount = (uint)(FireLunarGuns.baseBulletCount / 2),
				aimVector = aimRay.direction,
				origin = aimRay.origin,
				damage = damage,
				damageColorIndex = DamageColorIndex.Default,
				damageType = DamageType.Generic,
				falloffModel = BulletAttack.FalloffModel.None,
				maxDistance = FireLunarGuns.bulletMaxDistance,
				force = force,
				hitMask = LayerIndex.CommonMasks.bullet,
				minSpread = FireLunarGuns.bulletMinSpread,
				maxSpread = FireLunarGuns.bulletMaxSpread,
				isCrit = isCrit,
				owner = base.gameObject,
				muzzleName = this.muzzleNameTwo,
				smartCollision = false,
				procChainMask = default(ProcChainMask),
				procCoefficient = procCoefficient,
				radius = 0f,
				sniper = false,
				stopperMask = LayerIndex.CommonMasks.bullet,
				weapon = null,
				tracerEffectPrefab = FireLunarGuns.bulletTracerEffectPrefab,
				spreadPitchScale = 1f,
				spreadYawScale = 1f,
				queryTriggerInteraction = QueryTriggerInteraction.UseGlobal,
				hitEffectPrefab = FireLunarGuns.bulletHitEffectPrefab,
				HitEffectNormal = FireLunarGuns.bulletHitEffectNormal
			}.Fire();
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x000333C4 File Offset: 0x000315C4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.fireTimer -= Time.fixedDeltaTime;
			if (this.fireTimer <= 0f)
			{
				float num = FireLunarGuns.baseFireInterval / this.attackSpeedStat;
				this.fireTimer += num;
				this.OnFireShared();
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000EAF RID: 3759
		public static GameObject muzzleVfxPrefab;

		// Token: 0x04000EB0 RID: 3760
		public static float baseDuration;

		// Token: 0x04000EB1 RID: 3761
		private float duration;

		// Token: 0x04000EB2 RID: 3762
		public static float baseFireInterval;

		// Token: 0x04000EB3 RID: 3763
		public static int baseBulletCount;

		// Token: 0x04000EB4 RID: 3764
		public static float baseDamagePerSecondCoefficient;

		// Token: 0x04000EB5 RID: 3765
		public static float baseForcePerSecond;

		// Token: 0x04000EB6 RID: 3766
		public static float baseProcCoefficientPerSecond;

		// Token: 0x04000EB7 RID: 3767
		public static float bulletMinSpread;

		// Token: 0x04000EB8 RID: 3768
		public static float bulletMaxSpread;

		// Token: 0x04000EB9 RID: 3769
		public static GameObject bulletTracerEffectPrefab;

		// Token: 0x04000EBA RID: 3770
		public static GameObject bulletHitEffectPrefab;

		// Token: 0x04000EBB RID: 3771
		public static bool bulletHitEffectNormal;

		// Token: 0x04000EBC RID: 3772
		public static float bulletMaxDistance;

		// Token: 0x04000EBD RID: 3773
		public static string fireSound;

		// Token: 0x04000EBE RID: 3774
		public static string windLoopSound;

		// Token: 0x04000EBF RID: 3775
		public static string windDownSound;

		// Token: 0x04000EC0 RID: 3776
		public static string shootLoopSound;

		// Token: 0x04000EC1 RID: 3777
		private uint windLoopSoundID;

		// Token: 0x04000EC2 RID: 3778
		private uint shootLoopSoundID;

		// Token: 0x04000EC3 RID: 3779
		private Transform muzzleTransform;

		// Token: 0x04000EC4 RID: 3780
		public Transform muzzleTransformOne;

		// Token: 0x04000EC5 RID: 3781
		public Transform muzzleTransformTwo;

		// Token: 0x04000EC6 RID: 3782
		public string muzzleNameOne;

		// Token: 0x04000EC7 RID: 3783
		public string muzzleNameTwo;

		// Token: 0x04000EC8 RID: 3784
		private GameObject muzzleVFXInstanceOne;

		// Token: 0x04000EC9 RID: 3785
		private GameObject muzzleVFXInstanceTwo;

		// Token: 0x04000ECA RID: 3786
		private float fireTimer;

		// Token: 0x04000ECB RID: 3787
		private float baseFireRate;

		// Token: 0x04000ECC RID: 3788
		private float baseBulletsPerSecond;

		// Token: 0x04000ECD RID: 3789
		private Run.FixedTimeStamp critEndTime;

		// Token: 0x04000ECE RID: 3790
		private Run.FixedTimeStamp lastCritCheck;
	}
}
