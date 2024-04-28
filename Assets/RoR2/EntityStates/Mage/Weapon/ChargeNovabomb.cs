using System;

namespace EntityStates.Mage.Weapon
{
	// Token: 0x0200029B RID: 667
	public class ChargeNovabomb : BaseChargeBombState
	{
		// Token: 0x06000BDA RID: 3034 RVA: 0x000313A2 File Offset: 0x0002F5A2
		protected override BaseThrowBombState GetNextState()
		{
			return new ThrowNovabomb();
		}
	}
}
