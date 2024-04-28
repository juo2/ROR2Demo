using System;

namespace EntityStates.Railgunner.Weapon
{
	// Token: 0x020001FD RID: 509
	public class WindupSnipeCryo : BaseWindupSnipe
	{
		// Token: 0x060008FF RID: 2303 RVA: 0x00025A1E File Offset: 0x00023C1E
		protected override EntityState InstantiateNextState()
		{
			return new FireSnipeCryo();
		}
	}
}
