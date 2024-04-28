using System;

namespace EntityStates.Railgunner.Scope
{
	// Token: 0x020001FF RID: 511
	public class ActiveScopeHeavy : BaseActive
	{
		// Token: 0x06000903 RID: 2307 RVA: 0x00025A34 File Offset: 0x00023C34
		protected override BaseWindDown GetNextState()
		{
			return new WindDownScopeHeavy();
		}
	}
}
