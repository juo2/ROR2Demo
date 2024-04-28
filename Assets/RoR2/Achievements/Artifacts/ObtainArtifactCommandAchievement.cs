using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F0C RID: 3852
	[RegisterAchievement("ObtainArtifactCommand", "Artifacts.Command", null, null)]
	public class ObtainArtifactCommandAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06005796 RID: 22422 RVA: 0x0015BD50 File Offset: 0x00159F50
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.commandArtifactDef;
			}
		}
	}
}
