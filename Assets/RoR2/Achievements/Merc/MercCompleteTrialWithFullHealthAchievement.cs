using System;

namespace RoR2.Achievements.Merc
{
	// Token: 0x02000EDD RID: 3805
	[RegisterAchievement("MercCompleteTrialWithFullHealth", "Skills.Merc.EvisProjectile", "CompleteUnknownEnding", typeof(MercCompleteTrialWithFullHealthAchievement.MercCompleteTrialWithFullHealthServerAchievement))]
	public class MercCompleteTrialWithFullHealthAchievement : BaseAchievement
	{
		// Token: 0x0600569B RID: 22171 RVA: 0x001602A3 File Offset: 0x0015E4A3
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("MercBody");
		}

		// Token: 0x0600569C RID: 22172 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x0600569D RID: 22173 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x02000EDE RID: 3806
		private class MercCompleteTrialWithFullHealthServerAchievement : BaseServerAchievement
		{
			// Token: 0x0600569F RID: 22175 RVA: 0x001602B0 File Offset: 0x0015E4B0
			public override void OnInstall()
			{
				base.OnInstall();
				this.listenForDamage = new ToggleAction(delegate()
				{
					RoR2Application.onFixedUpdate += this.OnFixedUpdate;
				}, delegate()
				{
					RoR2Application.onFixedUpdate -= this.OnFixedUpdate;
				});
				this.listenForGameOver = new ToggleAction(delegate()
				{
					Run.onServerGameOver += this.OnServerGameOver;
				}, delegate()
				{
					Run.onServerGameOver -= this.OnServerGameOver;
				});
				Run.onRunStartGlobal += this.OnRunStart;
				Run.onRunDestroyGlobal += this.OnRunDestroy;
				if (Run.instance)
				{
					this.OnRunDiscovered(Run.instance);
				}
			}

			// Token: 0x060056A0 RID: 22176 RVA: 0x00160344 File Offset: 0x0015E544
			public override void OnUninstall()
			{
				Run.onRunDestroyGlobal -= this.OnRunDestroy;
				Run.onRunStartGlobal -= this.OnRunStart;
				this.listenForGameOver.SetActive(false);
				this.listenForDamage.SetActive(false);
				base.OnUninstall();
			}

			// Token: 0x060056A1 RID: 22177 RVA: 0x00160394 File Offset: 0x0015E594
			private bool CharacterIsAtFullHealthOrNull()
			{
				CharacterBody currentBody = base.GetCurrentBody();
				return !currentBody || currentBody.healthComponent.fullCombinedHealth <= currentBody.healthComponent.combinedHealth;
			}

			// Token: 0x060056A2 RID: 22178 RVA: 0x001603CD File Offset: 0x0015E5CD
			private void OnFixedUpdate()
			{
				if (!this.CharacterIsAtFullHealthOrNull())
				{
					this.Fail();
				}
			}

			// Token: 0x060056A3 RID: 22179 RVA: 0x001603DD File Offset: 0x0015E5DD
			private void Fail()
			{
				this.failed = true;
				this.listenForDamage.SetActive(false);
				this.listenForGameOver.SetActive(false);
			}

			// Token: 0x060056A4 RID: 22180 RVA: 0x001603FE File Offset: 0x0015E5FE
			private void OnServerGameOver(Run run, GameEndingDef gameEndingDef)
			{
				if (gameEndingDef.isWin)
				{
					if (this.runOk && !this.failed)
					{
						base.Grant();
					}
					this.runOk = false;
				}
			}

			// Token: 0x060056A5 RID: 22181 RVA: 0x00160425 File Offset: 0x0015E625
			private void OnRunStart(Run run)
			{
				this.OnRunDiscovered(run);
			}

			// Token: 0x060056A6 RID: 22182 RVA: 0x0016042E File Offset: 0x0015E62E
			private void OnRunDiscovered(Run run)
			{
				this.runOk = (run is WeeklyRun);
				if (this.runOk)
				{
					this.listenForGameOver.SetActive(true);
					this.listenForDamage.SetActive(true);
					this.failed = false;
				}
			}

			// Token: 0x060056A7 RID: 22183 RVA: 0x00160466 File Offset: 0x0015E666
			private void OnRunDestroy(Run run)
			{
				this.OnRunLost(run);
			}

			// Token: 0x060056A8 RID: 22184 RVA: 0x0016046F File Offset: 0x0015E66F
			private void OnRunLost(Run run)
			{
				this.listenForGameOver.SetActive(false);
				this.listenForDamage.SetActive(false);
			}

			// Token: 0x0400509A RID: 20634
			private ToggleAction listenForDamage;

			// Token: 0x0400509B RID: 20635
			private ToggleAction listenForGameOver;

			// Token: 0x0400509C RID: 20636
			private bool failed;

			// Token: 0x0400509D RID: 20637
			private bool runOk;
		}
	}
}
