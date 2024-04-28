using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004F9 RID: 1273
	public struct CharacterAnimParamAvailability
	{
		// Token: 0x0600172A RID: 5930 RVA: 0x000663AC File Offset: 0x000645AC
		public static CharacterAnimParamAvailability FromAnimator(Animator animator)
		{
			return new CharacterAnimParamAvailability
			{
				isMoving = Util.HasAnimationParameter(AnimationParameters.isMoving, animator),
				turnAngle = Util.HasAnimationParameter(AnimationParameters.turnAngle, animator),
				isGrounded = Util.HasAnimationParameter(AnimationParameters.isGrounded, animator),
				mainRootPlaybackRate = Util.HasAnimationParameter(AnimationParameters.mainRootPlaybackRate, animator),
				forwardSpeed = Util.HasAnimationParameter(AnimationParameters.forwardSpeed, animator),
				rightSpeed = Util.HasAnimationParameter(AnimationParameters.rightSpeed, animator),
				upSpeed = Util.HasAnimationParameter(AnimationParameters.upSpeed, animator),
				walkSpeed = Util.HasAnimationParameter(AnimationParameters.walkSpeed, animator),
				isSprinting = Util.HasAnimationParameter(AnimationParameters.isSprinting, animator)
			};
		}

		// Token: 0x04001CDA RID: 7386
		public bool isMoving;

		// Token: 0x04001CDB RID: 7387
		public bool turnAngle;

		// Token: 0x04001CDC RID: 7388
		public bool isGrounded;

		// Token: 0x04001CDD RID: 7389
		public bool mainRootPlaybackRate;

		// Token: 0x04001CDE RID: 7390
		public bool forwardSpeed;

		// Token: 0x04001CDF RID: 7391
		public bool rightSpeed;

		// Token: 0x04001CE0 RID: 7392
		public bool upSpeed;

		// Token: 0x04001CE1 RID: 7393
		public bool walkSpeed;

		// Token: 0x04001CE2 RID: 7394
		public bool isSprinting;
	}
}
