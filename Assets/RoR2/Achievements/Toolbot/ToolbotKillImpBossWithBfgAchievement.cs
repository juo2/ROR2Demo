using System;

namespace RoR2.Achievements.Toolbot
{
	// Token: 0x02000ED5 RID: 3797
	[RegisterAchievement("ToolbotKillImpBossWithBfg", "Skills.Toolbot.Buzzsaw", "RepeatFirstTeleporter", typeof(ToolbotKillImpBossWithBfgAchievement.ToolbotKillImpBossWithBfgServerAchievement))]
	public class ToolbotKillImpBossWithBfgAchievement : BaseAchievement
	{
		// Token: 0x06005678 RID: 22136 RVA: 0x0015FD82 File Offset: 0x0015DF82
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("ToolbotBody");
		}

		// Token: 0x06005679 RID: 22137 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x0600567A RID: 22138 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x02000ED6 RID: 3798
		private class ToolbotKillImpBossWithBfgServerAchievement : BaseServerAchievement
		{
			// Token: 0x0600567C RID: 22140 RVA: 0x0015FFDC File Offset: 0x0015E1DC
			public override void OnInstall()
			{
				base.OnInstall();
				this.impBossBodyIndex = BodyCatalog.FindBodyIndex("ImpBossBody");
				this.bfgProjectileIndex = ProjectileCatalog.FindProjectileIndex("BeamSphere");
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeath;
			}

			// Token: 0x0600567D RID: 22141 RVA: 0x00160015 File Offset: 0x0015E215
			public override void OnUninstall()
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeath;
				base.OnUninstall();
			}

			// Token: 0x0600567E RID: 22142 RVA: 0x00160030 File Offset: 0x0015E230
			private void OnCharacterDeath(DamageReport damageReport)
			{
				if (!damageReport.victimBody)
				{
					return;
				}
				if (damageReport.victimBody.bodyIndex != this.impBossBodyIndex)
				{
					return;
				}
				if (!base.IsCurrentBody(damageReport.damageInfo.attacker))
				{
					return;
				}
				if (ProjectileCatalog.GetProjectileIndex(damageReport.damageInfo.inflictor) != this.bfgProjectileIndex)
				{
					return;
				}
				base.Grant();
			}

			// Token: 0x04005092 RID: 20626
			private BodyIndex impBossBodyIndex = BodyIndex.None;

			// Token: 0x04005093 RID: 20627
			private int bfgProjectileIndex = -1;
		}
	}
}
