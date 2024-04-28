using System;

namespace EntityStates.Railgunner.Weapon
{
	// Token: 0x020001FE RID: 510
	public class WindupSnipeSuper : BaseWindupSnipe
	{
		// Token: 0x06000901 RID: 2305 RVA: 0x00025A2D File Offset: 0x00023C2D
		protected override EntityState InstantiateNextState()
		{
			return new FireSnipeSuper();
		}
	}
}
