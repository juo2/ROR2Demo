using System;

namespace RoR2.Achievements.Bandit2
{
	// Token: 0x02000F03 RID: 3843
	[RegisterAchievement("Bandit2ClearGameMonsoon", "Skins.Bandit2.Alt1", "CompleteThreeStages", null)]
	public class Bandit2ClearGameMonsoonAchievement : BasePerSurvivorClearGameMonsoonAchievement
	{
		// Token: 0x0600576D RID: 22381 RVA: 0x0016172B File Offset: 0x0015F92B
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("Bandit2Body");
		}
	}
}
