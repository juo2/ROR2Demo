using System;
using EntityStates.VoidSurvivor.CorruptMode;
using UnityEngine.Networking;

namespace EntityStates.VoidSurvivor
{
	// Token: 0x020000EB RID: 235
	public class ExitCorruptionTransition : CorruptionTransitionBase
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x0001177D File Offset: 0x0000F97D
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.voidSurvivorController && NetworkServer.active)
			{
				this.voidSurvivorController.AddCorruption(-100f);
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000117A9 File Offset: 0x0000F9A9
		public override void OnFinishAuthority()
		{
			base.OnFinishAuthority();
			if (this.voidSurvivorController)
			{
				this.voidSurvivorController.corruptionModeStateMachine.SetNextState(new UncorruptedMode());
			}
		}
	}
}
