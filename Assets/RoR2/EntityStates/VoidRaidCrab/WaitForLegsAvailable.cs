using System;
using RoR2.VoidRaidCrab;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x0200012D RID: 301
	public class WaitForLegsAvailable : BaseState
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x00016C1A File Offset: 0x00014E1A
		public override void OnEnter()
		{
			base.OnEnter();
			this.centralLegController = base.GetComponent<CentralLegController>();
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00016C2E File Offset: 0x00014E2E
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && !this.centralLegController.AreLegsBlockingBodyAnimation())
			{
				this.outer.SetNextState(this.nextState);
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00016C5C File Offset: 0x00014E5C
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return this.nextState.GetMinimumInterruptPriority();
		}

		// Token: 0x04000636 RID: 1590
		public EntityState nextState;

		// Token: 0x04000637 RID: 1591
		private CentralLegController centralLegController;
	}
}
