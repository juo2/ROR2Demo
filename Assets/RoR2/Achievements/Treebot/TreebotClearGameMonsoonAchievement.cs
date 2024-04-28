using System;

namespace RoR2.Achievements.Treebot
{
	// Token: 0x02000ECC RID: 3788
	[RegisterAchievement("TreebotClearGameMonsoon", "Skins.Treebot.Alt1", "RescueTreebot", null)]
	public class TreebotClearGameMonsoonAchievement : BasePerSurvivorClearGameMonsoonAchievement
	{
		// Token: 0x0600564A RID: 22090 RVA: 0x0015FAAE File Offset: 0x0015DCAE
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("TreebotBody");
		}
	}
}
