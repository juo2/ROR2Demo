using System;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000128 RID: 296
	public class VacuumEnter : BaseVacuumAttackState
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x00016709 File Offset: 0x00014909
		protected override void OnLifetimeExpiredAuthority()
		{
			this.outer.SetNextState(new VacuumWindUp());
		}
	}
}
