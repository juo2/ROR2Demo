using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F0D RID: 3853
	[RegisterAchievement("ObtainArtifactEliteOnly", "Artifacts.EliteOnly", null, null)]
	public class ObtainArtifactAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06005798 RID: 22424 RVA: 0x00161B5F File Offset: 0x0015FD5F
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.eliteOnlyArtifactDef;
			}
		}
	}
}
