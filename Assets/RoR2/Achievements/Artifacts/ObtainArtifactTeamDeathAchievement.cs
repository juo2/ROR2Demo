using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F18 RID: 3864
	[RegisterAchievement("ObtainArtifactTeamDeath", "Artifacts.TeamDeath", null, null)]
	public class ObtainArtifactTeamDeathAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060057AE RID: 22446 RVA: 0x0015CD9C File Offset: 0x0015AF9C
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.teamDeathArtifactDef;
			}
		}
	}
}
