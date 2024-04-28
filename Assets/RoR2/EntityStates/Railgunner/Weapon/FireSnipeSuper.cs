using System;
using EntityStates.Railgunner.Backpack;

namespace EntityStates.Railgunner.Weapon
{
	// Token: 0x020001FB RID: 507
	public class FireSnipeSuper : BaseFireSnipe
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x00025A17 File Offset: 0x00023C17
		protected override EntityState InstantiateBackpackState()
		{
			return new Offline();
		}
	}
}
