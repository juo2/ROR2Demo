using System;

namespace EntityStates.Railgunner.Backpack
{
	// Token: 0x02000213 RID: 531
	public class ChargedSuper : BaseCharged
	{
		// Token: 0x06000964 RID: 2404 RVA: 0x00026F2F File Offset: 0x0002512F
		protected override EntityState InstantiateExpiredState()
		{
			return new ExpiredSuper();
		}
	}
}
