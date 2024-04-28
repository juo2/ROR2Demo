using System;

namespace RoR2.Achievements.Merc
{
	// Token: 0x02000EDC RID: 3804
	[RegisterAchievement("MercClearGameMonsoon", "Skins.Merc.Alt1", "CompleteUnknownEnding", null)]
	public class MercClearGameMonsoonAchievement : BasePerSurvivorClearGameMonsoonAchievement
	{
		// Token: 0x06005699 RID: 22169 RVA: 0x001602A3 File Offset: 0x0015E4A3
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("MercBody");
		}
	}
}
