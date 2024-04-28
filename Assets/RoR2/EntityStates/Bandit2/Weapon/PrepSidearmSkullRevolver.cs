using System;

namespace EntityStates.Bandit2.Weapon
{
	// Token: 0x02000482 RID: 1154
	public class PrepSidearmSkullRevolver : BasePrepSidearmRevolverState
	{
		// Token: 0x060014A0 RID: 5280 RVA: 0x0005BA85 File Offset: 0x00059C85
		protected override EntityState GetNextState()
		{
			return new FireSidearmSkullRevolver();
		}
	}
}
