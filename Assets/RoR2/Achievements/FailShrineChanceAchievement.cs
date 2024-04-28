using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E99 RID: 3737
	[RegisterAchievement("FailShrineChance", "Items.Hoof", null, typeof(FailShrineChanceAchievement.FailShrineChanceServerAchievement))]
	public class FailShrineChanceAchievement : BaseAchievement
	{
		// Token: 0x06005562 RID: 21858 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x06005563 RID: 21859 RVA: 0x0015D5B8 File Offset: 0x0015B7B8
		public override void OnUninstall()
		{
			base.OnUninstall();
		}

		// Token: 0x02000E9A RID: 3738
		private class FailShrineChanceServerAchievement : BaseServerAchievement
		{
			// Token: 0x06005565 RID: 21861 RVA: 0x0015E250 File Offset: 0x0015C450
			public override void OnInstall()
			{
				base.OnInstall();
				ShrineChanceBehavior.onShrineChancePurchaseGlobal += this.OnShrineChancePurchase;
				Run.onRunStartGlobal += this.OnRunStartGlobal;
			}

			// Token: 0x06005566 RID: 21862 RVA: 0x0015E27A File Offset: 0x0015C47A
			public override void OnUninstall()
			{
				base.OnInstall();
				ShrineChanceBehavior.onShrineChancePurchaseGlobal -= this.OnShrineChancePurchase;
				Run.onRunStartGlobal -= this.OnRunStartGlobal;
			}

			// Token: 0x06005567 RID: 21863 RVA: 0x0015E2A4 File Offset: 0x0015C4A4
			private void OnRunStartGlobal(Run run)
			{
				this.failedInARow = 0;
			}

			// Token: 0x06005568 RID: 21864 RVA: 0x0015E2B0 File Offset: 0x0015C4B0
			private void OnShrineChancePurchase(bool failed, Interactor interactor)
			{
				CharacterBody currentBody = this.serverAchievementTracker.networkUser.GetCurrentBody();
				if (currentBody && currentBody.GetComponent<Interactor>() == interactor)
				{
					if (failed)
					{
						this.failedInARow++;
						if (this.failedInARow >= 3)
						{
							base.Grant();
							return;
						}
					}
					else
					{
						this.failedInARow = 0;
					}
				}
			}

			// Token: 0x04005059 RID: 20569
			private int failedInARow;

			// Token: 0x0400505A RID: 20570
			private const int requirement = 3;
		}
	}
}
