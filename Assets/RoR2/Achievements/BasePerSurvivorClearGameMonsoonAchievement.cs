using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E76 RID: 3702
	public class BasePerSurvivorClearGameMonsoonAchievement : BaseAchievement
	{
		// Token: 0x060054CE RID: 21710 RVA: 0x0015D486 File Offset: 0x0015B686
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			Run.onClientGameOverGlobal += this.OnClientGameOverGlobal;
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x0015D49F File Offset: 0x0015B69F
		protected override void OnBodyRequirementBroken()
		{
			Run.onClientGameOverGlobal -= this.OnClientGameOverGlobal;
			base.OnBodyRequirementBroken();
		}

		// Token: 0x060054D0 RID: 21712 RVA: 0x0015D4B8 File Offset: 0x0015B6B8
		private void OnClientGameOverGlobal(Run run, RunReport runReport)
		{
			if (!runReport.gameEnding)
			{
				return;
			}
			if (runReport.gameEnding.isWin)
			{
				DifficultyDef difficultyDef = DifficultyCatalog.GetDifficultyDef(runReport.ruleBook.FindDifficulty());
				if (difficultyDef != null && difficultyDef.countsAsHardMode)
				{
					base.Grant();
				}
			}
		}
	}
}
