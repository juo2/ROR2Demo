using System;

namespace EntityStates.VoidMegaCrab.Weapon
{
	// Token: 0x0200014E RID: 334
	public class ChargeCrabWhiteCannon : ChargeCrabCannonBase
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x00018DDC File Offset: 0x00016FDC
		protected override FireCrabCannonBase GetNextState()
		{
			return new FireCrabWhiteCannon();
		}
	}
}
