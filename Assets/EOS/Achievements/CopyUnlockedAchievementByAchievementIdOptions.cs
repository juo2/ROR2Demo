using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000602 RID: 1538
	public class CopyUnlockedAchievementByAchievementIdOptions
	{
		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x0600257C RID: 9596 RVA: 0x00027DE6 File Offset: 0x00025FE6
		// (set) Token: 0x0600257D RID: 9597 RVA: 0x00027DEE File Offset: 0x00025FEE
		public ProductUserId UserId { get; set; }

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x00027DF7 File Offset: 0x00025FF7
		// (set) Token: 0x0600257F RID: 9599 RVA: 0x00027DFF File Offset: 0x00025FFF
		public string AchievementId { get; set; }
	}
}
