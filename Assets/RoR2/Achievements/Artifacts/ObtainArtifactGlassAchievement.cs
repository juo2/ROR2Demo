using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F10 RID: 3856
	[RegisterAchievement("ObtainArtifactGlass", "Artifacts.Glass", null, null)]
	public class ObtainArtifactGlassAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x0600579E RID: 22430 RVA: 0x00161B66 File Offset: 0x0015FD66
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.glassArtifactDef;
			}
		}
	}
}
