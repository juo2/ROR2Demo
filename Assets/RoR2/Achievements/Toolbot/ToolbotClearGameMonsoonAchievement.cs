using System;

namespace RoR2.Achievements.Toolbot
{
	// Token: 0x02000ED2 RID: 3794
	[RegisterAchievement("ToolbotClearGameMonsoon", "Skins.Toolbot.Alt1", "RepeatFirstTeleporter", null)]
	public class ToolbotClearGameMonsoonAchievement : BasePerSurvivorClearGameMonsoonAchievement
	{
		// Token: 0x06005668 RID: 22120 RVA: 0x0015FD82 File Offset: 0x0015DF82
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("ToolbotBody");
		}
	}
}
