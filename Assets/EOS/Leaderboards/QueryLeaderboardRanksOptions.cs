using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003D7 RID: 983
	public class QueryLeaderboardRanksOptions
	{
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x00019240 File Offset: 0x00017440
		// (set) Token: 0x060017CB RID: 6091 RVA: 0x00019248 File Offset: 0x00017448
		public string LeaderboardId { get; set; }

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x00019251 File Offset: 0x00017451
		// (set) Token: 0x060017CD RID: 6093 RVA: 0x00019259 File Offset: 0x00017459
		public ProductUserId LocalUserId { get; set; }
	}
}
