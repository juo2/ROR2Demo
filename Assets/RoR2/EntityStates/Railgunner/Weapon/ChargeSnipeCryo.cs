using System;
using EntityStates.Railgunner.Backpack;

namespace EntityStates.Railgunner.Weapon
{
	// Token: 0x020001F3 RID: 499
	public class ChargeSnipeCryo : BaseChargeSnipe
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x000257C9 File Offset: 0x000239C9
		protected override EntityState InstantiateBackpackState()
		{
			return new ChargingCryo();
		}
	}
}
