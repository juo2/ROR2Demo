using System;

namespace RoR2.Achievements.Engi
{
	// Token: 0x02000EF4 RID: 3828
	[RegisterAchievement("EngiKillBossQuick", "Skills.Engi.SpiderMine", "Complete30StagesCareer", typeof(EngiKillBossQuickAchievement.EngiKillBossQuickServerAchievement))]
	public class EngiKillBossQuickAchievement : BaseAchievement
	{
		// Token: 0x06005730 RID: 22320 RVA: 0x00160FED File Offset: 0x0015F1ED
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("EngiBody");
		}

		// Token: 0x06005731 RID: 22321 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x06005732 RID: 22322 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x02000EF5 RID: 3829
		private class EngiKillBossQuickServerAchievement : BaseServerAchievement
		{
			// Token: 0x06005734 RID: 22324 RVA: 0x00161178 File Offset: 0x0015F378
			public override void OnInstall()
			{
				base.OnInstall();
				BossGroup.onBossGroupDefeatedServer += this.OnBossGroupDefeatedServer;
			}

			// Token: 0x06005735 RID: 22325 RVA: 0x00161191 File Offset: 0x0015F391
			public override void OnUninstall()
			{
				BossGroup.onBossGroupDefeatedServer -= this.OnBossGroupDefeatedServer;
				base.OnUninstall();
			}

			// Token: 0x06005736 RID: 22326 RVA: 0x001611AA File Offset: 0x0015F3AA
			private void OnBossGroupDefeatedServer(BossGroup bossGroup)
			{
				if (bossGroup.fixedTimeSinceEnabled <= 5f)
				{
					base.Grant();
				}
			}

			// Token: 0x040050BD RID: 20669
			private const float requirement = 5f;
		}
	}
}
