using System;

namespace EntityStates.Railgunner.Scope
{
	// Token: 0x02000200 RID: 512
	public class ActiveScopeLight : BaseActive
	{
		// Token: 0x06000905 RID: 2309 RVA: 0x00025A43 File Offset: 0x00023C43
		protected override BaseWindDown GetNextState()
		{
			return new WindDownScopeLight();
		}
	}
}
