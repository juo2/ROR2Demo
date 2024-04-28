using System;
using EntityStates.Railgunner.Backpack;

namespace EntityStates.Railgunner.Weapon
{
	// Token: 0x020001F4 RID: 500
	public class ChargeSnipeSuper : BaseChargeSnipe
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x000257D8 File Offset: 0x000239D8
		protected override EntityState InstantiateBackpackState()
		{
			return new ChargingSuper();
		}
	}
}
