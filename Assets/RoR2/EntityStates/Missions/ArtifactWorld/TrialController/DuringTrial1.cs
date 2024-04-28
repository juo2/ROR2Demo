using System;

namespace EntityStates.Missions.ArtifactWorld.TrialController
{
	// Token: 0x02000258 RID: 600
	public class DuringTrial1 : DuringTrial
	{
		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002BC1D File Offset: 0x00029E1D
		public override EntityState GetNextState()
		{
			return new AfterTrial1();
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002BC24 File Offset: 0x00029E24
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= DuringTrial1.trialDuration)
			{
				this.outer.SetNextState(this.GetNextState());
			}
		}

		// Token: 0x04000C3D RID: 3133
		public static float trialDuration;
	}
}
