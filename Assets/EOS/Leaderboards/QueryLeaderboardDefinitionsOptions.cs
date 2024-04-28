using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003D5 RID: 981
	public class QueryLeaderboardDefinitionsOptions
	{
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x00019194 File Offset: 0x00017394
		// (set) Token: 0x060017BE RID: 6078 RVA: 0x0001919C File Offset: 0x0001739C
		public DateTimeOffset? StartTime { get; set; }

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x000191A5 File Offset: 0x000173A5
		// (set) Token: 0x060017C0 RID: 6080 RVA: 0x000191AD File Offset: 0x000173AD
		public DateTimeOffset? EndTime { get; set; }

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x000191B6 File Offset: 0x000173B6
		// (set) Token: 0x060017C2 RID: 6082 RVA: 0x000191BE File Offset: 0x000173BE
		public ProductUserId LocalUserId { get; set; }
	}
}
