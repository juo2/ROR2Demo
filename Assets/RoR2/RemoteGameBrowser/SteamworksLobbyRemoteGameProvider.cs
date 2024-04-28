using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Facepunch.Steamworks;
using HG;
using RoR2.Networking;

namespace RoR2.RemoteGameBrowser
{
	// Token: 0x02000AF2 RID: 2802
	public class SteamworksLobbyRemoteGameProvider : BaseAsyncRemoteGameProvider
	{
		// Token: 0x06004048 RID: 16456 RVA: 0x00109784 File Offset: 0x00107984
		private LobbyList.Filter BuildFilter()
		{
			LobbyList.Filter filter = new LobbyList.Filter();
			if (!this.searchFilters.allowMismatchedMods)
			{
				filter.StringFilters.Add("_mh", NetworkModCompatibilityHelper.networkModHash);
			}
			return filter;
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x001097BC File Offset: 0x001079BC
		public override bool RequestRefresh()
		{
			LobbyList.Filter filter = this.BuildFilter();
			bool flag = (PlatformSystems.lobbyManager as SteamworksLobbyManager).RequestLobbyList(this, filter, new Action<List<LobbyList.Lobby>>(this.OnLobbyListReceived));
			if (flag)
			{
				this.waitingForLobbyCount++;
			}
			return flag;
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x00109800 File Offset: 0x00107A00
		private void OnLobbyListReceived(List<LobbyList.Lobby> lobbies)
		{
			if (base.disposed)
			{
				return;
			}
			this.waitingForLobbyCount--;
			List<LobbyList.Lobby> obj = this.lobbyList;
			lock (obj)
			{
				this.lobbyList.Clear();
				this.lobbyList.AddRange(lobbies);
				base.SetDirty();
			}
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x00109870 File Offset: 0x00107A70
		protected override Task<RemoteGameInfo[]> CreateTask(CancellationToken cancellationToken)
		{
			List<LobbyList.Lobby> obj = this.lobbyList;
			LobbyList.Lobby[] lobbies;
			lock (obj)
			{
				lobbies = this.lobbyList.ToArray();
			}
			return new Task<RemoteGameInfo[]>(delegate()
			{
				RemoteGameInfo[] array = new RemoteGameInfo[lobbies.Length];
				for (int i = 0; i < lobbies.Length; i++)
				{
					SteamworksLobbyRemoteGameProvider.CreateRemoteGameInfo(lobbies[i], out array[i]);
				}
				return array;
			}, cancellationToken);
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x001098D4 File Offset: 0x00107AD4
		private static void CreateRemoteGameInfo(LobbyList.Lobby lobby, out RemoteGameInfo result)
		{
			SteamworksLobbyRemoteGameProvider.<>c__DisplayClass7_0 CS$<>8__locals1;
			CS$<>8__locals1.lobby = lobby;
			result = default(RemoteGameInfo);
			result.name = CS$<>8__locals1.lobby.Name;
			result.lobbyName = CS$<>8__locals1.lobby.Name;
			result.lobbyId = new ulong?(CS$<>8__locals1.lobby.LobbyID);
			string text = SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetString|7_0("_map", ref CS$<>8__locals1);
			result.serverId = SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetULong|7_1("server_id", ref CS$<>8__locals1);
			result.serverAddress = SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetAddressPortPair|7_2("server_address", ref CS$<>8__locals1);
			result.serverName = SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetString|7_0("_svnm", ref CS$<>8__locals1);
			result.lobbyPlayerCount = new int?(SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetInt|7_3("player_count", 1, int.MaxValue, ref CS$<>8__locals1) ?? 1);
			result.lobbyMaxPlayers = new int?(SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetInt|7_3("total_max_players", 1, int.MaxValue, ref CS$<>8__locals1) ?? 1);
			result.serverPlayerCount = SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetInt|7_3("_svplc", 0, int.MaxValue, ref CS$<>8__locals1);
			result.serverMaxPlayers = SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetInt|7_3("_svmpl", 0, int.MaxValue, ref CS$<>8__locals1);
			result.inGame = new bool?(result.serverId != null || result.serverAddress != null);
			result.currentSceneName = text;
			if (text != null)
			{
				SceneDef sceneDefFromSceneName = SceneCatalog.GetSceneDefFromSceneName(text);
				result.currentSceneIndex = ((sceneDefFromSceneName != null) ? new SceneIndex?(sceneDefFromSceneName.sceneDefIndex) : null);
			}
			result.requestRefreshImplementation = new RemoteGameInfo.RequestRefreshDelegate(SteamworksLobbyRemoteGameProvider.RemoteGameInfoRequestRefresh);
			result.getPlayersImplementation = new RemoteGameInfo.GetPlayersDelegate(SteamworksLobbyRemoteGameProvider.RemoteGameInfoGetPlayers);
			result.getRuleBookImplementation = new RemoteGameInfo.GetRuleBookDelegate(SteamworksLobbyRemoteGameProvider.RemoteGameInfoGetRuleBook);
			result.userData = CS$<>8__locals1.lobby;
			result.hasPassword = SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetBool|7_4("_pw", ref CS$<>8__locals1);
			result.gameModeName = SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetString|7_0("_svgm", ref CS$<>8__locals1);
			result.buildId = (SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetString|7_0("build_id", ref CS$<>8__locals1) ?? "UNKNOWN");
			result.modHash = (SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetString|7_0("_mh", ref CS$<>8__locals1) ?? "UNKNOWN");
			result.SetTags(SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetTags|7_5("_svtags", ref CS$<>8__locals1));
			result.CalcExtraFields();
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x00109B08 File Offset: 0x00107D08
		private static void RemoteGameInfoRequestRefresh(in RemoteGameInfo remoteGameInfo, RemoteGameInfo.RequestRefreshSuccessCallback successCallback, Action failureCallback, bool fetchDetails)
		{
			SteamworksLobbyRemoteGameProvider.<>c__DisplayClass8_0 CS$<>8__locals1 = new SteamworksLobbyRemoteGameProvider.<>c__DisplayClass8_0();
			CS$<>8__locals1.failureCallback = failureCallback;
			CS$<>8__locals1.successCallback = successCallback;
			LobbyList.Lobby lobby;
			if ((lobby = (remoteGameInfo.userData as LobbyList.Lobby)) != null)
			{
				Task<LobbyList.Lobby> lobby2 = Client.Instance.LobbyList.GetLobby(lobby.LobbyID);
				if (lobby2 == null)
				{
					Action failureCallback2 = CS$<>8__locals1.failureCallback;
					if (failureCallback2 == null)
					{
						return;
					}
					failureCallback2();
					return;
				}
				else
				{
					lobby2.ContinueWith(delegate(Task<LobbyList.Lobby> antecedentTask)
					{
						RoR2Application.onNextUpdate += delegate()
						{
							CS$<>8__locals1.<RemoteGameInfoRequestRefresh>g__HandleRefresh|0(antecedentTask);
						};
					});
				}
			}
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x00109B78 File Offset: 0x00107D78
		private static bool RemoteGameInfoGetRuleBook(in RemoteGameInfo remoteGameInfo, RuleBook dest)
		{
			LobbyList.Lobby lobby;
			if ((lobby = (remoteGameInfo.userData as LobbyList.Lobby)) != null)
			{
				KeyValueUnsplitter keyValueUnsplitter = new KeyValueUnsplitter("rulebook");
				List<KeyValuePair<string, string>> list = CollectionPool<KeyValuePair<string, string>, List<KeyValuePair<string, string>>>.RentCollection();
				try
				{
					int i = 0;
					int dataCount = lobby.GetDataCount();
					while (i < dataCount)
					{
						string key;
						string value;
						if (lobby.GetDataByIndex(i, out key, out value))
						{
							list.Add(new KeyValuePair<string, string>(key, value));
						}
						i++;
					}
					string value2 = keyValueUnsplitter.GetValue(list);
					if (!string.IsNullOrEmpty(value2))
					{
						RuleBook.ReadBase64(value2, dest);
						return true;
					}
				}
				finally
				{
					list = CollectionPool<KeyValuePair<string, string>, List<KeyValuePair<string, string>>>.ReturnCollection(list);
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x00109C18 File Offset: 0x00107E18
		private static void RemoteGameInfoGetPlayers(in RemoteGameInfo remoteGameInfo, List<RemotePlayerInfo> output)
		{
			LobbyList.Lobby lobby;
			if ((lobby = (remoteGameInfo.userData as LobbyList.Lobby)) != null)
			{
				for (int i = 0; i < lobby.NumMembers; i++)
				{
					output.Add(new RemotePlayerInfo
					{
						id = 0UL,
						name = "???"
					});
				}
			}
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x00109C69 File Offset: 0x00107E69
		public override bool IsBusy()
		{
			return base.IsBusy() || this.waitingForLobbyCount > 0;
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x00109CA4 File Offset: 0x00107EA4
		[CompilerGenerated]
		internal static string <CreateRemoteGameInfo>g__GetString|7_0(string key, ref SteamworksLobbyRemoteGameProvider.<>c__DisplayClass7_0 A_1)
		{
			string data = A_1.lobby.GetData(key);
			if (!(data == string.Empty))
			{
				return data;
			}
			return null;
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x00109CD0 File Offset: 0x00107ED0
		[CompilerGenerated]
		internal static ulong? <CreateRemoteGameInfo>g__GetULong|7_1(string key, ref SteamworksLobbyRemoteGameProvider.<>c__DisplayClass7_0 A_1)
		{
			ulong value;
			if (!ulong.TryParse(SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetString|7_0(key, ref A_1), out value))
			{
				return null;
			}
			return new ulong?(value);
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x00109D00 File Offset: 0x00107F00
		[CompilerGenerated]
		internal static AddressPortPair? <CreateRemoteGameInfo>g__GetAddressPortPair|7_2(string key, ref SteamworksLobbyRemoteGameProvider.<>c__DisplayClass7_0 A_1)
		{
			AddressPortPair value;
			if (!AddressPortPair.TryParse(SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetString|7_0(key, ref A_1), out value))
			{
				return null;
			}
			return new AddressPortPair?(value);
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x00109D30 File Offset: 0x00107F30
		[CompilerGenerated]
		internal static int? <CreateRemoteGameInfo>g__GetInt|7_3(string key, int min, int max, ref SteamworksLobbyRemoteGameProvider.<>c__DisplayClass7_0 A_3)
		{
			int num;
			if (int.TryParse(SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetString|7_0(key, ref A_3), out num) && min <= num && num <= max)
			{
				return new int?(num);
			}
			return null;
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x00109D68 File Offset: 0x00107F68
		[CompilerGenerated]
		internal static bool? <CreateRemoteGameInfo>g__GetBool|7_4(string key, ref SteamworksLobbyRemoteGameProvider.<>c__DisplayClass7_0 A_1)
		{
			int? num = SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetInt|7_3(key, int.MinValue, int.MaxValue, ref A_1);
			int num2 = 0;
			return new bool?(num.GetValueOrDefault() > num2 & num != null);
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x00109DA0 File Offset: 0x00107FA0
		[CompilerGenerated]
		internal static string[] <CreateRemoteGameInfo>g__GetTags|7_5(string key, ref SteamworksLobbyRemoteGameProvider.<>c__DisplayClass7_0 A_1)
		{
			string text = SteamworksLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetString|7_0(key, ref A_1);
			if (text == null)
			{
				return null;
			}
			return text.Split(SteamworksLobbyRemoteGameProvider.tagsSeparator, StringSplitOptions.None);
		}

		// Token: 0x04003E98 RID: 16024
		private readonly List<LobbyList.Lobby> lobbyList = new List<LobbyList.Lobby>();

		// Token: 0x04003E99 RID: 16025
		private int waitingForLobbyCount;

		// Token: 0x04003E9A RID: 16026
		private static readonly char[] tagsSeparator = new char[]
		{
			','
		};
	}
}
