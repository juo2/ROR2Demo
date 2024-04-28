using System;
using UnityEngine;

namespace EntityStates.Merc.Weapon
{
	// Token: 0x02000284 RID: 644
	public class ThrowEvisProjectile : GenericProjectileBaseState
	{
		// Token: 0x06000B63 RID: 2915 RVA: 0x0002F8F7 File Offset: 0x0002DAF7
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.characterMotor)
			{
				base.characterMotor.velocity.y = Mathf.Max(base.characterMotor.velocity.y, ThrowEvisProjectile.shortHopVelocity);
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002F938 File Offset: 0x0002DB38
		protected override void PlayAnimation(float duration)
		{
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				bool @bool = modelAnimator.GetBool("isMoving");
				bool bool2 = modelAnimator.GetBool("isGrounded");
				if (@bool || !bool2)
				{
					base.PlayAnimation("Gesture, Additive", "GroundLight3", "GroundLight.playbackRate", duration);
					base.PlayAnimation("Gesture, Override", "GroundLight3", "GroundLight.playbackRate", duration);
					return;
				}
				base.PlayAnimation("FullBody, Override", "GroundLight3", "GroundLight.playbackRate", duration);
			}
		}

		// Token: 0x04000D5D RID: 3421
		public static float shortHopVelocity;
	}
}
