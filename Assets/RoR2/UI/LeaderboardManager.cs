using System;
using System.Collections.Generic;

namespace RoR2.UI
{
	// Token: 0x02000DB9 RID: 3513
	public abstract class LeaderboardManager
	{
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600506F RID: 20591 RVA: 0x0014CDC5 File Offset: 0x0014AFC5
		// (set) Token: 0x06005070 RID: 20592 RVA: 0x0014CDCD File Offset: 0x0014AFCD
		public bool IsValid { get; protected set; }

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06005071 RID: 20593 RVA: 0x0014CDD6 File Offset: 0x0014AFD6
		// (set) Token: 0x06005072 RID: 20594 RVA: 0x0014CDDE File Offset: 0x0014AFDE
		public bool IsQuerying { get; protected set; }

		// Token: 0x06005073 RID: 20595
		internal abstract void UpdateLeaderboard();

		// Token: 0x06005074 RID: 20596
		internal abstract List<LeaderboardInfo> GetLeaderboardInfoList();

		// Token: 0x06005075 RID: 20597
		internal abstract UserID GetUserID(LeaderboardInfo leaderboardInfo);

		// Token: 0x06005076 RID: 20598
		internal abstract string GetLocalUserIdString();

		// Token: 0x06005077 RID: 20599 RVA: 0x0014CDE7 File Offset: 0x0014AFE7
		protected LeaderboardManager(LeaderboardController leaderboardController)
		{
			this.LeaderboardController = leaderboardController;
		}

		// Token: 0x04004D12 RID: 19730
		protected LeaderboardController LeaderboardController;
	}
}
