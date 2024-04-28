using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003B9 RID: 953
	public class CopyLeaderboardUserScoreByUserIdOptions
	{
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x00018616 File Offset: 0x00016816
		// (set) Token: 0x0600171D RID: 5917 RVA: 0x0001861E File Offset: 0x0001681E
		public ProductUserId UserId { get; set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x00018627 File Offset: 0x00016827
		// (set) Token: 0x0600171F RID: 5919 RVA: 0x0001862F File Offset: 0x0001682F
		public string StatName { get; set; }
	}
}
