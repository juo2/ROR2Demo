using System;

namespace RoR2.Achievements
{
	// Token: 0x02000EA5 RID: 3749
	[RegisterAchievement("KillBossQuick", "Items.TreasureCache", null, typeof(KillBossQuickAchievement.KillBossQuickServerAchievement))]
	public class KillBossQuickAchievement : BaseAchievement
	{
		// Token: 0x0600559A RID: 21914 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x0600559B RID: 21915 RVA: 0x0015D5B8 File Offset: 0x0015B7B8
		public override void OnUninstall()
		{
			base.OnUninstall();
		}

		// Token: 0x02000EA6 RID: 3750
		private class KillBossQuickServerAchievement : BaseServerAchievement
		{
			// Token: 0x0600559D RID: 21917 RVA: 0x0015E818 File Offset: 0x0015CA18
			public override void OnInstall()
			{
				base.OnInstall();
				BossGroup.onBossGroupDefeatedServer += this.OnBossGroupDefeatedServer;
			}

			// Token: 0x0600559E RID: 21918 RVA: 0x0015E831 File Offset: 0x0015CA31
			public override void OnUninstall()
			{
				BossGroup.onBossGroupDefeatedServer -= this.OnBossGroupDefeatedServer;
				base.OnUninstall();
			}

			// Token: 0x0600559F RID: 21919 RVA: 0x0015E84A File Offset: 0x0015CA4A
			private void OnBossGroupDefeatedServer(BossGroup bossGroup)
			{
				if (bossGroup.fixedTimeSinceEnabled <= 15f && bossGroup.GetComponent<TeleporterInteraction>())
				{
					base.Grant();
				}
			}

			// Token: 0x04005060 RID: 20576
			private const float requirement = 15f;
		}
	}
}
