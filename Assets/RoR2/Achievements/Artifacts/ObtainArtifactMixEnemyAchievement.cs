using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F11 RID: 3857
	[RegisterAchievement("ObtainArtifactMixEnemy", "Artifacts.MixEnemy", null, null)]
	public class ObtainArtifactMixEnemyAchievement : BaseObtainArtifactAchievement
	{
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060057A0 RID: 22432 RVA: 0x00161B6D File Offset: 0x0015FD6D
		protected override ArtifactDef artifactDef
		{
			get
			{
				return RoR2Content.Artifacts.mixEnemyArtifactDef;
			}
		}
	}
}
