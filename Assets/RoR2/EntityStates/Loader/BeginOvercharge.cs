using System;
using RoR2;

namespace EntityStates.Loader
{
	// Token: 0x020002BE RID: 702
	public class BeginOvercharge : BaseState
	{
		// Token: 0x06000C70 RID: 3184 RVA: 0x0003466A File Offset: 0x0003286A
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				base.GetComponent<LoaderStaticChargeComponent>().ConsumeChargeAuthority();
			}
			this.outer.SetNextStateToMain();
		}
	}
}
