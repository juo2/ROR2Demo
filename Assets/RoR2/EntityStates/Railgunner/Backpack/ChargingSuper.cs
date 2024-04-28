using System;

namespace EntityStates.Railgunner.Backpack
{
	// Token: 0x02000215 RID: 533
	public class ChargingSuper : BaseCharging
	{
		// Token: 0x06000968 RID: 2408 RVA: 0x00026F45 File Offset: 0x00025145
		protected override EntityState InstantiateChargedState()
		{
			return new ChargedSuper();
		}
	}
}
