using System;

namespace EntityStates.Railgunner.Backpack
{
	// Token: 0x02000214 RID: 532
	public class ChargingCryo : BaseCharging
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x00026F36 File Offset: 0x00025136
		protected override EntityState InstantiateChargedState()
		{
			return new ChargedCryo();
		}
	}
}
