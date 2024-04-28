using System;

namespace EntityStates.Mage.Weapon
{
	// Token: 0x0200029D RID: 669
	public class ChargeIcebomb : BaseChargeBombState
	{
		// Token: 0x06000BDD RID: 3037 RVA: 0x000313B9 File Offset: 0x0002F5B9
		protected override BaseThrowBombState GetNextState()
		{
			return new ThrowIcebomb();
		}
	}
}
