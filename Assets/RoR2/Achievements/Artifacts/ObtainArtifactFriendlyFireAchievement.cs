using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F0F RID: 3855
	[RegisterAchievement("ObtainArtifactFriendlyFire", "Artifacts.FriendlyFire", null, null)]
	public class ObtainArtifactFriendlyFireAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x0600579C RID: 22428 RVA: 0x0015C65A File Offset: 0x0015A85A
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.friendlyFireArtifactDef;
			}
		}
	}
}
