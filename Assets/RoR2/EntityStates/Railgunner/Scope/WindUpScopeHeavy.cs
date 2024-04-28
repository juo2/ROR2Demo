using System;

namespace EntityStates.Railgunner.Scope
{
	// Token: 0x02000207 RID: 519
	public class WindUpScopeHeavy : BaseWindUp
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x00025F0B File Offset: 0x0002410B
		protected override BaseActive GetNextState()
		{
			return new ActiveScopeHeavy();
		}
	}
}
