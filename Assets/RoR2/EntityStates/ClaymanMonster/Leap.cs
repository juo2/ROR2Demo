using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ClaymanMonster
{
	// Token: 0x020003F8 RID: 1016
	public class Leap : BaseState
	{
		// Token: 0x06001242 RID: 4674 RVA: 0x00051424 File Offset: 0x0004F624
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			Util.PlaySound(Leap.leapSoundString, base.gameObject);
			if (base.characterMotor)
			{
				base.characterMotor.velocity.y = Leap.verticalJumpSpeed;
			}
			this.forwardDirection = Vector3.ProjectOnPlane(base.inputBank.aimDirection, Vector3.up);
			base.characterDirection.moveVector = this.forwardDirection;
			base.PlayCrossfade("Body", "LeapAirLoop", 0.15f);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x000514B8 File Offset: 0x0004F6B8
		public override void FixedUpdate()
		{
			this.stopwatch += Time.fixedDeltaTime;
			this.animator.SetFloat("Leap.cycle", Mathf.Clamp01(Util.Remap(base.characterMotor.velocity.y, -Leap.verticalJumpSpeed, Leap.verticalJumpSpeed, 1f, 0f)));
			Vector3 velocity = this.forwardDirection * base.characterBody.moveSpeed * Leap.horizontalJumpSpeedCoefficient;
			velocity.y = base.characterMotor.velocity.y;
			base.characterMotor.velocity = velocity;
			base.FixedUpdate();
			if (base.characterMotor.isGrounded && this.stopwatch > Leap.minimumDuration && !this.playedImpact)
			{
				this.playedImpact = true;
				int layerIndex = this.animator.GetLayerIndex("Impact");
				if (layerIndex >= 0)
				{
					this.animator.SetLayerWeight(layerIndex, 1.5f);
					this.animator.PlayInFixedTime("LightImpact", layerIndex, 0f);
				}
				if (base.isAuthority)
				{
					this.outer.SetNextStateToMain();
					return;
				}
			}
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x000515D9 File Offset: 0x0004F7D9
		public override void OnExit()
		{
			this.PlayAnimation("Body", "Idle");
			base.OnExit();
		}

		// Token: 0x0400175F RID: 5983
		public static string leapSoundString;

		// Token: 0x04001760 RID: 5984
		public static float minimumDuration;

		// Token: 0x04001761 RID: 5985
		public static float verticalJumpSpeed;

		// Token: 0x04001762 RID: 5986
		public static float horizontalJumpSpeedCoefficient;

		// Token: 0x04001763 RID: 5987
		private Vector3 forwardDirection;

		// Token: 0x04001764 RID: 5988
		private Animator animator;

		// Token: 0x04001765 RID: 5989
		private float stopwatch;

		// Token: 0x04001766 RID: 5990
		private bool playedImpact;
	}
}
