using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F19 RID: 3865
	[RegisterAchievement("ObtainArtifactWeakAssKnees", "Artifacts.WeakAssKnees", null, null)]
	public class ObtainArtifactWeakAssKneesAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060057B0 RID: 22448 RVA: 0x00161B89 File Offset: 0x0015FD89
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.weakAssKneesArtifactDef;
			}
		}
	}
}
