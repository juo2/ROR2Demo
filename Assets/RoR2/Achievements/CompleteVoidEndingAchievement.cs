using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E97 RID: 3735
	[RegisterAchievement("CompleteVoidEnding", "Characters.VoidSurvivor", null, typeof(CompleteVoidEndingAchievement.CompleteWave50ServerAchievement))]
	public class CompleteVoidEndingAchievement : BaseEndingAchievement
	{
		// Token: 0x0600555A RID: 21850 RVA: 0x0015E1B9 File Offset: 0x0015C3B9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x0600555B RID: 21851 RVA: 0x0015E1C8 File Offset: 0x0015C3C8
		public override void OnUninstall()
		{
			base.SetServerTracked(false);
			base.OnUninstall();
		}

		// Token: 0x0600555C RID: 21852 RVA: 0x0015E1D7 File Offset: 0x0015C3D7
		protected override bool ShouldGrant(RunReport runReport)
		{
			return runReport.gameEnding == DLC1Content.GameEndings.VoidEnding;
		}

		// Token: 0x02000E98 RID: 3736
		private class CompleteWave50ServerAchievement : BaseServerAchievement
		{
			// Token: 0x0600555E RID: 21854 RVA: 0x0015E1EE File Offset: 0x0015C3EE
			public override void OnInstall()
			{
				base.OnInstall();
				InfiniteTowerRun.onAllEnemiesDefeatedServer += this.OnAllEnemiesDefeatedServer;
			}

			// Token: 0x0600555F RID: 21855 RVA: 0x0015E207 File Offset: 0x0015C407
			public override void OnUninstall()
			{
				InfiniteTowerRun.onAllEnemiesDefeatedServer -= this.OnAllEnemiesDefeatedServer;
				base.OnUninstall();
			}

			// Token: 0x06005560 RID: 21856 RVA: 0x0015E220 File Offset: 0x0015C420
			private void OnAllEnemiesDefeatedServer(InfiniteTowerWaveController waveController)
			{
				InfiniteTowerRun infiniteTowerRun = Run.instance as InfiniteTowerRun;
				if (infiniteTowerRun && infiniteTowerRun.waveIndex >= 50)
				{
					base.Grant();
				}
			}

			// Token: 0x04005058 RID: 20568
			private const int waveRequirement = 50;
		}
	}
}
