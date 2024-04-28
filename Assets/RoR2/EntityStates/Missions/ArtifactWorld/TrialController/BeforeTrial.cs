using System;

namespace EntityStates.Missions.ArtifactWorld.TrialController
{
	// Token: 0x0200025E RID: 606
	public class BeforeTrial : ArtifactTrialControllerBaseState
	{
		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002BC9A File Offset: 0x00029E9A
		public override void OnEnter()
		{
			base.OnEnter();
			this.purchaseInteraction.enabled = true;
			this.childLocator.FindChild("BeforeTrial").gameObject.SetActive(true);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002BCC9 File Offset: 0x00029EC9
		public override void OnExit()
		{
			this.childLocator.FindChild("BeforeTrial").gameObject.SetActive(false);
			base.OnExit();
		}
	}
}
