using System;

namespace RoR2.Achievements.Commando
{
	// Token: 0x02000EFD RID: 3837
	[RegisterAchievement("CommandoClearGameMonsoon", "Skins.Commando.Alt1", null, null)]
	public class CommandoClearGameMonsoonAchievement : BasePerSurvivorClearGameMonsoonAchievement
	{
		// Token: 0x06005752 RID: 22354 RVA: 0x001613DC File Offset: 0x0015F5DC
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("CommandoBody");
		}
	}
}
