using System;
using JetBrains.Annotations;

namespace RoR2
{
	// Token: 0x020004C3 RID: 1219
	[MeansImplicitUse]
	public class RegisterAchievementAttribute : Attribute
	{
		// Token: 0x06001619 RID: 5657 RVA: 0x00061BE9 File Offset: 0x0005FDE9
		public RegisterAchievementAttribute([NotNull] string identifier, string unlockableRewardIdentifier, string prerequisiteAchievementIdentifier, Type serverTrackerType = null)
		{
			this.identifier = identifier;
			this.unlockableRewardIdentifier = unlockableRewardIdentifier;
			this.prerequisiteAchievementIdentifier = prerequisiteAchievementIdentifier;
			this.serverTrackerType = serverTrackerType;
		}

		// Token: 0x04001BD8 RID: 7128
		public readonly string identifier;

		// Token: 0x04001BD9 RID: 7129
		public readonly string unlockableRewardIdentifier;

		// Token: 0x04001BDA RID: 7130
		public readonly string prerequisiteAchievementIdentifier;

		// Token: 0x04001BDB RID: 7131
		public readonly Type serverTrackerType;
	}
}
