using System;

namespace RoR2.Achievements.Mage
{
	// Token: 0x02000EE6 RID: 3814
	public class MageMultiExecuteAchievement : BaseAchievement
	{
		// Token: 0x060056E0 RID: 22240 RVA: 0x00160754 File Offset: 0x0015E954
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("MageBody");
		}

		// Token: 0x060056E1 RID: 22241 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x060056E2 RID: 22242 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x040050AC RID: 20652
		private static int requirement = 10;

		// Token: 0x040050AD RID: 20653
		private static float window = 10f;

		// Token: 0x02000EE7 RID: 3815
		private class MageMultiExecuteServerAchievement : BaseServerAchievement
		{
			// Token: 0x060056E5 RID: 22245 RVA: 0x00160A3E File Offset: 0x0015EC3E
			public override void OnInstall()
			{
				base.OnInstall();
				this.tracker = new DoXInYSecondsTracker(MageMultiExecuteAchievement.requirement, MageMultiExecuteAchievement.window);
				Run.onRunStartGlobal += this.OnRunStart;
				GlobalEventManager.onServerCharacterExecuted += this.OnServerCharacterExecuted;
			}

			// Token: 0x060056E6 RID: 22246 RVA: 0x00160A7D File Offset: 0x0015EC7D
			public override void OnUninstall()
			{
				Run.onRunStartGlobal -= this.OnRunStart;
				GlobalEventManager.onServerCharacterExecuted -= this.OnServerCharacterExecuted;
				base.OnUninstall();
			}

			// Token: 0x060056E7 RID: 22247 RVA: 0x00160AA7 File Offset: 0x0015ECA7
			private void OnRunStart(Run run)
			{
				this.tracker.Clear();
			}

			// Token: 0x060056E8 RID: 22248 RVA: 0x00160AB4 File Offset: 0x0015ECB4
			private void OnServerCharacterExecuted(DamageReport damageReport, float executionHealthLost)
			{
				if (damageReport.attackerMaster == base.networkUser.master && base.networkUser.master != null && this.tracker.Push(Run.FixedTimeStamp.now.t))
				{
					base.Grant();
				}
			}

			// Token: 0x040050AE RID: 20654
			private DoXInYSecondsTracker tracker;
		}
	}
}
