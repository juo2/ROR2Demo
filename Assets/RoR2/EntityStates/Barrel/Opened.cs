using System;

namespace EntityStates.Barrel
{
	// Token: 0x02000471 RID: 1137
	public class Opened : EntityState
	{
		// Token: 0x06001456 RID: 5206 RVA: 0x0005AC7B File Offset: 0x00058E7B
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Body", "Opened");
		}
	}
}
