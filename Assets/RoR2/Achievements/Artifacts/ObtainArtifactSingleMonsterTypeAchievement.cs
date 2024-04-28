using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F16 RID: 3862
	[RegisterAchievement("ObtainArtifactSingleMonsterType", "Artifacts.SingleMonsterType", null, null)]
	public class ObtainArtifactSingleMonsterTypeAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060057AA RID: 22442 RVA: 0x00161B82 File Offset: 0x0015FD82
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.singleMonsterTypeArtifactDef;
			}
		}
	}
}
