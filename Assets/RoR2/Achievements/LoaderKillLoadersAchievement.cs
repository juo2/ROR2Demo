using System;

namespace RoR2.Achievements
{
	// Token: 0x02000EB0 RID: 3760
	[RegisterAchievement("LoaderKillLoaders", "Skills.Loader.Thunderslam", "DefeatSuperRoboBallBoss", typeof(LoaderKillLoadersAchievement.LoaderKillLoadersServerAchievement))]
	public class LoaderKillLoadersAchievement : BaseAchievement
	{
		// Token: 0x060055BE RID: 21950 RVA: 0x0015E97A File Offset: 0x0015CB7A
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("LoaderBody");
		}

		// Token: 0x060055BF RID: 21951 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x060055C0 RID: 21952 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x04005063 RID: 20579
		private const int requirement = 3;

		// Token: 0x02000EB1 RID: 3761
		private class LoaderKillLoadersServerAchievement : BaseServerAchievement
		{
			// Token: 0x060055C2 RID: 21954 RVA: 0x0015EA47 File Offset: 0x0015CC47
			public override void OnInstall()
			{
				base.OnInstall();
				this.bodyIndex = BodyCatalog.FindBodyIndex("LoaderBody");
				this.requiredSceneDef = SceneCatalog.GetSceneDefFromSceneName("artifactworld");
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeath;
			}

			// Token: 0x060055C3 RID: 21955 RVA: 0x0015EA80 File Offset: 0x0015CC80
			public override void OnUninstall()
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeath;
				base.OnUninstall();
			}

			// Token: 0x060055C4 RID: 21956 RVA: 0x0015EA9C File Offset: 0x0015CC9C
			private void OnCharacterDeath(DamageReport damageReport)
			{
				if (!damageReport.victimBody)
				{
					return;
				}
				if (damageReport.victimBody.bodyIndex == this.bodyIndex && base.IsCurrentBody(damageReport.attackerBody) && SceneCatalog.mostRecentSceneDef == this.requiredSceneDef)
				{
					this.numKills++;
				}
				if (this.numKills >= 3)
				{
					base.Grant();
				}
			}

			// Token: 0x04005064 RID: 20580
			private BodyIndex bodyIndex;

			// Token: 0x04005065 RID: 20581
			private SceneDef requiredSceneDef;

			// Token: 0x04005066 RID: 20582
			private int numKills;
		}
	}
}
