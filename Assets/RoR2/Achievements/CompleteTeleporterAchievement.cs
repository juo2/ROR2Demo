using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E85 RID: 3717
	[RegisterAchievement("CompleteTeleporter", "Items.BossDamageBonus", null, typeof(CompleteTeleporterAchievement.CompleteTeleporterServerAchievement))]
	public class CompleteTeleporterAchievement : BaseAchievement
	{
		// Token: 0x06005511 RID: 21777 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x0015DB42 File Offset: 0x0015BD42
		public override void OnUninstall()
		{
			base.SetServerTracked(false);
			base.OnUninstall();
		}

		// Token: 0x02000E86 RID: 3718
		private class CompleteTeleporterServerAchievement : BaseServerAchievement
		{
			// Token: 0x06005514 RID: 21780 RVA: 0x0015DB51 File Offset: 0x0015BD51
			public override void OnInstall()
			{
				base.OnInstall();
				SceneExitController.onBeginExit += this.OnSceneBeginExit;
			}

			// Token: 0x06005515 RID: 21781 RVA: 0x0015DB6A File Offset: 0x0015BD6A
			public override void OnUninstall()
			{
				SceneExitController.onBeginExit -= this.OnSceneBeginExit;
				base.OnInstall();
			}

			// Token: 0x06005516 RID: 21782 RVA: 0x0015DB83 File Offset: 0x0015BD83
			private void OnSceneBeginExit(SceneExitController exitController)
			{
				this.Check();
			}

			// Token: 0x06005517 RID: 21783 RVA: 0x0015DB8C File Offset: 0x0015BD8C
			private void Check()
			{
				CharacterBody currentBody = base.GetCurrentBody();
				if (currentBody && currentBody.healthComponent && currentBody.healthComponent.alive)
				{
					base.Grant();
				}
			}
		}
	}
}
