using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Bandit2.Weapon
{
	// Token: 0x02000478 RID: 1144
	public class Bandit2FireShiv : BaseSkillState
	{
		// Token: 0x06001472 RID: 5234 RVA: 0x0005B150 File Offset: 0x00059350
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.delayBetweenShivs = Bandit2FireShiv.baseDelayBetweenShivs / this.attackSpeedStat;
			base.PlayAnimation("Gesture, Additive", "SlashBlade", "SlashBlade.playbackRate", this.duration);
			base.StartAimMode(2f, false);
			if (base.characterMotor)
			{
				base.characterMotor.velocity = new Vector3(base.characterMotor.velocity.x, Mathf.Max(base.characterMotor.velocity.y, Bandit2FireShiv.shortHopVelocity), base.characterMotor.velocity.z);
			}
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0005B208 File Offset: 0x00059408
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.countdownSinceLastShiv -= Time.fixedDeltaTime;
			if (this.shivCount < this.maxShivCount && this.countdownSinceLastShiv <= 0f)
			{
				this.shivCount++;
				this.countdownSinceLastShiv += this.delayBetweenShivs;
				this.FireShiv();
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0005B294 File Offset: 0x00059494
		private void FireShiv()
		{
			if (Bandit2FireShiv.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(Bandit2FireShiv.muzzleEffectPrefab, base.gameObject, Bandit2FireShiv.muzzleString, false);
			}
			if (base.isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				if (this.projectilePrefab != null)
				{
					FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
					{
						projectilePrefab = this.projectilePrefab,
						position = aimRay.origin,
						rotation = Util.QuaternionSafeLookRotation(aimRay.direction),
						owner = base.gameObject,
						damage = this.damageStat * this.damageCoefficient,
						force = this.force,
						crit = base.RollCrit(),
						damageTypeOverride = new DamageType?(DamageType.SuperBleedOnCrit)
					};
					ProjectileManager.instance.FireProjectile(fireProjectileInfo);
				}
			}
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001A35 RID: 6709
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x04001A36 RID: 6710
		[SerializeField]
		public float baseDuration;

		// Token: 0x04001A37 RID: 6711
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x04001A38 RID: 6712
		[SerializeField]
		public float force;

		// Token: 0x04001A39 RID: 6713
		[SerializeField]
		public int maxShivCount;

		// Token: 0x04001A3A RID: 6714
		public static float baseDelayBetweenShivs;

		// Token: 0x04001A3B RID: 6715
		public static float shortHopVelocity;

		// Token: 0x04001A3C RID: 6716
		public static GameObject muzzleEffectPrefab;

		// Token: 0x04001A3D RID: 6717
		public static string muzzleString;

		// Token: 0x04001A3E RID: 6718
		private float duration;

		// Token: 0x04001A3F RID: 6719
		private float delayBetweenShivs;

		// Token: 0x04001A40 RID: 6720
		private float countdownSinceLastShiv;

		// Token: 0x04001A41 RID: 6721
		private int shivCount;
	}
}
