using System;
using EntityStates.Treebot.UnlockInteractable;

namespace RoR2.Achievements
{
	// Token: 0x02000EC0 RID: 3776
	[RegisterAchievement("RescueTreebot", "Characters.Treebot", null, typeof(RescueTreebotAchievement.RescueTreebotServerAchievement))]
	public class RescueTreebotAchievement : BaseAchievement
	{
		// Token: 0x06005609 RID: 22025 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x0600560A RID: 22026 RVA: 0x0015D5B8 File Offset: 0x0015B7B8
		public override void OnUninstall()
		{
			base.OnUninstall();
		}

		// Token: 0x02000EC1 RID: 3777
		public class RescueTreebotServerAchievement : BaseServerAchievement
		{
			// Token: 0x0600560C RID: 22028 RVA: 0x0015F2D8 File Offset: 0x0015D4D8
			public override void OnInstall()
			{
				base.OnInstall();
				Unlock.onActivated += this.OnActivated;
			}

			// Token: 0x0600560D RID: 22029 RVA: 0x0015F2F1 File Offset: 0x0015D4F1
			public override void OnUninstall()
			{
				Unlock.onActivated -= this.OnActivated;
				base.OnInstall();
			}

			// Token: 0x0600560E RID: 22030 RVA: 0x0015F30C File Offset: 0x0015D50C
			private void OnActivated(Interactor interactor)
			{
				CharacterBody currentBody = this.serverAchievementTracker.networkUser.GetCurrentBody();
				if (currentBody && currentBody.GetComponent<Interactor>() == interactor)
				{
					base.Grant();
				}
			}
		}
	}
}
