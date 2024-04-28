using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E83 RID: 3715
	[RegisterAchievement("CompleteMultiBossShrine", "Items.Lightning", null, typeof(CompleteMultiBossShrineAchievement.CompleteMultiBossShrineServerAchievement))]
	public class CompleteMultiBossShrineAchievement : BaseAchievement
	{
		// Token: 0x06005509 RID: 21769 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x0015D5B8 File Offset: 0x0015B7B8
		public override void OnUninstall()
		{
			base.OnUninstall();
		}

		// Token: 0x02000E84 RID: 3716
		private class CompleteMultiBossShrineServerAchievement : BaseServerAchievement
		{
			// Token: 0x0600550C RID: 21772 RVA: 0x0015DA8F File Offset: 0x0015BC8F
			public override void OnInstall()
			{
				base.OnInstall();
				BossGroup.onBossGroupDefeatedServer += this.OnBossGroupDefeatedServer;
			}

			// Token: 0x0600550D RID: 21773 RVA: 0x0015DAA8 File Offset: 0x0015BCA8
			public override void OnUninstall()
			{
				BossGroup.onBossGroupDefeatedServer -= this.OnBossGroupDefeatedServer;
				base.OnUninstall();
			}

			// Token: 0x0600550E RID: 21774 RVA: 0x0015DAC4 File Offset: 0x0015BCC4
			private void OnBossGroupDefeatedServer(BossGroup bossGroup)
			{
				CharacterBody currentBody = base.GetCurrentBody();
				if (currentBody && currentBody.healthComponent && currentBody.healthComponent.alive && TeleporterInteraction.instance && this.CheckTeleporter(bossGroup, TeleporterInteraction.instance))
				{
					base.Grant();
				}
			}

			// Token: 0x0600550F RID: 21775 RVA: 0x0015DB1A File Offset: 0x0015BD1A
			private bool CheckTeleporter(BossGroup bossGroup, TeleporterInteraction teleporterInteraction)
			{
				return teleporterInteraction.bossDirector.combatSquad == bossGroup.combatSquad && teleporterInteraction.shrineBonusStacks >= 2;
			}

			// Token: 0x04005051 RID: 20561
			private const int requirement = 2;
		}
	}
}
