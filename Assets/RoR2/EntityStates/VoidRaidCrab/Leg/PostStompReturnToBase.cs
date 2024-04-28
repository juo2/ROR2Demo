using System;

namespace EntityStates.VoidRaidCrab.Leg
{
	// Token: 0x02000140 RID: 320
	public class PostStompReturnToBase : BaseStompState
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x0000CC4B File Offset: 0x0000AE4B
		protected override void OnLifetimeExpiredAuthority()
		{
			this.outer.SetNextStateToMain();
		}
	}
}
