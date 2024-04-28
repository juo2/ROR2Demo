using System;

namespace RoR2.Achievements.Croco
{
	// Token: 0x02000EF6 RID: 3830
	[RegisterAchievement("CrocoKillScavenger", "Skills.Croco.CrocoBite", "BeatArena", typeof(CrocoKillScavengerAchievement.CrocoKillScavengerServerAchievement))]
	public class CrocoKillScavengerAchievement : BaseAchievement
	{
		// Token: 0x06005738 RID: 22328 RVA: 0x001611BF File Offset: 0x0015F3BF
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("CrocoBody");
		}

		// Token: 0x06005739 RID: 22329 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x0600573A RID: 22330 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x02000EF7 RID: 3831
		private class CrocoKillScavengerServerAchievement : BaseServerAchievement
		{
			// Token: 0x0600573C RID: 22332 RVA: 0x001611CB File Offset: 0x0015F3CB
			public override void OnInstall()
			{
				base.OnInstall();
				this.requiredVictimBodyIndex = BodyCatalog.FindBodyIndex("ScavBody");
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathGlobal;
			}

			// Token: 0x0600573D RID: 22333 RVA: 0x001611F4 File Offset: 0x0015F3F4
			public override void OnUninstall()
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeathGlobal;
				base.OnUninstall();
			}

			// Token: 0x0600573E RID: 22334 RVA: 0x0016120D File Offset: 0x0015F40D
			private void OnCharacterDeathGlobal(DamageReport damageReport)
			{
				if (damageReport.victimBodyIndex == this.requiredVictimBodyIndex && this.serverAchievementTracker.networkUser.master == damageReport.attackerMaster)
				{
					base.Grant();
				}
			}

			// Token: 0x040050BE RID: 20670
			private BodyIndex requiredVictimBodyIndex;
		}
	}
}
