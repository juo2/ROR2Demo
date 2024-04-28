using System;

namespace EntityStates.BrotherMonster
{
	// Token: 0x0200044A RID: 1098
	public class StaggerLoop : StaggerBaseState
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x00057600 File Offset: 0x00055800
		public override EntityState nextState
		{
			get
			{
				return new StaggerExit();
			}
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x00057607 File Offset: 0x00055807
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayCrossfade("Body", "StaggerLoop", 0.2f);
		}
	}
}
