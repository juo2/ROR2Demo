using System;

namespace EntityStates.MinorConstruct
{
	// Token: 0x0200026C RID: 620
	public class Revealed : BaseHideState
	{
		// Token: 0x06000ADC RID: 2780 RVA: 0x0002C46A File Offset: 0x0002A66A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.characterBody.outOfCombat && base.characterBody.outOfDanger)
			{
				this.outer.SetNextState(new Hidden());
			}
		}
	}
}
