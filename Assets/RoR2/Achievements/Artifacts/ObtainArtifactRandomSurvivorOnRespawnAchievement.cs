using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F13 RID: 3859
	[RegisterAchievement("ObtainArtifactRandomSurvivorOnRespawn", "Artifacts.RandomSurvivorOnRespawn", null, null)]
	public class ObtainArtifactRandomSurvivorOnRespawnAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060057A4 RID: 22436 RVA: 0x00161B74 File Offset: 0x0015FD74
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.randomSurvivorOnRespawnArtifactDef;
			}
		}
	}
}
