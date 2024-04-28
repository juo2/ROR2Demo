using System;

namespace EntityStates
{
	// Token: 0x020000C4 RID: 196
	public class GenericCharacterVehicleSeated : BaseState
	{
		// Token: 0x06000398 RID: 920 RVA: 0x0000EBE5 File Offset: 0x0000CDE5
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Vehicle;
		}
	}
}
