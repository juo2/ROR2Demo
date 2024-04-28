using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F12 RID: 3858
	[RegisterAchievement("ObtainArtifactMonsterTeamGainsItems", "Artifacts.MonsterTeamGainsItems", null, null)]
	public class ObtainArtifactMonsterTeamGainsItemsAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060057A2 RID: 22434 RVA: 0x0015C6B1 File Offset: 0x0015A8B1
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.monsterTeamGainsItemsArtifactDef;
			}
		}
	}
}
