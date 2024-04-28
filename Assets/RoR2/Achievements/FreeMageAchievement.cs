using System;
using EntityStates.LockedMage;

namespace RoR2.Achievements
{
	// Token: 0x02000E9F RID: 3743
	[RegisterAchievement("FreeMage", "Characters.Mage", null, typeof(FreeMageAchievement.FreeMageServerAchievement))]
	public class FreeMageAchievement : BaseAchievement
	{
		// Token: 0x0600557D RID: 21885 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x0015D5B8 File Offset: 0x0015B7B8
		public override void OnUninstall()
		{
			base.OnUninstall();
		}

		// Token: 0x02000EA0 RID: 3744
		private class FreeMageServerAchievement : BaseServerAchievement
		{
			// Token: 0x06005580 RID: 21888 RVA: 0x0015E45B File Offset: 0x0015C65B
			public override void OnInstall()
			{
				base.OnInstall();
				UnlockingMage.onOpened += this.OnOpened;
			}

			// Token: 0x06005581 RID: 21889 RVA: 0x0015E474 File Offset: 0x0015C674
			public override void OnUninstall()
			{
				base.OnInstall();
				UnlockingMage.onOpened -= this.OnOpened;
			}

			// Token: 0x06005582 RID: 21890 RVA: 0x0015E490 File Offset: 0x0015C690
			private void OnOpened(Interactor interactor)
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
