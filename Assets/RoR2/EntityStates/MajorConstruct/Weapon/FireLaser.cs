using System;
using EntityStates.EngiTurret.EngiTurretWeapon;
using RoR2;
using RoR2.Audio;
using UnityEngine;

namespace EntityStates.MajorConstruct.Weapon
{
	// Token: 0x02000293 RID: 659
	public class FireLaser : FireBeam
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x000304E0 File Offset: 0x0002E6E0
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation(this.animationLayerName, this.animationStateName, this.animationPlaybackParameterName, this.duration);
			if (this.loopSoundDef)
			{
				this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, this.loopSoundDef);
			}
			AimAnimator component = base.GetComponent<AimAnimator>();
			if (component)
			{
				this.animatorDirectionOverrideRequest = component.RequestDirectionOverride(new Func<Vector3>(this.GetAimDirection));
			}
			this.aimDirection = this.GetTargetDirection();
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00030568 File Offset: 0x0002E768
		public override void OnExit()
		{
			AimAnimator.DirectionOverrideRequest directionOverrideRequest = this.animatorDirectionOverrideRequest;
			if (directionOverrideRequest != null)
			{
				directionOverrideRequest.Dispose();
			}
			LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
			base.OnExit();
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0003058C File Offset: 0x0002E78C
		public override void FixedUpdate()
		{
			this.aimDirection = Vector3.RotateTowards(this.aimDirection, this.GetTargetDirection(), this.aimMaxSpeed * 0.017453292f * Time.fixedDeltaTime, float.PositiveInfinity);
			base.FixedUpdate();
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x000026ED File Offset: 0x000008ED
		public override void ModifyBullet(BulletAttack bulletAttack)
		{
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000305C2 File Offset: 0x0002E7C2
		protected override EntityState GetNextState()
		{
			return new TerminateLaser(base.GetBeamEndPoint());
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000305CF File Offset: 0x0002E7CF
		public override bool ShouldFireLaser()
		{
			return this.duration > base.fixedAge;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x000305DF File Offset: 0x0002E7DF
		private Vector3 GetAimDirection()
		{
			return this.aimDirection;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x000305E7 File Offset: 0x0002E7E7
		private Vector3 GetTargetDirection()
		{
			if (base.inputBank)
			{
				return base.inputBank.aimDirection;
			}
			return base.transform.forward;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0003060D File Offset: 0x0002E80D
		public override Ray GetLaserRay()
		{
			if (base.inputBank)
			{
				return new Ray(base.inputBank.aimOrigin, this.aimDirection);
			}
			return new Ray(base.transform.position, this.aimDirection);
		}

		// Token: 0x04000DBC RID: 3516
		[SerializeField]
		public float duration;

		// Token: 0x04000DBD RID: 3517
		[SerializeField]
		public float aimMaxSpeed;

		// Token: 0x04000DBE RID: 3518
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000DBF RID: 3519
		[SerializeField]
		public string animationStateName;

		// Token: 0x04000DC0 RID: 3520
		[SerializeField]
		public string animationPlaybackParameterName;

		// Token: 0x04000DC1 RID: 3521
		[SerializeField]
		public LoopSoundDef loopSoundDef;

		// Token: 0x04000DC2 RID: 3522
		private LoopSoundManager.SoundLoopPtr loopPtr;

		// Token: 0x04000DC3 RID: 3523
		private AimAnimator.DirectionOverrideRequest animatorDirectionOverrideRequest;

		// Token: 0x04000DC4 RID: 3524
		private Vector3 aimDirection;

		// Token: 0x04000DC5 RID: 3525
		private Vector3 aimVelocity;
	}
}
