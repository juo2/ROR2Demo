using System;

namespace EntityStates.Railgunner.Backpack
{
	// Token: 0x02000218 RID: 536
	public class ExpiredSuper : BaseExpired
	{
		// Token: 0x0600096F RID: 2415 RVA: 0x00026FDB File Offset: 0x000251DB
		protected override EntityState InstantiateNextState()
		{
			return new OnlineSuper();
		}
	}
}
