using System;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000129 RID: 297
	public class VacuumWindUp : BaseVacuumAttackState
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x00016723 File Offset: 0x00014923
		protected override void OnLifetimeExpiredAuthority()
		{
			this.outer.SetNextState(new VacuumAttack());
		}
	}
}
