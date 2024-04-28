using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003D9 RID: 985
	public class QueryLeaderboardUserScoresOptions
	{
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x000192CC File Offset: 0x000174CC
		// (set) Token: 0x060017D5 RID: 6101 RVA: 0x000192D4 File Offset: 0x000174D4
		public ProductUserId[] UserIds { get; set; }

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x000192DD File Offset: 0x000174DD
		// (set) Token: 0x060017D7 RID: 6103 RVA: 0x000192E5 File Offset: 0x000174E5
		public UserScoresQueryStatInfo[] StatInfo { get; set; }

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x000192EE File Offset: 0x000174EE
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x000192F6 File Offset: 0x000174F6
		public DateTimeOffset? StartTime { get; set; }

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x000192FF File Offset: 0x000174FF
		// (set) Token: 0x060017DB RID: 6107 RVA: 0x00019307 File Offset: 0x00017507
		public DateTimeOffset? EndTime { get; set; }

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x00019310 File Offset: 0x00017510
		// (set) Token: 0x060017DD RID: 6109 RVA: 0x00019318 File Offset: 0x00017518
		public ProductUserId LocalUserId { get; set; }
	}
}
