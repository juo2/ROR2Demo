using System;

namespace RoR2.Achievements.Bandit2
{
	// Token: 0x02000F08 RID: 3848
	[RegisterAchievement("Bandit2StackSuperBleed", "Skills.Bandit2.SerratedShivs", "CompleteThreeStages", typeof(Bandit2StackSuperBleedAchievement.Bandit2StackSuperBleedServerAchievement))]
	public class Bandit2StackSuperBleedAchievement : BaseAchievement
	{
		// Token: 0x06005786 RID: 22406 RVA: 0x0016172B File Offset: 0x0015F92B
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("Bandit2Body");
		}

		// Token: 0x06005787 RID: 22407 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x06005788 RID: 22408 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x040050D0 RID: 20688
		private static readonly int requirement = 20;

		// Token: 0x02000F09 RID: 3849
		private class Bandit2StackSuperBleedServerAchievement : BaseServerAchievement
		{
			// Token: 0x0600578B RID: 22411 RVA: 0x00161A47 File Offset: 0x0015FC47
			public override void OnInstall()
			{
				base.OnInstall();
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathGlobal;
			}

			// Token: 0x0600578C RID: 22412 RVA: 0x00161A60 File Offset: 0x0015FC60
			public override void OnUninstall()
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeathGlobal;
				base.OnUninstall();
			}

			// Token: 0x0600578D RID: 22413 RVA: 0x00161A7C File Offset: 0x0015FC7C
			private void OnCharacterDeathGlobal(DamageReport damageReport)
			{
				if (damageReport.attackerBody && damageReport.attackerBody == base.GetCurrentBody() && damageReport.victimBody && damageReport.victimBody.GetBuffCount(RoR2Content.Buffs.SuperBleed) >= Bandit2StackSuperBleedAchievement.requirement)
				{
					base.Grant();
				}
			}
		}
	}
}
