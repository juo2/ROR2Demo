using System;

namespace RoR2.Achievements.Loader
{
	// Token: 0x02000EEB RID: 3819
	[RegisterAchievement("LoaderSpeedRun", "Skills.Loader.YankHook", "DefeatSuperRoboBallBoss", null)]
	public class LoaderSpeedRunAchievement : BaseAchievement
	{
		// Token: 0x060056F5 RID: 22261 RVA: 0x00160B5F File Offset: 0x0015ED5F
		public override void OnInstall()
		{
			base.OnInstall();
			this.requiredSceneDef = SceneCatalog.GetSceneDefFromSceneName("mysteryspace");
		}

		// Token: 0x060056F6 RID: 22262 RVA: 0x0015E97A File Offset: 0x0015CB7A
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("LoaderBody");
		}

		// Token: 0x060056F7 RID: 22263 RVA: 0x00160B77 File Offset: 0x0015ED77
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			SceneCatalog.onMostRecentSceneDefChanged += this.OnMostRecentSceneDefChanged;
		}

		// Token: 0x060056F8 RID: 22264 RVA: 0x00160B90 File Offset: 0x0015ED90
		private void OnMostRecentSceneDefChanged(SceneDef sceneDef)
		{
			if (sceneDef == this.requiredSceneDef && Run.instance.GetRunStopwatch() <= LoaderSpeedRunAchievement.requirement)
			{
				base.Grant();
			}
		}

		// Token: 0x060056F9 RID: 22265 RVA: 0x00160BB7 File Offset: 0x0015EDB7
		protected override void OnBodyRequirementBroken()
		{
			SceneCatalog.onMostRecentSceneDefChanged -= this.OnMostRecentSceneDefChanged;
			base.OnBodyRequirementBroken();
		}

		// Token: 0x040050B0 RID: 20656
		private SceneDef requiredSceneDef;

		// Token: 0x040050B1 RID: 20657
		private static readonly float requirement = 1500f;
	}
}
