using System;
using System.Collections.Generic;
using System.Linq;
using Facepunch.Steamworks;
using Facepunch.Steamworks.Callbacks;

namespace RoR2.UI
{
	// Token: 0x02000DBB RID: 3515
	public class SteamLeaderboardManager : LeaderboardManager
	{
		// Token: 0x06005079 RID: 20601 RVA: 0x0014CE05 File Offset: 0x0014B005
		public SteamLeaderboardManager(LeaderboardController leaderboardController) : base(leaderboardController)
		{
			base.IsValid = true;
		}

		// Token: 0x0600507A RID: 20602 RVA: 0x0014CE20 File Offset: 0x0014B020
		internal override List<LeaderboardInfo> GetLeaderboardInfoList()
		{
			return this._leaderboardInfoList;
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x0014CE28 File Offset: 0x0014B028
		internal override UserID GetUserID(LeaderboardInfo leaderboardInfo)
		{
			return new UserID(Convert.ToUInt64(leaderboardInfo.userID));
		}

		// Token: 0x0600507C RID: 20604 RVA: 0x0014CE3C File Offset: 0x0014B03C
		internal override string GetLocalUserIdString()
		{
			return Client.Instance.SteamId.ToString();
		}

		// Token: 0x0600507D RID: 20605 RVA: 0x0014CE5C File Offset: 0x0014B05C
		internal override void UpdateLeaderboard()
		{
			this._currentLeaderboard = Client.Instance.GetLeaderboard(this.LeaderboardController.currentLeaderboardName, Client.LeaderboardSortMethod.None, Client.LeaderboardDisplayType.None);
			int num = this.LeaderboardController.currentPage * this.LeaderboardController.entriesPerPage - this.LeaderboardController.entriesPerPage / 2;
			base.IsValid = (this._currentLeaderboard != null && this._currentLeaderboard.IsValid);
			base.IsQuerying = true;
			this._currentLeaderboard.FetchScores((Leaderboard.RequestType)this.LeaderboardController.currentRequestType, num, num + this.LeaderboardController.entriesPerPage, delegate(Leaderboard.Entry[] entries)
			{
				this._leaderboardInfoList.SetLeaderboardInfo(entries.Select(new Func<Leaderboard.Entry, LeaderboardInfo>(SteamLeaderboardManager.LeaderboardInfoFromSteamLeaderboardEntry)).ToArray<LeaderboardInfo>());
				base.IsQuerying = false;
				base.IsValid = this._currentLeaderboard.IsValid;
			}, delegate(Result reason)
			{
			});
		}

		// Token: 0x0600507E RID: 20606 RVA: 0x0014CF20 File Offset: 0x0014B120
		private static LeaderboardInfo LeaderboardInfoFromSteamLeaderboardEntry(Leaderboard.Entry entry)
		{
			SurvivorIndex value = SurvivorIndex.None;
			int num = (entry.SubScores != null && entry.SubScores.Length >= 1) ? entry.SubScores[1] : 0;
			if (num >= 0 && num < SurvivorCatalog.survivorCount)
			{
				value = (SurvivorIndex)num;
			}
			return new LeaderboardInfo
			{
				timeInSeconds = (float)entry.Score * 0.001f,
				survivorIndex = new SurvivorIndex?(value),
				userID = entry.SteamId.ToString(),
				rank = entry.GlobalRank
			};
		}

		// Token: 0x04004D15 RID: 19733
		private Leaderboard _currentLeaderboard;

		// Token: 0x04004D16 RID: 19734
		private List<LeaderboardInfo> _leaderboardInfoList = new List<LeaderboardInfo>();
	}
}
