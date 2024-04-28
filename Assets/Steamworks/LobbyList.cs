using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x02000169 RID: 361
	public class LobbyList : IDisposable
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x000363AB File Offset: 0x000345AB
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x000363B3 File Offset: 0x000345B3
		public List<LobbyList.Lobby> Lobbies { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x000363BC File Offset: 0x000345BC
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x000363C4 File Offset: 0x000345C4
		public bool Finished { get; private set; }

		// Token: 0x06000AE4 RID: 2788 RVA: 0x000363D0 File Offset: 0x000345D0
		internal LobbyList(Client client)
		{
			this.client = client;
			this.Lobbies = new List<LobbyList.Lobby>();
			this.requests = new List<ulong>();
			client.RegisterCallback<LobbyDataUpdate_t>(new Action<LobbyDataUpdate_t>(this.OnLobbyDataUpdated));
			client.RegisterCallback<LobbyDataUpdate_t>(new Action<LobbyDataUpdate_t>(this.HandleLobbyRefresh));
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00036430 File Offset: 0x00034630
		public void Refresh(LobbyList.Filter filter = null)
		{
			this.Lobbies.Clear();
			this.requests.Clear();
			this.Finished = false;
			if (filter == null)
			{
				filter = new LobbyList.Filter();
				filter.StringFilters.Add("appid", this.client.AppId.ToString());
				this.client.native.matchmaking.RequestLobbyList(new Action<LobbyMatchList_t, bool>(this.OnLobbyList));
				return;
			}
			this.client.native.matchmaking.AddRequestLobbyListDistanceFilter((LobbyDistanceFilter)filter.DistanceFilter);
			if (filter.SlotsAvailable != null)
			{
				this.client.native.matchmaking.AddRequestLobbyListFilterSlotsAvailable(filter.SlotsAvailable.Value);
			}
			if (filter.MaxResults != null)
			{
				this.client.native.matchmaking.AddRequestLobbyListResultCountFilter(filter.MaxResults.Value);
			}
			foreach (KeyValuePair<string, string> keyValuePair in filter.StringFilters)
			{
				this.client.native.matchmaking.AddRequestLobbyListStringFilter(keyValuePair.Key, keyValuePair.Value, LobbyComparison.Equal);
			}
			foreach (KeyValuePair<string, int> keyValuePair2 in filter.NearFilters)
			{
				this.client.native.matchmaking.AddRequestLobbyListNearValueFilter(keyValuePair2.Key, keyValuePair2.Value);
			}
			this.client.native.matchmaking.RequestLobbyList(new Action<LobbyMatchList_t, bool>(this.OnLobbyList));
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00036610 File Offset: 0x00034810
		private void OnLobbyList(LobbyMatchList_t callback, bool error)
		{
			if (error)
			{
				return;
			}
			uint lobbiesMatching = callback.LobbiesMatching;
			int num = 0;
			while ((long)num < (long)((ulong)lobbiesMatching))
			{
				ulong lobbyByIndex = this.client.native.matchmaking.GetLobbyByIndex(num);
				this.requests.Add(lobbyByIndex);
				LobbyList.Lobby lobby = LobbyList.Lobby.FromSteam(this.client, lobbyByIndex);
				if (lobby.Name != "")
				{
					this.Lobbies.Add(lobby);
					this.checkFinished();
				}
				else
				{
					this.client.native.matchmaking.RequestLobbyData(lobbyByIndex);
				}
				num++;
			}
			this.checkFinished();
			if (this.OnLobbiesUpdated != null)
			{
				this.OnLobbiesUpdated();
			}
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000366C1 File Offset: 0x000348C1
		private void checkFinished()
		{
			if (this.Lobbies.Count == this.requests.Count)
			{
				this.Finished = true;
				return;
			}
			this.Finished = false;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000366EC File Offset: 0x000348EC
		private void OnLobbyDataUpdated(LobbyDataUpdate_t callback)
		{
			if (callback.Success == 1)
			{
				if (this.Lobbies.Find((LobbyList.Lobby x) => x.LobbyID == callback.SteamIDLobby) == null)
				{
					this.Lobbies.Add(LobbyList.Lobby.FromSteam(this.client, callback.SteamIDLobby));
					this.checkFinished();
				}
				if (this.OnLobbiesUpdated != null)
				{
					this.OnLobbiesUpdated();
				}
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00036767 File Offset: 0x00034967
		public void Dispose()
		{
			this.client = null;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00036770 File Offset: 0x00034970
		public Task<LobbyList.Lobby> GetLobby(ulong lobbyId)
		{
			if (this.refreshTasks.ContainsKey(lobbyId))
			{
				return null;
			}
			TaskCompletionSource<LobbyList.Lobby> taskCompletionSource = new TaskCompletionSource<LobbyList.Lobby>();
			this.refreshTasks[lobbyId] = taskCompletionSource;
			this.client.native.matchmaking.RequestLobbyData(lobbyId);
			return taskCompletionSource.Task;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000367C4 File Offset: 0x000349C4
		private void HandleLobbyRefresh(LobbyDataUpdate_t data)
		{
			if (data.SteamIDLobby != data.SteamIDMember)
			{
				return;
			}
			TaskCompletionSource<LobbyList.Lobby> taskCompletionSource;
			if (this.refreshTasks.TryGetValue(data.SteamIDLobby, out taskCompletionSource))
			{
				LobbyList.Lobby result = null;
				if (data.Success != 0)
				{
					result = LobbyList.Lobby.FromSteam(this.client, data.SteamIDLobby);
				}
				this.refreshTasks.Remove(data.SteamIDLobby);
				taskCompletionSource.SetResult(result);
			}
		}

		// Token: 0x04000811 RID: 2065
		internal Client client;

		// Token: 0x04000814 RID: 2068
		internal List<ulong> requests;

		// Token: 0x04000815 RID: 2069
		public Action OnLobbiesUpdated;

		// Token: 0x04000816 RID: 2070
		private Dictionary<ulong, TaskCompletionSource<LobbyList.Lobby>> refreshTasks = new Dictionary<ulong, TaskCompletionSource<LobbyList.Lobby>>();

		// Token: 0x02000270 RID: 624
		public class Filter
		{
			// Token: 0x170000BC RID: 188
			// (get) Token: 0x06001DDD RID: 7645 RVA: 0x000647B2 File Offset: 0x000629B2
			// (set) Token: 0x06001DDE RID: 7646 RVA: 0x000647BA File Offset: 0x000629BA
			public int? SlotsAvailable { get; set; }

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x06001DDF RID: 7647 RVA: 0x000647C3 File Offset: 0x000629C3
			// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x000647CB File Offset: 0x000629CB
			public int? MaxResults { get; set; }

			// Token: 0x04000BDF RID: 3039
			public Dictionary<string, string> StringFilters = new Dictionary<string, string>();

			// Token: 0x04000BE0 RID: 3040
			public Dictionary<string, int> NearFilters = new Dictionary<string, int>();

			// Token: 0x04000BE1 RID: 3041
			public LobbyList.Filter.Distance DistanceFilter = LobbyList.Filter.Distance.Worldwide;

			// Token: 0x020002B9 RID: 697
			public enum Distance
			{
				// Token: 0x04000D35 RID: 3381
				Close,
				// Token: 0x04000D36 RID: 3382
				Default,
				// Token: 0x04000D37 RID: 3383
				Far,
				// Token: 0x04000D38 RID: 3384
				Worldwide
			}

			// Token: 0x020002BA RID: 698
			public enum Comparison
			{
				// Token: 0x04000D3A RID: 3386
				EqualToOrLessThan = -2,
				// Token: 0x04000D3B RID: 3387
				LessThan,
				// Token: 0x04000D3C RID: 3388
				Equal,
				// Token: 0x04000D3D RID: 3389
				GreaterThan,
				// Token: 0x04000D3E RID: 3390
				EqualToOrGreaterThan,
				// Token: 0x04000D3F RID: 3391
				NotEqual
			}
		}

		// Token: 0x02000271 RID: 625
		public class Lobby
		{
			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x000647F9 File Offset: 0x000629F9
			// (set) Token: 0x06001DE3 RID: 7651 RVA: 0x00064801 File Offset: 0x00062A01
			public string Name { get; private set; }

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x0006480A File Offset: 0x00062A0A
			// (set) Token: 0x06001DE5 RID: 7653 RVA: 0x00064812 File Offset: 0x00062A12
			public ulong LobbyID { get; private set; }

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0006481B File Offset: 0x00062A1B
			// (set) Token: 0x06001DE7 RID: 7655 RVA: 0x00064823 File Offset: 0x00062A23
			public ulong Owner { get; private set; }

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x0006482C File Offset: 0x00062A2C
			// (set) Token: 0x06001DE9 RID: 7657 RVA: 0x00064834 File Offset: 0x00062A34
			public int MemberLimit { get; private set; }

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06001DEA RID: 7658 RVA: 0x0006483D File Offset: 0x00062A3D
			// (set) Token: 0x06001DEB RID: 7659 RVA: 0x00064845 File Offset: 0x00062A45
			public int NumMembers { get; private set; }

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x06001DEC RID: 7660 RVA: 0x0006484E File Offset: 0x00062A4E
			// (set) Token: 0x06001DED RID: 7661 RVA: 0x00064856 File Offset: 0x00062A56
			public string LobbyType { get; private set; }

			// Token: 0x06001DEE RID: 7662 RVA: 0x00064860 File Offset: 0x00062A60
			public string GetData(string k)
			{
				string result;
				if (this.lobbyData.TryGetValue(k, out result))
				{
					return result;
				}
				return string.Empty;
			}

			// Token: 0x06001DEF RID: 7663 RVA: 0x00064884 File Offset: 0x00062A84
			public Dictionary<string, string> GetAllData()
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (KeyValuePair<string, string> keyValuePair in this.lobbyData)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
				return dictionary;
			}

			// Token: 0x06001DF0 RID: 7664 RVA: 0x000648EC File Offset: 0x00062AEC
			internal static LobbyList.Lobby FromSteam(Client client, ulong lobby)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				int lobbyDataCount = client.native.matchmaking.GetLobbyDataCount(lobby);
				for (int i = 0; i < lobbyDataCount; i++)
				{
					string key;
					string value;
					if (client.native.matchmaking.GetLobbyDataByIndex(lobby, i, out key, out value))
					{
						dictionary.Add(key, value);
					}
				}
				return new LobbyList.Lobby
				{
					Client = client,
					LobbyID = lobby,
					Name = client.native.matchmaking.GetLobbyData(lobby, "name"),
					LobbyType = client.native.matchmaking.GetLobbyData(lobby, "lobbytype"),
					MemberLimit = client.native.matchmaking.GetLobbyMemberLimit(lobby),
					Owner = client.native.matchmaking.GetLobbyOwner(lobby),
					NumMembers = client.native.matchmaking.GetNumLobbyMembers(lobby),
					lobbyData = dictionary
				};
			}

			// Token: 0x06001DF1 RID: 7665 RVA: 0x000649F8 File Offset: 0x00062BF8
			public int GetDataCount()
			{
				return this.Client.native.matchmaking.GetLobbyDataCount(this.LobbyID);
			}

			// Token: 0x06001DF2 RID: 7666 RVA: 0x00064A1A File Offset: 0x00062C1A
			public bool GetDataByIndex(int index, out string key, out string value)
			{
				return this.Client.native.matchmaking.GetLobbyDataByIndex(this.LobbyID, index, out key, out value);
			}

			// Token: 0x04000BE4 RID: 3044
			private Dictionary<string, string> lobbyData;

			// Token: 0x04000BE5 RID: 3045
			internal Client Client;
		}
	}
}
