using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F0E RID: 3854
	[RegisterAchievement("ObtainArtifactEnigma", "Artifacts.Enigma", null, null)]
	public class ObtainArtifactEnigmaAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x0600579A RID: 22426 RVA: 0x0015C3C7 File Offset: 0x0015A5C7
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.enigmaArtifactDef;
			}
		}
	}
}
