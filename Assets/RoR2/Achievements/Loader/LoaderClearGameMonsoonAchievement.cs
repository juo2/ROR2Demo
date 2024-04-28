using System;

namespace RoR2.Achievements.Loader
{
	// Token: 0x02000EEA RID: 3818
	[RegisterAchievement("LoaderClearGameMonsoon", "Skins.Loader.Alt1", "DefeatSuperRoboBallBoss", null)]
	public class LoaderClearGameMonsoonAchievement : BasePerSurvivorClearGameMonsoonAchievement
	{
		// Token: 0x060056F3 RID: 22259 RVA: 0x0015E97A File Offset: 0x0015CB7A
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("LoaderBody");
		}
	}
}
