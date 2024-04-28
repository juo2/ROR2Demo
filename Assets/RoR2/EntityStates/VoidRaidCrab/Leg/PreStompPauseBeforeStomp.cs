using System;

namespace EntityStates.VoidRaidCrab.Leg
{
	// Token: 0x0200013D RID: 317
	public class PreStompPauseBeforeStomp : BaseStompState
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x00018137 File Offset: 0x00016337
		protected override void OnLifetimeExpiredAuthority()
		{
			this.outer.SetNextState(new Stomp
			{
				target = this.target
			});
		}
	}
}
