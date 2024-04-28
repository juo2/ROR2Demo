using System;

namespace EntityStates.Loader
{
	// Token: 0x020002C3 RID: 707
	public class ChargeZapFist : BaseChargeFist
	{
		// Token: 0x06000C8B RID: 3211 RVA: 0x00034E10 File Offset: 0x00033010
		protected override bool ShouldKeepChargingAuthority()
		{
			return base.fixedAge < base.chargeDuration;
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00034E20 File Offset: 0x00033020
		protected override EntityState GetNextStateAuthority()
		{
			return new SwingZapFist
			{
				charge = base.charge
			};
		}
	}
}
