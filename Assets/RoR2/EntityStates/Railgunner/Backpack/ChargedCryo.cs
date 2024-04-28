using System;

namespace EntityStates.Railgunner.Backpack
{
	// Token: 0x02000212 RID: 530
	public class ChargedCryo : BaseCharged
	{
		// Token: 0x06000962 RID: 2402 RVA: 0x00026F20 File Offset: 0x00025120
		protected override EntityState InstantiateExpiredState()
		{
			return new ExpiredCryo();
		}
	}
}
