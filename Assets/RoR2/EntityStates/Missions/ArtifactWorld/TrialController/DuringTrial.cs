using System;

namespace EntityStates.Missions.ArtifactWorld.TrialController
{
	// Token: 0x0200025F RID: 607
	public class DuringTrial : ArtifactTrialControllerBaseState
	{
		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002BCF4 File Offset: 0x00029EF4
		public virtual EntityState GetNextState()
		{
			return new AfterTrial();
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002BCFB File Offset: 0x00029EFB
		public override void OnEnter()
		{
			base.OnEnter();
			this.purchaseInteraction.enabled = false;
			this.childLocator.FindChild("DuringTrial").gameObject.SetActive(true);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002BD2A File Offset: 0x00029F2A
		public override void OnExit()
		{
			this.childLocator.FindChild("DuringTrial").gameObject.SetActive(false);
			base.OnExit();
		}
	}
}
