using System;

namespace EntityStates.Missions.ArtifactWorld.TrialController
{
	// Token: 0x02000259 RID: 601
	public class AfterTrial1 : AfterTrial
	{
		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002BC52 File Offset: 0x00029E52
		public override Type GetNextStateType()
		{
			return typeof(FinishTrial1);
		}
	}
}
