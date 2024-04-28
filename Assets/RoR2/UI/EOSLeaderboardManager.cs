using System;
using System.Collections.Generic;
using System.Linq;
using Epic.OnlineServices;
using Epic.OnlineServices.Leaderboards;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000DB5 RID: 3509
	public class EOSLeaderboardManager : LeaderboardManager
	{
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06005057 RID: 20567 RVA: 0x000EABA1 File Offset: 0x000E8DA1
		private static ProductUserId LocalUserId
		{
			get
			{
				return EOSLoginManager.loggedInProductId;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06005058 RID: 20568 RVA: 0x0014C756 File Offset: 0x0014A956
		private static DateTime StartTime
		{
			get
			{
				return WeeklyRun.GetSeedCycleStartDateTime();
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06005059 RID: 20569 RVA: 0x0014C75D File Offset: 0x0014A95D
		private static DateTime EndTime
		{
			get
			{
				return WeeklyRun.GetSeedCycleEndDateTime();
			}
		}

		// Token: 0x0600505A RID: 20570 RVA: 0x0014C764 File Offset: 0x0014A964
		public EOSLeaderboardManager(LeaderboardController leaderboardController) : base(leaderboardController)
		{
			this._leaderboardsInterface = EOSPlatformManager.GetPlatformInterface().GetLeaderboardsInterface();
			this._leaderboardsInterface.QueryLeaderboardDefinitions(new QueryLeaderboardDefinitionsOptions
			{
				LocalUserId = EOSLeaderboardManager.LocalUserId
			}, null, new OnQueryLeaderboardDefinitionsCompleteCallback(this.LeaderboardQueryComplete));
			base.IsQuerying = true;
		}

		// Token: 0x0600505B RID: 20571 RVA: 0x0014C7CD File Offset: 0x0014A9CD
		private void LeaderboardQueryComplete(OnQueryLeaderboardDefinitionsCompleteCallbackInfo data)
		{
			base.IsValid = (data.ResultCode == Result.Success);
			base.IsQuerying = false;
		}

		// Token: 0x0600505C RID: 20572 RVA: 0x0014C7E8 File Offset: 0x0014A9E8
		internal override void UpdateLeaderboard()
		{
			RequestType currentRequestType = this.LeaderboardController.currentRequestType;
			if (currentRequestType > RequestType.GlobalAroundUser)
			{
				if (currentRequestType != RequestType.Friends)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.QueryFriendsLeaderboard();
				EOSLeaderboardManager._isGlobal = false;
			}
			else
			{
				this.QueryLeaderboardRanks();
				EOSLeaderboardManager._isGlobal = true;
			}
			base.IsQuerying = true;
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x0014C834 File Offset: 0x0014AA34
		private void QueryFriendsLeaderboard()
		{
			ProductUserId[] userIds = EOSFriendsManager.FriendsProductUserIds.ToArray();
			this._leaderboardsInterface.QueryLeaderboardUserScores(new QueryLeaderboardUserScoresOptions
			{
				StartTime = new DateTimeOffset?(EOSLeaderboardManager.StartTime),
				EndTime = new DateTimeOffset?(EOSLeaderboardManager.EndTime),
				LocalUserId = EOSLeaderboardManager.LocalUserId,
				StatInfo = new UserScoresQueryStatInfo[]
				{
					new UserScoresQueryStatInfo
					{
						Aggregation = LeaderboardAggregation.Min,
						StatName = "FASTESTWEEKLYRUN"
					}
				},
				UserIds = userIds
			}, "FASTESTWEEKLYRUN", new OnQueryLeaderboardUserScoresCompleteCallback(this.FriendsQueryComplete));
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x0014C8D4 File Offset: 0x0014AAD4
		private void FriendsQueryComplete(OnQueryLeaderboardUserScoresCompleteCallbackInfo data)
		{
			object clientData = data.ClientData;
			string text = ((clientData != null) ? clientData.ToString() : null) ?? "";
			uint leaderboardUserScoreCount = this._leaderboardsInterface.GetLeaderboardUserScoreCount(new GetLeaderboardUserScoreCountOptions
			{
				StatName = text
			});
			this.UpdateFriendsLeaderboardList(text, leaderboardUserScoreCount);
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x0014C920 File Offset: 0x0014AB20
		private void UpdateFriendsLeaderboardList(string leaderboardName, uint leaderboardMax)
		{
			int num = this.LeaderboardController.currentPage * this.LeaderboardController.entriesPerPage;
			long num2 = (long)(((long)(num + this.LeaderboardController.entriesPerPage) > (long)((ulong)leaderboardMax)) ? ((ulong)leaderboardMax - (ulong)((long)num)) : ((ulong)((long)this.LeaderboardController.entriesPerPage)));
			this._currentFriendsLeaderboardList.Clear();
			int num3 = num;
			while ((long)num3 < num2)
			{
				this.GetFriendsLeaderboardEntry(Convert.ToUInt32(num3), leaderboardName);
				num3++;
			}
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x0014C994 File Offset: 0x0014AB94
		private void GetFriendsLeaderboardEntry(uint leaderboardRecordIndex, string statName)
		{
			LeaderboardUserScore leaderboardUserScore;
			if (this._leaderboardsInterface.CopyLeaderboardUserScoreByIndex(new CopyLeaderboardUserScoreByIndexOptions
			{
				LeaderboardUserScoreIndex = leaderboardRecordIndex,
				StatName = statName
			}, out leaderboardUserScore) == Result.Success && leaderboardUserScore != null)
			{
				Debug.Log(string.Format("Leaderboard user score {0}, Index {1} successfully fetched!", leaderboardUserScore, leaderboardRecordIndex) + string.Format("\n {0}: {1}", "Score", leaderboardUserScore.Score) + string.Format("\n {0}: {1}", "UserId", leaderboardUserScore.UserId));
				UserManagerEOS userManagerEOS = PlatformSystems.userManager as UserManagerEOS;
				if (userManagerEOS != null)
				{
					userManagerEOS.QueryForDisplayNames(leaderboardUserScore.UserId, delegate()
					{
						base.IsQuerying = false;
					});
				}
				this._currentFriendsLeaderboardList.Add(leaderboardUserScore);
			}
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x0014CA46 File Offset: 0x0014AC46
		private void QueryLeaderboardRanks()
		{
			this._leaderboardsInterface.QueryLeaderboardRanks(new QueryLeaderboardRanksOptions
			{
				LeaderboardId = "PrismaticTrials",
				LocalUserId = EOSLeaderboardManager.LocalUserId
			}, this.LeaderboardController.currentLeaderboardName, new OnQueryLeaderboardRanksCompleteCallback(this.GlobalQueryComplete));
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x0014CA88 File Offset: 0x0014AC88
		private void GlobalQueryComplete(OnQueryLeaderboardRanksCompleteCallbackInfo data)
		{
			object clientData = data.ClientData;
			string leaderboardName = ((clientData != null) ? clientData.ToString() : null) ?? "";
			uint leaderboardRecordCount = this._leaderboardsInterface.GetLeaderboardRecordCount(new GetLeaderboardRecordCountOptions());
			this.UpdateGlobalLeaderboardList(leaderboardName, leaderboardRecordCount);
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x0014CACC File Offset: 0x0014ACCC
		private void UpdateGlobalLeaderboardList(string leaderboardName, uint leaderboardMax)
		{
			int num = this.LeaderboardController.currentPage * this.LeaderboardController.entriesPerPage;
			long num2 = (long)(((long)(num + this.LeaderboardController.entriesPerPage) > (long)((ulong)leaderboardMax)) ? ((ulong)leaderboardMax - (ulong)((long)num)) : ((ulong)((long)this.LeaderboardController.entriesPerPage)));
			this._currentGlobalLeaderboardList.Clear();
			int num3 = num;
			while ((long)num3 < num2)
			{
				this.GetGlobalLeaderboardEntry(Convert.ToUInt32(num3), leaderboardName);
				num3++;
			}
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x0014CB40 File Offset: 0x0014AD40
		private void GetGlobalLeaderboardEntry(uint leaderboardRecordIndex, string leaderboardName)
		{
			LeaderboardRecord leaderboardRecord;
			if (this._leaderboardsInterface.CopyLeaderboardRecordByIndex(new CopyLeaderboardRecordByIndexOptions
			{
				LeaderboardRecordIndex = leaderboardRecordIndex
			}, out leaderboardRecord) == Result.Success && leaderboardRecord != null)
			{
				Debug.Log(string.Concat(new string[]
				{
					string.Format("Leaderboard {0}, Index {1} successfully fetched!", leaderboardName, leaderboardRecordIndex),
					"\n UserDisplayName: ",
					leaderboardRecord.UserDisplayName,
					string.Format("\n {0}: {1}", "UserId", leaderboardRecord.UserId),
					string.Format("\n {0}: {1}", "Rank", leaderboardRecord.Rank),
					string.Format("\n {0}: {1}", "Score", leaderboardRecord.Score)
				}));
				UserManagerEOS userManagerEOS = PlatformSystems.userManager as UserManagerEOS;
				if (userManagerEOS != null)
				{
					userManagerEOS.QueryForDisplayNames(leaderboardRecord.UserId, delegate()
					{
						base.IsQuerying = false;
					});
				}
				this._currentGlobalLeaderboardList.Add(leaderboardRecord);
			}
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x0014CC2C File Offset: 0x0014AE2C
		internal override List<LeaderboardInfo> GetLeaderboardInfoList()
		{
			if (EOSLeaderboardManager._isGlobal)
			{
				return (from leaderboardRecord in this._currentGlobalLeaderboardList
				select new LeaderboardInfo
				{
					rank = Convert.ToInt32(leaderboardRecord.Rank),
					survivorIndex = null,
					timeInSeconds = (float)leaderboardRecord.Score,
					userID = leaderboardRecord.UserId.ToString()
				}).ToList<LeaderboardInfo>();
			}
			this._currentFriendsLeaderboardList.Sort((LeaderboardUserScore x, LeaderboardUserScore y) => x.Score.CompareTo(y.Score));
			return this._currentFriendsLeaderboardList.Select((LeaderboardUserScore leaderboardUserScore, int index) => new LeaderboardInfo
			{
				rank = index + 1,
				survivorIndex = null,
				timeInSeconds = (float)leaderboardUserScore.Score,
				userID = leaderboardUserScore.UserId.ToString()
			}).ToList<LeaderboardInfo>();
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x0014CCC9 File Offset: 0x0014AEC9
		internal override UserID GetUserID(LeaderboardInfo leaderboardInfo)
		{
			return new UserID(ProductUserId.FromString(leaderboardInfo.userID));
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x0014CCDB File Offset: 0x0014AEDB
		internal override string GetLocalUserIdString()
		{
			return EOSLeaderboardManager.LocalUserId.ToString();
		}

		// Token: 0x04004D01 RID: 19713
		public const string kPrismaticTrialsLeaderboardId = "PrismaticTrials";

		// Token: 0x04004D02 RID: 19714
		private readonly LeaderboardsInterface _leaderboardsInterface;

		// Token: 0x04004D03 RID: 19715
		private readonly List<LeaderboardRecord> _currentGlobalLeaderboardList = new List<LeaderboardRecord>();

		// Token: 0x04004D04 RID: 19716
		private readonly List<LeaderboardUserScore> _currentFriendsLeaderboardList = new List<LeaderboardUserScore>();

		// Token: 0x04004D05 RID: 19717
		private static bool _isGlobal;
	}
}
