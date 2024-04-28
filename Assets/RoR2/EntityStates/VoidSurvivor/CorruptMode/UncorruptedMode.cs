using System;

namespace EntityStates.VoidSurvivor.CorruptMode
{
	// Token: 0x02000111 RID: 273
	public class UncorruptedMode : CorruptModeBase
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x00014998 File Offset: 0x00012B98
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && this.voidSurvivorController && this.voidSurvivorController.corruption >= this.voidSurvivorController.maxCorruption && this.voidSurvivorController.bodyStateMachine)
			{
				this.voidSurvivorController.bodyStateMachine.SetInterruptState(new EnterCorruptionTransition(), InterruptPriority.Skill);
			}
		}
	}
}
