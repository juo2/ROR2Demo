using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F15 RID: 3861
	[RegisterAchievement("ObtainArtifactShadowClone", "Artifacts.ShadowClone", null, null)]
	public class ObtainArtifactShadowCloneAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060057A8 RID: 22440 RVA: 0x00161B7B File Offset: 0x0015FD7B
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.shadowCloneArtifactDef;
			}
		}
	}
}
