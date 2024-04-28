using System;
using System.Collections.Generic;
using System.Linq;
using Facepunch.Steamworks.Callbacks;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x02000167 RID: 359
	public class Leaderboard : IDisposable
	{
		// Token: 0x06000A9B RID: 2715 RVA: 0x0003513C File Offset: 0x0003333C
		public ulong GetBoardId()
		{
			return this.BoardId;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00035144 File Offset: 0x00033344
		internal Leaderboard(Client c)
		{
			this.client = c;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0003515E File Offset: 0x0003335E
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x00035166 File Offset: 0x00033366
		public string Name { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0003516F File Offset: 0x0003336F
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x00035177 File Offset: 0x00033377
		public int TotalEntries { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00035180 File Offset: 0x00033380
		public bool IsValid
		{
			get
			{
				return this.BoardId > 0UL;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0003518C File Offset: 0x0003338C
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x00035194 File Offset: 0x00033394
		public bool IsError { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0003519D File Offset: 0x0003339D
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x000351A5 File Offset: 0x000333A5
		public bool IsQuerying { get; private set; }

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000351AE File Offset: 0x000333AE
		public void Dispose()
		{
			this.client = null;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000351B7 File Offset: 0x000333B7
		private void DispatchOnCreatedCallbacks()
		{
			while (this._onCreated.Count > 0)
			{
				this._onCreated.Dequeue()();
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000351DC File Offset: 0x000333DC
		private bool DeferOnCreated(Action onValid, FailureCallback onFailure = null)
		{
			if (this.IsValid || this.IsError)
			{
				return false;
			}
			this._onCreated.Enqueue(delegate
			{
				if (this.IsValid)
				{
					onValid();
					return;
				}
				FailureCallback onFailure2 = onFailure;
				if (onFailure2 == null)
				{
					return;
				}
				onFailure2(Facepunch.Steamworks.Callbacks.Result.Fail);
			});
			return true;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00035230 File Offset: 0x00033430
		internal void OnBoardCreated(LeaderboardFindResult_t result, bool error)
		{
			Console.WriteLine(string.Format("result.LeaderboardFound: {0}", result.LeaderboardFound));
			Console.WriteLine(string.Format("result.SteamLeaderboard: {0}", result.SteamLeaderboard));
			if (error || result.LeaderboardFound == 0)
			{
				this.IsError = true;
			}
			else
			{
				this.BoardId = result.SteamLeaderboard;
				if (this.IsValid)
				{
					this.Name = this.client.native.userstats.GetLeaderboardName(this.BoardId);
					this.TotalEntries = this.client.native.userstats.GetLeaderboardEntryCount(this.BoardId);
					Action onBoardInformation = this.OnBoardInformation;
					if (onBoardInformation != null)
					{
						onBoardInformation();
					}
				}
			}
			this.DispatchOnCreatedCallbacks();
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000352FC File Offset: 0x000334FC
		public bool AddScore(bool onlyIfBeatsOldScore, int score, params int[] subscores)
		{
			if (this.IsError)
			{
				return false;
			}
			if (!this.IsValid)
			{
				return this.DeferOnCreated(delegate
				{
					this.AddScore(onlyIfBeatsOldScore, score, subscores);
				}, null);
			}
			LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod = LeaderboardUploadScoreMethod.ForceUpdate;
			if (onlyIfBeatsOldScore)
			{
				eLeaderboardUploadScoreMethod = LeaderboardUploadScoreMethod.KeepBest;
			}
			this.client.native.userstats.UploadLeaderboardScore(this.BoardId, eLeaderboardUploadScoreMethod, score, subscores, subscores.Length, null);
			return true;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00035398 File Offset: 0x00033598
		public bool AddScore(bool onlyIfBeatsOldScore, int score, int[] subscores = null, Leaderboard.AddScoreCallback onSuccess = null, FailureCallback onFailure = null)
		{
			if (this.IsError)
			{
				return false;
			}
			if (!this.IsValid)
			{
				return this.DeferOnCreated(delegate
				{
					this.AddScore(onlyIfBeatsOldScore, score, subscores, onSuccess, onFailure);
				}, onFailure);
			}
			if (subscores == null)
			{
				subscores = new int[0];
			}
			LeaderboardUploadScoreMethod eLeaderboardUploadScoreMethod = LeaderboardUploadScoreMethod.ForceUpdate;
			if (onlyIfBeatsOldScore)
			{
				eLeaderboardUploadScoreMethod = LeaderboardUploadScoreMethod.KeepBest;
			}
			this.client.native.userstats.UploadLeaderboardScore(this.BoardId, eLeaderboardUploadScoreMethod, score, subscores, subscores.Length, delegate(LeaderboardScoreUploaded_t result, bool error)
			{
				if (!error && result.Success != 0)
				{
					Leaderboard.AddScoreCallback onSuccess2 = onSuccess;
					if (onSuccess2 == null)
					{
						return;
					}
					onSuccess2(new Leaderboard.AddScoreResult
					{
						Score = result.Score,
						ScoreChanged = (result.ScoreChanged > 0),
						GlobalRankNew = result.GlobalRankNew,
						GlobalRankPrevious = result.GlobalRankPrevious
					});
					return;
				}
				else
				{
					FailureCallback onFailure2 = onFailure;
					if (onFailure2 == null)
					{
						return;
					}
					onFailure2(error ? Facepunch.Steamworks.Callbacks.Result.IOFailure : Facepunch.Steamworks.Callbacks.Result.Fail);
					return;
				}
			});
			return true;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00035468 File Offset: 0x00033668
		public bool AttachRemoteFile(RemoteFile file, Leaderboard.AttachRemoteFileCallback onSuccess = null, FailureCallback onFailure = null)
		{
			if (this.IsError)
			{
				return false;
			}
			if (!this.IsValid)
			{
				return this.DeferOnCreated(delegate
				{
					this.AttachRemoteFile(file, onSuccess, onFailure);
				}, onFailure);
			}
			if (file.IsShared)
			{
				return this.client.native.userstats.AttachLeaderboardUGC(this.BoardId, file.UGCHandle, delegate(LeaderboardUGCSet_t result, bool error)
				{
					if (!error && result.Result == SteamNative.Result.OK)
					{
						Leaderboard.AttachRemoteFileCallback onSuccess2 = onSuccess;
						if (onSuccess2 == null)
						{
							return;
						}
						onSuccess2();
						return;
					}
					else
					{
						FailureCallback onFailure2 = onFailure;
						if (onFailure2 == null)
						{
							return;
						}
						onFailure2((Facepunch.Steamworks.Callbacks.Result)((result.Result == (SteamNative.Result)0) ? SteamNative.Result.IOFailure : result.Result));
						return;
					}
				}).IsValid;
			}
			file.Share(delegate
			{
				if (!file.IsShared || !this.AttachRemoteFile(file, onSuccess, onFailure))
				{
					FailureCallback onFailure2 = onFailure;
					if (onFailure2 == null)
					{
						return;
					}
					onFailure2(Facepunch.Steamworks.Callbacks.Result.Fail);
				}
			}, onFailure);
			return true;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0003552C File Offset: 0x0003372C
		public bool FetchScores(Leaderboard.RequestType RequestType, int start, int end)
		{
			if (!this.IsValid)
			{
				return false;
			}
			if (this.IsQuerying)
			{
				return false;
			}
			this.client.native.userstats.DownloadLeaderboardEntries(this.BoardId, (LeaderboardDataRequest)RequestType, start, end, new Action<LeaderboardScoresDownloaded_t, bool>(this.OnScores));
			this.Results = null;
			this.IsQuerying = true;
			return true;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0003558C File Offset: 0x0003378C
		private unsafe void ReadScores(LeaderboardScoresDownloaded_t result, List<Leaderboard.Entry> dest)
		{
			for (int i = 0; i < result.CEntryCount; i++)
			{
				int[] array;
				int* value;
				if ((array = Leaderboard.subEntriesBuffer) == null || array.Length == 0)
				{
					value = null;
				}
				else
				{
					value = &array[0];
				}
				LeaderboardEntry_t leaderboardEntry_t = default(LeaderboardEntry_t);
				if (this.client.native.userstats.GetDownloadedLeaderboardEntry(result.SteamLeaderboardEntries, i, ref leaderboardEntry_t, (IntPtr)((void*)value), Leaderboard.subEntriesBuffer.Length))
				{
					dest.Add(new Leaderboard.Entry
					{
						GlobalRank = leaderboardEntry_t.GlobalRank,
						Score = leaderboardEntry_t.Score,
						SteamId = leaderboardEntry_t.SteamIDUser,
						SubScores = ((leaderboardEntry_t.CDetails == 0) ? null : Leaderboard.subEntriesBuffer.Take(leaderboardEntry_t.CDetails).ToArray<int>()),
						Name = this.client.Friends.GetName(leaderboardEntry_t.SteamIDUser),
						AttachedFile = ((leaderboardEntry_t.UGC >> 32 == (ulong)-1) ? null : new RemoteFile(this.client.RemoteStorage, leaderboardEntry_t.UGC))
					});
				}
				array = null;
			}
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000356B8 File Offset: 0x000338B8
		public bool FetchScores(Leaderboard.RequestType RequestType, int start, int end, Leaderboard.FetchScoresCallback onSuccess, FailureCallback onFailure = null)
		{
			if (this.IsError)
			{
				return false;
			}
			if (!this.IsValid)
			{
				return this.DeferOnCreated(delegate
				{
					this.FetchScores(RequestType, start, end, onSuccess, onFailure);
				}, onFailure);
			}
			this.client.native.userstats.DownloadLeaderboardEntries(this.BoardId, (LeaderboardDataRequest)RequestType, start, end, delegate(LeaderboardScoresDownloaded_t result, bool error)
			{
				if (!error)
				{
					if (Leaderboard._sEntryBuffer == null)
					{
						Leaderboard._sEntryBuffer = new List<Leaderboard.Entry>();
					}
					else
					{
						Leaderboard._sEntryBuffer.Clear();
					}
					this.ReadScores(result, Leaderboard._sEntryBuffer);
					onSuccess(Leaderboard._sEntryBuffer.ToArray());
					return;
				}
				FailureCallback onFailure2 = onFailure;
				if (onFailure2 == null)
				{
					return;
				}
				onFailure2(Facepunch.Steamworks.Callbacks.Result.IOFailure);
			});
			return true;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00035764 File Offset: 0x00033964
		public unsafe bool FetchUsersScores(Leaderboard.RequestType RequestType, ulong[] steamIds, Leaderboard.FetchScoresCallback onSuccess, FailureCallback onFailure = null)
		{
			if (this.IsError)
			{
				return false;
			}
			if (!this.IsValid)
			{
				return this.DeferOnCreated(delegate
				{
					this.FetchUsersScores(RequestType, steamIds, onSuccess, onFailure);
				}, onFailure);
			}
			ulong[] array;
			ulong* value;
			if ((array = steamIds) == null || array.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array[0];
			}
			this.client.native.userstats.DownloadLeaderboardEntriesForUsers(this.BoardId, (IntPtr)((void*)value), steamIds.Length, delegate(LeaderboardScoresDownloaded_t result, bool error)
			{
				if (!error)
				{
					if (Leaderboard._sEntryBuffer == null)
					{
						Leaderboard._sEntryBuffer = new List<Leaderboard.Entry>();
					}
					else
					{
						Leaderboard._sEntryBuffer.Clear();
					}
					this.ReadScores(result, Leaderboard._sEntryBuffer);
					onSuccess(Leaderboard._sEntryBuffer.ToArray());
					return;
				}
				FailureCallback onFailure2 = onFailure;
				if (onFailure2 == null)
				{
					return;
				}
				onFailure2(Facepunch.Steamworks.Callbacks.Result.IOFailure);
			});
			array = null;
			return true;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00035824 File Offset: 0x00033A24
		private void OnScores(LeaderboardScoresDownloaded_t result, bool error)
		{
			this.IsQuerying = false;
			if (this.client == null)
			{
				return;
			}
			if (error)
			{
				return;
			}
			this.TotalEntries = this.client.native.userstats.GetLeaderboardEntryCount(this.BoardId);
			List<Leaderboard.Entry> list = new List<Leaderboard.Entry>();
			this.ReadScores(result, list);
			this.Results = list.ToArray();
		}

		// Token: 0x040007F6 RID: 2038
		private static readonly int[] subEntriesBuffer = new int[512];

		// Token: 0x040007F7 RID: 2039
		internal ulong BoardId;

		// Token: 0x040007F8 RID: 2040
		internal Client client;

		// Token: 0x040007F9 RID: 2041
		private readonly Queue<Action> _onCreated = new Queue<Action>();

		// Token: 0x040007FA RID: 2042
		public Leaderboard.Entry[] Results;

		// Token: 0x040007FF RID: 2047
		public Action OnBoardInformation;

		// Token: 0x04000800 RID: 2048
		[ThreadStatic]
		private static List<Leaderboard.Entry> _sEntryBuffer;

		// Token: 0x02000261 RID: 609
		public enum RequestType
		{
			// Token: 0x04000BA7 RID: 2983
			Global,
			// Token: 0x04000BA8 RID: 2984
			GlobalAroundUser,
			// Token: 0x04000BA9 RID: 2985
			Friends
		}

		// Token: 0x02000262 RID: 610
		// (Invoke) Token: 0x06001DBB RID: 7611
		public delegate void AddScoreCallback(Leaderboard.AddScoreResult result);

		// Token: 0x02000263 RID: 611
		public struct AddScoreResult
		{
			// Token: 0x04000BAA RID: 2986
			public int Score;

			// Token: 0x04000BAB RID: 2987
			public bool ScoreChanged;

			// Token: 0x04000BAC RID: 2988
			public int GlobalRankNew;

			// Token: 0x04000BAD RID: 2989
			public int GlobalRankPrevious;
		}

		// Token: 0x02000264 RID: 612
		// (Invoke) Token: 0x06001DBF RID: 7615
		public delegate void AttachRemoteFileCallback();

		// Token: 0x02000265 RID: 613
		// (Invoke) Token: 0x06001DC3 RID: 7619
		public delegate void FetchScoresCallback(Leaderboard.Entry[] results);

		// Token: 0x02000266 RID: 614
		public struct Entry
		{
			// Token: 0x04000BAE RID: 2990
			public ulong SteamId;

			// Token: 0x04000BAF RID: 2991
			public int Score;

			// Token: 0x04000BB0 RID: 2992
			public int[] SubScores;

			// Token: 0x04000BB1 RID: 2993
			public int GlobalRank;

			// Token: 0x04000BB2 RID: 2994
			public RemoteFile AttachedFile;

			// Token: 0x04000BB3 RID: 2995
			public string Name;
		}
	}
}
