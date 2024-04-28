using System;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x0200012B RID: 299
	public class VacuumWindDown : BaseVacuumAttackState
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x00016BB8 File Offset: 0x00014DB8
		protected override void OnLifetimeExpiredAuthority()
		{
			this.outer.SetNextState(new VacuumExit());
		}
	}
}
