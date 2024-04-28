using System;

namespace EntityStates.Toolbot
{
	// Token: 0x020001A0 RID: 416
	public class DroneProjectilePrepHover : BaseState
	{
		// Token: 0x0600077A RID: 1914 RVA: 0x00020172 File Offset: 0x0001E372
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.age >= DroneProjectilePrepHover.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x0400090D RID: 2317
		public static float duration;
	}
}
