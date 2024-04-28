using System;

namespace EntityStates.VoidMegaCrab.Weapon
{
	// Token: 0x0200014D RID: 333
	public class ChargeCrabBlackCannon : ChargeCrabCannonBase
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x00018DCD File Offset: 0x00016FCD
		protected override FireCrabCannonBase GetNextState()
		{
			return new FireCrabBlackCannon();
		}
	}
}
