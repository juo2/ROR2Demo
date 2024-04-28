using System;

namespace RoR2.Achievements.Treebot
{
	// Token: 0x02000ECA RID: 3786
	[RegisterAchievement("TreebotBigHeal", "Skills.Treebot.SpecialAlt1", "RescueTreebot", typeof(TreebotBigHealAchievement.TreebotBigHealServerAchievement))]
	public class TreebotBigHealAchievement : BaseAchievement
	{
		// Token: 0x0600563F RID: 22079 RVA: 0x0015FAAE File Offset: 0x0015DCAE
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("TreebotBody");
		}

		// Token: 0x06005640 RID: 22080 RVA: 0x0015FABA File Offset: 0x0015DCBA
		public override void OnInstall()
		{
			base.OnInstall();
		}

		// Token: 0x06005641 RID: 22081 RVA: 0x0015D5B8 File Offset: 0x0015B7B8
		public override void OnUninstall()
		{
			base.OnUninstall();
		}

		// Token: 0x06005642 RID: 22082 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x06005643 RID: 22083 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x04005082 RID: 20610
		private static readonly float requirement = 1000f;

		// Token: 0x02000ECB RID: 3787
		private class TreebotBigHealServerAchievement : BaseServerAchievement
		{
			// Token: 0x06005646 RID: 22086 RVA: 0x0015FACE File Offset: 0x0015DCCE
			public override void OnInstall()
			{
				base.OnInstall();
				HealthComponent.onCharacterHealServer += this.OnCharacterHealServer;
			}

			// Token: 0x06005647 RID: 22087 RVA: 0x0015FAE7 File Offset: 0x0015DCE7
			public override void OnUninstall()
			{
				HealthComponent.onCharacterHealServer -= this.OnCharacterHealServer;
				base.OnInstall();
			}

			// Token: 0x06005648 RID: 22088 RVA: 0x0015FB00 File Offset: 0x0015DD00
			private void OnCharacterHealServer(HealthComponent healthComponent, float amount, ProcChainMask procChainMask)
			{
				if (amount >= TreebotBigHealAchievement.requirement)
				{
					HealthComponent component = base.GetCurrentBody().GetComponent<HealthComponent>();
					if (healthComponent == component)
					{
						base.Grant();
					}
				}
			}
		}
	}
}
