using System;
using System.Collections.Generic;

namespace RoR2.UI
{
	// Token: 0x02000DBA RID: 3514
	internal static class LeaderboardExtensions
	{
		// Token: 0x06005078 RID: 20600 RVA: 0x0014CDF6 File Offset: 0x0014AFF6
		internal static void SetLeaderboardInfo(this List<LeaderboardInfo> leaderboardInfoList, LeaderboardInfo[] leaderboardInfos)
		{
			leaderboardInfoList.Clear();
			leaderboardInfoList.AddRange(leaderboardInfos);
		}
	}
}
