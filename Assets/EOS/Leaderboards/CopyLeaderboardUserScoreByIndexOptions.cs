using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003B7 RID: 951
	public class CopyLeaderboardUserScoreByIndexOptions
	{
		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x0001859C File Offset: 0x0001679C
		// (set) Token: 0x06001713 RID: 5907 RVA: 0x000185A4 File Offset: 0x000167A4
		public uint LeaderboardUserScoreIndex { get; set; }

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x000185AD File Offset: 0x000167AD
		// (set) Token: 0x06001715 RID: 5909 RVA: 0x000185B5 File Offset: 0x000167B5
		public string StatName { get; set; }
	}
}
