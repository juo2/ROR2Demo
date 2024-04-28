using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F14 RID: 3860
	[RegisterAchievement("ObtainArtifactSacrifice", "Artifacts.Sacrifice", null, null)]
	public class ObtainArtifactSacrificeAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060057A6 RID: 22438 RVA: 0x0015C94C File Offset: 0x0015AB4C
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.sacrificeArtifactDef;
			}
		}
	}
}
