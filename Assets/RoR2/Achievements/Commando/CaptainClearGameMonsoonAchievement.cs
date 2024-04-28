using System;

namespace RoR2.Achievements.Commando
{
	// Token: 0x02000EFC RID: 3836
	[RegisterAchievement("CaptainClearGameMonsoon", "Skins.Captain.Alt1", "CompleteMainEnding", null)]
	public class CaptainClearGameMonsoonAchievement : BasePerSurvivorClearGameMonsoonAchievement
	{
		// Token: 0x06005750 RID: 22352 RVA: 0x0015D615 File Offset: 0x0015B815
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("CaptainBody");
		}
	}
}
