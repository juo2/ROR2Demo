using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003E9 RID: 1001
	public class FireBarrage : BaseState
	{
		// Token: 0x060011F9 RID: 4601 RVA: 0x0004FBEC File Offset: 0x0004DDEC
		public override void OnEnter()
		{
			base.OnEnter();
			base.characterBody.SetSpreadBloom(0.2f, false);
			this.duration = FireBarrage.totalDuration;
			this.durationBetweenShots = FireBarrage.baseDurationBetweenShots / this.attackSpeedStat;
			this.bulletCount = (int)((float)FireBarrage.baseBulletCount * this.attackSpeedStat);
			this.modelAnimator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			base.PlayCrossfade("Gesture, Additive", "FireBarrage", "FireBarrage.playbackRate", this.duration, 0.2f);
			base.PlayCrossfade("Gesture, Override", "FireBarrage", "FireBarrage.playbackRate", this.duration, 0.2f);
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(2f);
			}
			this.FireBullet();
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0004FCBC File Offset: 0x0004DEBC
		private void FireBullet()
		{
			Ray aimRay = base.GetAimRay();
			string muzzleName = "MuzzleRight";
			if (this.modelAnimator)
			{
				if (FireBarrage.effectPrefab)
				{
					EffectManager.SimpleMuzzleFlash(FireBarrage.effectPrefab, base.gameObject, muzzleName, false);
				}
				this.PlayAnimation("Gesture Additive, Right", "FirePistol, Right");
			}
			base.AddRecoil(-0.8f * FireBarrage.recoilAmplitude, -1f * FireBarrage.recoilAmplitude, -0.1f * FireBarrage.recoilAmplitude, 0.15f * FireBarrage.recoilAmplitude);
			if (base.isAuthority)
			{
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = aimRay.origin,
					aimVector = aimRay.direction,
					minSpread = FireBarrage.minSpread,
					maxSpread = FireBarrage.maxSpread,
					bulletCount = 1U,
					damage = FireBarrage.damageCoefficient * this.damageStat,
					force = FireBarrage.force,
					tracerEffectPrefab = FireBarrage.tracerEffectPrefab,
					muzzleName = muzzleName,
					hitEffectPrefab = FireBarrage.hitEffectPrefab,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master),
					radius = FireBarrage.bulletRadius,
					smartCollision = true,
					damageType = DamageType.Stun1s
				}.Fire();
			}
			base.characterBody.AddSpreadBloom(FireBarrage.spreadBloomValue);
			this.totalBulletsFired++;
			Util.PlaySound(FireBarrage.fireBarrageSoundString, base.gameObject);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0004FE48 File Offset: 0x0004E048
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatchBetweenShots += Time.fixedDeltaTime;
			if (this.stopwatchBetweenShots >= this.durationBetweenShots && this.totalBulletsFired < this.bulletCount)
			{
				this.stopwatchBetweenShots -= this.durationBetweenShots;
				this.FireBullet();
			}
			if (base.fixedAge >= this.duration && this.totalBulletsFired == this.bulletCount && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040016DD RID: 5853
		public static GameObject effectPrefab;

		// Token: 0x040016DE RID: 5854
		public static GameObject hitEffectPrefab;

		// Token: 0x040016DF RID: 5855
		public static GameObject tracerEffectPrefab;

		// Token: 0x040016E0 RID: 5856
		public static float damageCoefficient;

		// Token: 0x040016E1 RID: 5857
		public static float force;

		// Token: 0x040016E2 RID: 5858
		public static float minSpread;

		// Token: 0x040016E3 RID: 5859
		public static float maxSpread;

		// Token: 0x040016E4 RID: 5860
		public static float baseDurationBetweenShots = 1f;

		// Token: 0x040016E5 RID: 5861
		public static float totalDuration = 2f;

		// Token: 0x040016E6 RID: 5862
		public static float bulletRadius = 1.5f;

		// Token: 0x040016E7 RID: 5863
		public static int baseBulletCount = 1;

		// Token: 0x040016E8 RID: 5864
		public static string fireBarrageSoundString;

		// Token: 0x040016E9 RID: 5865
		public static float recoilAmplitude;

		// Token: 0x040016EA RID: 5866
		public static float spreadBloomValue;

		// Token: 0x040016EB RID: 5867
		private int totalBulletsFired;

		// Token: 0x040016EC RID: 5868
		private int bulletCount;

		// Token: 0x040016ED RID: 5869
		public float stopwatchBetweenShots;

		// Token: 0x040016EE RID: 5870
		private Animator modelAnimator;

		// Token: 0x040016EF RID: 5871
		private Transform modelTransform;

		// Token: 0x040016F0 RID: 5872
		private float duration;

		// Token: 0x040016F1 RID: 5873
		private float durationBetweenShots;
	}
}
