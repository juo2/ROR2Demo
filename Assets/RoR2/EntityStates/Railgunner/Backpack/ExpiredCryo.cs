using System;

namespace EntityStates.Railgunner.Backpack
{
	// Token: 0x02000217 RID: 535
	public class ExpiredCryo : BaseExpired
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x00026FCC File Offset: 0x000251CC
		protected override EntityState InstantiateNextState()
		{
			return new OnlineCryo();
		}
	}
}
