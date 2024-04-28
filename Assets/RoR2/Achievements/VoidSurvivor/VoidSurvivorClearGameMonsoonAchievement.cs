using System;

namespace RoR2.Achievements.VoidSurvivor
{
	// Token: 0x02000EC9 RID: 3785
	[RegisterAchievement("VoidSurvivorClearGameMonsoon", "Skins.VoidSurvivor.Alt1", "CompleteVoidEnding", null)]
	public class VoidSurvivorClearGameMonsoonAchievement : BasePerSurvivorClearGameMonsoonAchievement
	{
		// Token: 0x0600563D RID: 22077 RVA: 0x0015FA9A File Offset: 0x0015DC9A
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("VoidSurvivorBody");
		}
	}
}
