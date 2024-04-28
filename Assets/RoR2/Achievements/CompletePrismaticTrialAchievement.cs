using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E96 RID: 3734
	[RegisterAchievement("CompletePrismaticTrial", "Items.HealOnCrit", null, null)]
	public class CompletePrismaticTrialAchievement : BaseEndingAchievement
	{
		// Token: 0x06005558 RID: 21848 RVA: 0x0015E1A2 File Offset: 0x0015C3A2
		protected override bool ShouldGrant(RunReport runReport)
		{
			return runReport.gameEnding == GameEndingCatalog.FindGameEndingDef("PrismaticTrialEnding");
		}
	}
}
