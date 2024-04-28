using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F17 RID: 3863
	[RegisterAchievement("ObtainArtifactSwarms", "Artifacts.Swarms", null, null)]
	public class ObtainArtifactSwarmsAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060057AC RID: 22444 RVA: 0x0015CBDD File Offset: 0x0015ADDD
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.swarmsArtifactDef;
			}
		}
	}
}
