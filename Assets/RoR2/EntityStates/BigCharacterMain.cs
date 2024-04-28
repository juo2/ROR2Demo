using System;

namespace EntityStates
{
	// Token: 0x020000B6 RID: 182
	public class BigCharacterMain : GenericCharacterMain
	{
		// Token: 0x06000311 RID: 785 RVA: 0x0000CC78 File Offset: 0x0000AE78
		public override void ProcessJump()
		{
			if (base.characterMotor.jumpCount > base.characterBody.baseJumpCount)
			{
				base.ProcessJump();
				return;
			}
			if (this.jumpInputReceived && base.characterMotor.isGrounded)
			{
				this.outer.SetNextState(new AnimatedJump());
			}
		}
	}
}
