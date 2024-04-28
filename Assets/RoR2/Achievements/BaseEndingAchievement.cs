using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E93 RID: 3731
	public abstract class BaseEndingAchievement : BaseAchievement
	{
		// Token: 0x0600554F RID: 21839
		protected abstract bool ShouldGrant(RunReport runReport);

		// Token: 0x06005550 RID: 21840 RVA: 0x0015E116 File Offset: 0x0015C316
		public override void OnInstall()
		{
			base.OnInstall();
			Run.onClientGameOverGlobal += this.OnClientGameOverGlobal;
		}

		// Token: 0x06005551 RID: 21841 RVA: 0x0015E12F File Offset: 0x0015C32F
		public override void OnUninstall()
		{
			Run.onClientGameOverGlobal -= this.OnClientGameOverGlobal;
			base.OnUninstall();
		}

		// Token: 0x06005552 RID: 21842 RVA: 0x0015E148 File Offset: 0x0015C348
		private void OnClientGameOverGlobal(Run run, RunReport runReport)
		{
			if (this.ShouldGrant(runReport))
			{
				base.Grant();
			}
		}
	}
}
