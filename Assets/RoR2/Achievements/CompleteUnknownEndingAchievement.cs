using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E8B RID: 3723
	[RegisterAchievement("CompleteUnknownEnding", "Characters.Mercenary", null, typeof(CompleteUnknownEndingAchievement.CompleteUnknownEndingServerAchievement))]
	public class CompleteUnknownEndingAchievement : BaseAchievement
	{
		// Token: 0x0600552D RID: 21805 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x0015D5B8 File Offset: 0x0015B7B8
		public override void OnUninstall()
		{
			base.OnUninstall();
		}

		// Token: 0x02000E8C RID: 3724
		private class CompleteUnknownEndingServerAchievement : BaseServerAchievement
		{
			// Token: 0x06005530 RID: 21808 RVA: 0x0015DE78 File Offset: 0x0015C078
			public override void OnInstall()
			{
				base.OnInstall();
				Run.onServerGameOver += this.OnServerGameOver;
			}

			// Token: 0x06005531 RID: 21809 RVA: 0x0015DE91 File Offset: 0x0015C091
			public override void OnUninstall()
			{
				base.OnInstall();
				Run.onServerGameOver -= this.OnServerGameOver;
			}

			// Token: 0x06005532 RID: 21810 RVA: 0x0015DEAA File Offset: 0x0015C0AA
			private void OnServerGameOver(Run run, GameEndingDef gameEndingDef)
			{
				if (gameEndingDef == RoR2Content.GameEndings.ObliterationEnding || gameEndingDef == RoR2Content.GameEndings.LimboEnding)
				{
					base.Grant();
				}
			}
		}
	}
}
