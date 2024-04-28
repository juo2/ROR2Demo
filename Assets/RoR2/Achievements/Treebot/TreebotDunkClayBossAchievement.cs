using System;

namespace RoR2.Achievements.Treebot
{
	// Token: 0x02000ECD RID: 3789
	[RegisterAchievement("TreebotDunkClayBoss", "Skills.Treebot.PlantSonicBoom", "RescueTreebot", typeof(TreebotDunkClayBossAchievement.TreebotDunkClayBossServerAchievement))]
	public class TreebotDunkClayBossAchievement : BaseAchievement
	{
		// Token: 0x0600564C RID: 22092 RVA: 0x0015FAAE File Offset: 0x0015DCAE
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("TreebotBody");
		}

		// Token: 0x0600564D RID: 22093 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x0600564E RID: 22094 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x02000ECE RID: 3790
		private class TreebotDunkClayBossServerAchievement : BaseServerAchievement
		{
			// Token: 0x06005650 RID: 22096 RVA: 0x0015FB2B File Offset: 0x0015DD2B
			public override void OnInstall()
			{
				base.OnInstall();
				this.clayBossBodyIndex = BodyCatalog.FindBodyIndex("ClayBossBody");
				this.requiredSceneDef = SceneCatalog.GetSceneDefFromSceneName("goolake");
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathGlobal;
			}

			// Token: 0x06005651 RID: 22097 RVA: 0x0015FB64 File Offset: 0x0015DD64
			private void OnCharacterDeathGlobal(DamageReport damageReport)
			{
				if (damageReport.victimBodyIndex == this.clayBossBodyIndex && base.IsCurrentBody(damageReport.attackerBody) && damageReport.damageInfo.inflictor && damageReport.damageInfo.inflictor.GetComponent<MapZone>() && SceneCatalog.mostRecentSceneDef == this.requiredSceneDef)
				{
					base.Grant();
				}
			}

			// Token: 0x06005652 RID: 22098 RVA: 0x0015FBCE File Offset: 0x0015DDCE
			public override void OnUninstall()
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeathGlobal;
				base.OnUninstall();
			}

			// Token: 0x04005083 RID: 20611
			private BodyIndex clayBossBodyIndex;

			// Token: 0x04005084 RID: 20612
			private SceneDef requiredSceneDef;
		}
	}
}
