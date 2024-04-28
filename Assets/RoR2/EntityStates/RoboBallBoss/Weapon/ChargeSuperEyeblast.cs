using System;

namespace EntityStates.RoboBallBoss.Weapon
{
	// Token: 0x020001E7 RID: 487
	public class ChargeSuperEyeblast : ChargeEyeblast
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x00024D75 File Offset: 0x00022F75
		public override EntityState GetNextState()
		{
			return new FireSuperEyeblast();
		}
	}
}
