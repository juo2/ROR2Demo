using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E95 RID: 3733
	[RegisterAchievement("CompleteMainEndingHard", "Items.LunarBadLuck", null, null)]
	public class CompleteMainEndingHardAchievement : BaseEndingAchievement
	{
		// Token: 0x06005556 RID: 21846 RVA: 0x0015E178 File Offset: 0x0015C378
		protected override bool ShouldGrant(RunReport runReport)
		{
			return runReport.gameEnding == RoR2Content.GameEndings.MainEnding && runReport.ruleBook.FindDifficulty() >= DifficultyIndex.Hard;
		}
	}
}
