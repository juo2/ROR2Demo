using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E94 RID: 3732
	[RegisterAchievement("CompleteMainEnding", "Characters.Captain", null, null)]
	public class CompleteMainEndingAchievement : BaseEndingAchievement
	{
		// Token: 0x06005554 RID: 21844 RVA: 0x0015E159 File Offset: 0x0015C359
		protected override bool ShouldGrant(RunReport runReport)
		{
			return runReport.gameEnding == RoR2Content.GameEndings.MainEnding;
		}
	}
}
