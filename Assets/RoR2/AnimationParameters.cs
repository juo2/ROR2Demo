using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004C7 RID: 1223
	public static class AnimationParameters
	{
		// Token: 0x04001BE6 RID: 7142
		public static readonly int isMoving = Animator.StringToHash("isMoving");

		// Token: 0x04001BE7 RID: 7143
		public static readonly int turnAngle = Animator.StringToHash("turnAngle");

		// Token: 0x04001BE8 RID: 7144
		public static readonly int isGrounded = Animator.StringToHash("isGrounded");

		// Token: 0x04001BE9 RID: 7145
		public static readonly int mainRootPlaybackRate = Animator.StringToHash("mainRootPlaybackRate");

		// Token: 0x04001BEA RID: 7146
		public static readonly int forwardSpeed = Animator.StringToHash("forwardSpeed");

		// Token: 0x04001BEB RID: 7147
		public static readonly int rightSpeed = Animator.StringToHash("rightSpeed");

		// Token: 0x04001BEC RID: 7148
		public static readonly int upSpeed = Animator.StringToHash("upSpeed");

		// Token: 0x04001BED RID: 7149
		public static readonly int walkSpeed = Animator.StringToHash("walkSpeed");

		// Token: 0x04001BEE RID: 7150
		public static readonly int isSprinting = Animator.StringToHash("isSprinting");

		// Token: 0x04001BEF RID: 7151
		public static readonly int aimWeight = Animator.StringToHash("aimWeight");
	}
}
