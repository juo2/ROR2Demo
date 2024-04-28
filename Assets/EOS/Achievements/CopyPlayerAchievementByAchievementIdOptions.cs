using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x020005FE RID: 1534
	public class CopyPlayerAchievementByAchievementIdOptions
	{
		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x00027C70 File Offset: 0x00025E70
		// (set) Token: 0x06002563 RID: 9571 RVA: 0x00027C78 File Offset: 0x00025E78
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x00027C81 File Offset: 0x00025E81
		// (set) Token: 0x06002565 RID: 9573 RVA: 0x00027C89 File Offset: 0x00025E89
		public string AchievementId { get; set; }

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x00027C92 File Offset: 0x00025E92
		// (set) Token: 0x06002567 RID: 9575 RVA: 0x00027C9A File Offset: 0x00025E9A
		public ProductUserId LocalUserId { get; set; }
	}
}
