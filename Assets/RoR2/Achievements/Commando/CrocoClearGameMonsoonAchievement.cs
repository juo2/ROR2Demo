using System;

namespace RoR2.Achievements.Commando
{
	// Token: 0x02000F02 RID: 3842
	[RegisterAchievement("CrocoClearGameMonsoon", "Skins.Croco.Alt1", "BeatArena", null)]
	public class CrocoClearGameMonsoonAchievement : BasePerSurvivorClearGameMonsoonAchievement
	{
		// Token: 0x0600576B RID: 22379 RVA: 0x001611BF File Offset: 0x0015F3BF
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("CrocoBody");
		}
	}
}
