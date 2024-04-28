using System;

namespace RoR2.Achievements.Mage
{
	// Token: 0x02000EE8 RID: 3816
	[RegisterAchievement("MageMultiKill", "Skills.Mage.LightningBolt", "FreeMage", typeof(MageMultiKillAchievement.MageMultiKillServerAchievement))]
	public class MageMultiKillAchievement : BaseAchievement
	{
		// Token: 0x060056EA RID: 22250 RVA: 0x00160754 File Offset: 0x0015E954
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("MageBody");
		}

		// Token: 0x060056EB RID: 22251 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x060056EC RID: 22252 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x040050AF RID: 20655
		private static readonly int requirement = 20;

		// Token: 0x02000EE9 RID: 3817
		private class MageMultiKillServerAchievement : BaseServerAchievement
		{
			// Token: 0x060056EF RID: 22255 RVA: 0x00160AFC File Offset: 0x0015ECFC
			public override void OnInstall()
			{
				base.OnInstall();
				RoR2Application.onFixedUpdate += this.OnFixedUpdate;
			}

			// Token: 0x060056F0 RID: 22256 RVA: 0x00160B15 File Offset: 0x0015ED15
			public override void OnUninstall()
			{
				RoR2Application.onFixedUpdate -= this.OnFixedUpdate;
				base.OnUninstall();
			}

			// Token: 0x060056F1 RID: 22257 RVA: 0x00160B30 File Offset: 0x0015ED30
			private void OnFixedUpdate()
			{
				CharacterBody currentBody = base.GetCurrentBody();
				if (currentBody && MageMultiKillAchievement.requirement <= currentBody.multiKillCount)
				{
					base.Grant();
				}
			}
		}
	}
}
