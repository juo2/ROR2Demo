using System;

namespace RoR2.Achievements.Railgunner
{
	// Token: 0x02000ED9 RID: 3801
	[RegisterAchievement("RailgunnerClearGameMonsoon", "Skins.RailGunner.Alt1", null, null)]
	public class RailgunnerClearGameMonsoonAchievement : BasePerSurvivorClearGameMonsoonAchievement
	{
		// Token: 0x0600568C RID: 22156 RVA: 0x001600A8 File Offset: 0x0015E2A8
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("RailgunnerBody");
		}
	}
}
