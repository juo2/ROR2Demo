using System;

namespace EntityStates.Railgunner.Scope
{
	// Token: 0x02000208 RID: 520
	public class WindUpScopeLight : BaseWindUp
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x00025F1A File Offset: 0x0002411A
		protected override BaseActive GetNextState()
		{
			return new ActiveScopeLight();
		}
	}
}
