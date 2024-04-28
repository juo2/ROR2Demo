using System;

namespace RoR2.Achievements.Huntress
{
	// Token: 0x02000EEE RID: 3822
	[RegisterAchievement("HuntressClearGameMonsoon", "Skins.Huntress.Alt1", null, null)]
	public class HuntressClearGameMonsoonAchievement : BasePerSurvivorClearGameMonsoonAchievement
	{
		// Token: 0x06005704 RID: 22276 RVA: 0x00160BDC File Offset: 0x0015EDDC
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("HuntressBody");
		}
	}
}
