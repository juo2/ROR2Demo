using System;

namespace EntityStates.Missions.ArtifactWorld.TrialController
{
	// Token: 0x0200025B RID: 603
	public class AfterTrial2 : AfterTrial
	{
		// Token: 0x06000AAB RID: 2731 RVA: 0x0002BC6E File Offset: 0x00029E6E
		public override Type GetNextStateType()
		{
			return typeof(FinishTrial2);
		}
	}
}
