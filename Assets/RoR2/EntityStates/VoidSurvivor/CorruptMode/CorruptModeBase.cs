using System;
using RoR2;

namespace EntityStates.VoidSurvivor.CorruptMode
{
	// Token: 0x0200010F RID: 271
	public class CorruptModeBase : BaseState
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x0001479A File Offset: 0x0001299A
		public override void OnEnter()
		{
			base.OnEnter();
			this.voidSurvivorController = base.GetComponent<VoidSurvivorController>();
		}

		// Token: 0x04000574 RID: 1396
		protected VoidSurvivorController voidSurvivorController;
	}
}
