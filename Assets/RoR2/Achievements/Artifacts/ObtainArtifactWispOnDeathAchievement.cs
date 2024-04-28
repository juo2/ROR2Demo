using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F1A RID: 3866
	[RegisterAchievement("ObtainArtifactWispOnDeath", "Artifacts.WispOnDeath", null, null)]
	public class ObtainArtifactWispOnDeathAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060057B2 RID: 22450 RVA: 0x00161B90 File Offset: 0x0015FD90
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.wispOnDeath;
			}
		}
	}
}
