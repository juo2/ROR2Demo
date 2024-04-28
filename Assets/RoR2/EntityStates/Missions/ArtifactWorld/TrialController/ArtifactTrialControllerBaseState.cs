using System;
using RoR2;

namespace EntityStates.Missions.ArtifactWorld.TrialController
{
	// Token: 0x0200025D RID: 605
	public class ArtifactTrialControllerBaseState : EntityState
	{
		// Token: 0x06000AAE RID: 2734 RVA: 0x0002BC7A File Offset: 0x00029E7A
		public override void OnEnter()
		{
			base.OnEnter();
			this.purchaseInteraction = base.GetComponent<PurchaseInteraction>();
			this.childLocator = base.GetComponent<ChildLocator>();
		}

		// Token: 0x04000C3E RID: 3134
		protected PurchaseInteraction purchaseInteraction;

		// Token: 0x04000C3F RID: 3135
		protected ChildLocator childLocator;
	}
}
