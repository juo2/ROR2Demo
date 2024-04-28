using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F0B RID: 3851
	[RegisterAchievement("ObtainArtifactBomb", "Artifacts.Bomb", null, null)]
	public class ObtainArtifactBombAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06005794 RID: 22420 RVA: 0x0015B9A9 File Offset: 0x00159BA9
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.bombArtifactDef;
			}
		}
	}
}
