using System;

namespace RoR2.Achievements.Engi
{
	// Token: 0x02000EF2 RID: 3826
	[RegisterAchievement("EngiClearGameMonsoon", "Skins.Engi.Alt1", "Complete30StagesCareer", null)]
	public class EngiClearGameMonsoonAchievement : BasePerSurvivorClearGameMonsoonAchievement
	{
		// Token: 0x06005729 RID: 22313 RVA: 0x00160FED File Offset: 0x0015F1ED
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("EngiBody");
		}
	}
}
