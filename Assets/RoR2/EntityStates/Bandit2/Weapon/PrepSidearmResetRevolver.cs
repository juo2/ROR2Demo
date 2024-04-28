using System;

namespace EntityStates.Bandit2.Weapon
{
	// Token: 0x02000480 RID: 1152
	public class PrepSidearmResetRevolver : BasePrepSidearmRevolverState
	{
		// Token: 0x0600149C RID: 5276 RVA: 0x0005BA57 File Offset: 0x00059C57
		protected override EntityState GetNextState()
		{
			return new FireSidearmResetRevolver();
		}
	}
}
