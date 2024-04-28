using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Loader
{
	// Token: 0x020002C9 RID: 713
	public class BaseSwingChargedFist : LoaderMeleeAttack
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x00035425 File Offset: 0x00033625
		// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x0003542D File Offset: 0x0003362D
		public float punchSpeed { get; private set; }

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00035438 File Offset: 0x00033638
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				base.characterMotor.Motor.ForceUnground();
				base.characterMotor.disableAirControlUntilCollision |= BaseSwingChargedFist.disableAirControlUntilCollision;
				this.punchVelocity = BaseSwingChargedFist.CalculateLungeVelocity(base.characterMotor.velocity, base.GetAimRay().direction, this.charge, this.minLungeSpeed, this.maxLungeSpeed);
				base.characterMotor.velocity = this.punchVelocity;
				base.characterDirection.forward = base.characterMotor.velocity.normalized;
				this.punchSpeed = base.characterMotor.velocity.magnitude;
				this.bonusDamage = this.punchSpeed * (BaseSwingChargedFist.velocityDamageCoefficient * this.damageStat);
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0003550E File Offset: 0x0003370E
		protected override float CalcDuration()
		{
			return Mathf.Lerp(this.minDuration, this.maxDuration, this.charge);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00035527 File Offset: 0x00033727
		protected override void PlayAnimation()
		{
			base.PlayAnimation();
			base.PlayAnimation("FullBody, Override", "ChargePunch", "ChargePunch.playbackRate", this.duration);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0003554A File Offset: 0x0003374A
		protected override void AuthorityFixedUpdate()
		{
			base.AuthorityFixedUpdate();
			if (!base.authorityInHitPause)
			{
				base.characterMotor.velocity = this.punchVelocity;
				base.characterDirection.forward = this.punchVelocity;
				base.characterBody.isSprinting = true;
			}
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00035588 File Offset: 0x00033788
		protected override void AuthorityModifyOverlapAttack(OverlapAttack overlapAttack)
		{
			base.AuthorityModifyOverlapAttack(overlapAttack);
			overlapAttack.damage = this.damageCoefficient * this.damageStat + this.bonusDamage;
			overlapAttack.forceVector = base.characterMotor.velocity + base.GetAimRay().direction * Mathf.Lerp(this.minPunchForce, this.maxPunchForce, this.charge);
			if (base.fixedAge + Time.fixedDeltaTime >= this.duration)
			{
				HitBoxGroup hitBoxGroup = base.FindHitBoxGroup("PunchLollypop");
				if (hitBoxGroup)
				{
					this.hitBoxGroup = hitBoxGroup;
					overlapAttack.hitBoxGroup = hitBoxGroup;
				}
			}
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0003562C File Offset: 0x0003382C
		protected override void OnMeleeHitAuthority()
		{
			base.OnMeleeHitAuthority();
			Action<BaseSwingChargedFist> action = BaseSwingChargedFist.onHitAuthorityGlobal;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000CA9 RID: 3241 RVA: 0x00035644 File Offset: 0x00033844
		// (remove) Token: 0x06000CAA RID: 3242 RVA: 0x00035678 File Offset: 0x00033878
		public static event Action<BaseSwingChargedFist> onHitAuthorityGlobal;

		// Token: 0x06000CAB RID: 3243 RVA: 0x000356AB File Offset: 0x000338AB
		public override void OnExit()
		{
			base.OnExit();
			base.characterMotor.velocity *= BaseSwingChargedFist.speedCoefficientOnExit;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000356CE File Offset: 0x000338CE
		public static Vector3 CalculateLungeVelocity(Vector3 currentVelocity, Vector3 aimDirection, float charge, float minLungeSpeed, float maxLungeSpeed)
		{
			currentVelocity = ((Vector3.Dot(currentVelocity, aimDirection) < 0f) ? Vector3.zero : Vector3.Project(currentVelocity, aimDirection));
			return currentVelocity + aimDirection * Mathf.Lerp(minLungeSpeed, maxLungeSpeed, charge);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000F72 RID: 3954
		public float charge;

		// Token: 0x04000F73 RID: 3955
		[SerializeField]
		public float minLungeSpeed;

		// Token: 0x04000F74 RID: 3956
		[SerializeField]
		public float maxLungeSpeed;

		// Token: 0x04000F75 RID: 3957
		[SerializeField]
		public float minPunchForce;

		// Token: 0x04000F76 RID: 3958
		[SerializeField]
		public float maxPunchForce;

		// Token: 0x04000F77 RID: 3959
		[SerializeField]
		public float minDuration;

		// Token: 0x04000F78 RID: 3960
		[SerializeField]
		public float maxDuration;

		// Token: 0x04000F79 RID: 3961
		public static bool disableAirControlUntilCollision;

		// Token: 0x04000F7A RID: 3962
		public static float speedCoefficientOnExit;

		// Token: 0x04000F7B RID: 3963
		public static float velocityDamageCoefficient;

		// Token: 0x04000F7C RID: 3964
		protected Vector3 punchVelocity;

		// Token: 0x04000F7D RID: 3965
		private float bonusDamage;
	}
}
