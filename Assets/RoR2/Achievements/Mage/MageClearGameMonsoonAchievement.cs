using System;

namespace RoR2.Achievements.Mage
{
	// Token: 0x02000EE3 RID: 3811
	[RegisterAchievement("MageClearGameMonsoon", "Skins.Mage.Alt1", "FreeMage", null)]
	public class MageClearGameMonsoonAchievement : BasePerSurvivorClearGameMonsoonAchievement
	{
		// Token: 0x060056CB RID: 22219 RVA: 0x00160754 File Offset: 0x0015E954
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("MageBody");
		}
	}
}
