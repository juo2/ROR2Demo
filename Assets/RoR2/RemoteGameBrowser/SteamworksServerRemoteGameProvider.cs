using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Facepunch.Steamworks;
using RoR2.Networking;

namespace RoR2.RemoteGameBrowser
{
	// Token: 0x02000AF7 RID: 2807
	public class SteamworksServerRemoteGameProvider : BaseAsyncRemoteGameProvider
	{
		// Token: 0x06004060 RID: 16480 RVA: 0x00109E7A File Offset: 0x0010807A
		public SteamworksServerRemoteGameProvider(SteamworksServerRemoteGameProvider.Mode mode)
		{
			this.mode = mode;
			this.filter = new ServerList.Filter();
		}

		// Token: 0x06004061 RID: 16481 RVA: 0x00109E94 File Offset: 0x00108094
		public new SteamworksServerRemoteGameProvider.SearchFilters GetSearchFilters()
		{
			return this.searchFilters;
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x00109E9C File Offset: 0x0010809C
		public void SetSearchFilters(SteamworksServerRemoteGameProvider.SearchFilters newSearchFilters)
		{
			if (this.searchFilters.Equals(newSearchFilters))
			{
				return;
			}
			this.searchFilters = newSearchFilters;
			this.BuildFilter();
			if (this.refreshOnFiltersChanged)
			{
				this.RequestRefresh();
			}
			base.SetDirty();
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x00109ED0 File Offset: 0x001080D0
		private void BuildFilter()
		{
			this.filter.Clear();
			SteamworksServerRemoteGameProvider.<>c__DisplayClass17_0 CS$<>8__locals1;
			CS$<>8__locals1.elements = new SteamworksServerRemoteGameProvider.FilterElement[64];
			CS$<>8__locals1.elementCount = 0;
			CS$<>8__locals1.operatorIndicesAndCounts = new Stack<ValueTuple<int, int>>();
			CS$<>8__locals1.currentOperandCount = 0;
			SteamworksServerRemoteGameProvider.<BuildFilter>g__PushOperation|17_2("and", ref CS$<>8__locals1);
			SteamworksServerRemoteGameProvider.<BuildFilter>g__AddOperand|17_1("appid", Client.Instance.AppId.ToString(), ref CS$<>8__locals1);
			if (this.searchFilters.allowDedicatedServers != this.searchFilters.allowListenServers)
			{
				if (this.searchFilters.allowListenServers)
				{
					SteamworksServerRemoteGameProvider.<BuildFilter>g__PushOperation|17_2("nor", ref CS$<>8__locals1);
					SteamworksServerRemoteGameProvider.<BuildFilter>g__AddOperand|17_1("dedicated", "", ref CS$<>8__locals1);
					SteamworksServerRemoteGameProvider.<BuildFilter>g__PopOperation|17_3(ref CS$<>8__locals1);
				}
				else
				{
					SteamworksServerRemoteGameProvider.<BuildFilter>g__AddOperand|17_1("dedicated", "", ref CS$<>8__locals1);
				}
			}
			if (this.searchFilters.mustHavePlayers)
			{
				SteamworksServerRemoteGameProvider.<BuildFilter>g__AddOperand|17_1("hasplayers", "", ref CS$<>8__locals1);
			}
			if (this.searchFilters.mustNotBeFull)
			{
				SteamworksServerRemoteGameProvider.<BuildFilter>g__AddOperand|17_1("notfull", "", ref CS$<>8__locals1);
			}
			if (!this.searchFilters.allowInProgressGames)
			{
				SteamworksServerRemoteGameProvider.<BuildFilter>g__AddOperand|17_1("map", "lobby", ref CS$<>8__locals1);
			}
			if (!string.IsNullOrEmpty(this.searchFilters.requiredTags))
			{
				SteamworksServerRemoteGameProvider.<BuildFilter>g__AddOperand|17_1("gametagsand", this.searchFilters.requiredTags, ref CS$<>8__locals1);
			}
			if (!string.IsNullOrEmpty(this.searchFilters.forbiddenTags))
			{
				SteamworksServerRemoteGameProvider.<BuildFilter>g__AddOperand|17_1("gametagsnor", this.searchFilters.forbiddenTags, ref CS$<>8__locals1);
			}
			SteamworksServerRemoteGameProvider.<BuildFilter>g__AddOperand|17_1("gamedataand", ServerManagerBase<SteamworksServerManager>.GetVersionGameDataString(), ref CS$<>8__locals1);
			if (!this.searchFilters.allowMismatchedMods)
			{
				SteamworksServerRemoteGameProvider.<BuildFilter>g__AddOperand|17_1("gamedataand", NetworkModCompatibilityHelper.steamworksGameserverGameDataValue, ref CS$<>8__locals1);
			}
			SteamworksServerRemoteGameProvider.<BuildFilter>g__PopOperation|17_3(ref CS$<>8__locals1);
			for (int i = 0; i < CS$<>8__locals1.elementCount; i++)
			{
				ref SteamworksServerRemoteGameProvider.FilterElement ptr = ref CS$<>8__locals1.elements[i];
				this.filter.Add(ptr.key, ptr.isOperator ? ptr.operandCount.ToString() : ptr.value);
			}
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x0010A0C4 File Offset: 0x001082C4
		public override bool RequestRefresh()
		{
			this.CancelCurrentRequest();
			this.currentRequest = SteamworksServerRemoteGameProvider.CreateRequest(this.mode, this.filter);
			this.currentRequest.OnUpdate = new Action(this.OnCurrentRequestUpdated);
			this.currentRequest.OnFinished = new Action(this.OnCurrentRequestFinished);
			this.currentRequest.OnServerResponded = new Action<ServerList.Server>(this.OnCurrentRequestServerResponded);
			return true;
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x0010A134 File Offset: 0x00108334
		private static Func<ServerList.Filter, ServerList.Request> GetRequestMethod(SteamworksServerRemoteGameProvider.Mode mode)
		{
			ServerList serverList = Client.Instance.ServerList;
			switch (mode)
			{
			case SteamworksServerRemoteGameProvider.Mode.Internet:
				return new Func<ServerList.Filter, ServerList.Request>(serverList.Internet);
			case SteamworksServerRemoteGameProvider.Mode.Favorites:
				return new Func<ServerList.Filter, ServerList.Request>(serverList.Favourites);
			case SteamworksServerRemoteGameProvider.Mode.History:
				return new Func<ServerList.Filter, ServerList.Request>(serverList.History);
			case SteamworksServerRemoteGameProvider.Mode.Local:
				return new Func<ServerList.Filter, ServerList.Request>(serverList.Local);
			case SteamworksServerRemoteGameProvider.Mode.Friends:
				return new Func<ServerList.Filter, ServerList.Request>(serverList.Friends);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x0010A1AE File Offset: 0x001083AE
		private static ServerList.Request CreateRequest(SteamworksServerRemoteGameProvider.Mode mode, ServerList.Filter filter)
		{
			return SteamworksServerRemoteGameProvider.GetRequestMethod(mode)(filter);
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x001069D6 File Offset: 0x00104BD6
		private void OnCurrentRequestServerResponded(ServerList.Server server)
		{
			base.SetDirty();
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x001069D6 File Offset: 0x00104BD6
		private void OnCurrentRequestFinished()
		{
			base.SetDirty();
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x001069D6 File Offset: 0x00104BD6
		private void OnCurrentRequestUpdated()
		{
			base.SetDirty();
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x0010A1BC File Offset: 0x001083BC
		private void CancelCurrentRequest()
		{
			if (this.currentRequest == null)
			{
				return;
			}
			this.currentRequest.OnFinished = null;
			this.currentRequest.OnServerResponded = null;
			this.currentRequest.OnUpdate = null;
			this.currentRequest.Dispose();
			this.currentRequest = null;
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x0010A208 File Offset: 0x00108408
		private static RemoteGameInfo[] GenerateRemoteGameInfoCache(ServerList.Server[] knownServers, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			RemoteGameInfo[] array = new RemoteGameInfo[knownServers.Length];
			for (int i = 0; i < knownServers.Length; i++)
			{
				cancellationToken.ThrowIfCancellationRequested();
				ServerList.Server serverInfo = knownServers[i];
				SteamworksServerRemoteGameProvider.GenerateRemoteGameInfo(ref array[i], serverInfo);
			}
			return array;
		}

		// Token: 0x0600406C RID: 16492 RVA: 0x0010A24C File Offset: 0x0010844C
		protected override Task<RemoteGameInfo[]> CreateTask(CancellationToken cancellationToken)
		{
			if (this.currentRequest == null)
			{
				return new Task<RemoteGameInfo[]>(() => Array.Empty<RemoteGameInfo>());
			}
			ServerList.Server[] servers = this.currentRequest.Responded.ToArray();
			return new Task<RemoteGameInfo[]>(() => SteamworksServerRemoteGameProvider.GenerateRemoteGameInfoCache(servers, cancellationToken), cancellationToken);
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x0010A2C0 File Offset: 0x001084C0
		public override void Dispose()
		{
			this.CancelCurrentRequest();
			base.Dispose();
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x0010A2CE File Offset: 0x001084CE
		public override bool IsBusy()
		{
			if (!base.IsBusy())
			{
				ServerList.Request request = this.currentRequest;
				return request != null && !request.Finished;
			}
			return true;
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x0010A2F0 File Offset: 0x001084F0
		private static void GenerateRemoteGameInfo(ref RemoteGameInfo remoteGameInfo, ServerList.Server serverInfo)
		{
			remoteGameInfo.name = serverInfo.Name;
			remoteGameInfo.serverAddress = new AddressPortPair?(new AddressPortPair(serverInfo.Address, (ushort)serverInfo.ConnectionPort));
			remoteGameInfo.serverName = serverInfo.Name;
			remoteGameInfo.serverId = new ulong?(serverInfo.SteamId);
			remoteGameInfo.hasPassword = new bool?(serverInfo.Passworded);
			remoteGameInfo.serverPlayerCount = new int?(serverInfo.Players);
			remoteGameInfo.serverMaxPlayers = new int?(serverInfo.MaxPlayers);
			remoteGameInfo.ping = new int?(serverInfo.Ping);
			remoteGameInfo.SetTags(serverInfo.Tags);
			remoteGameInfo.isFavorite = new bool?(serverInfo.Favourite);
			remoteGameInfo.userData = serverInfo;
			remoteGameInfo.getPlayersImplementation = new RemoteGameInfo.GetPlayersDelegate(SteamworksServerRemoteGameProvider.RemoteGameInfoGetPlayers);
			remoteGameInfo.requestRefreshImplementation = new RemoteGameInfo.RequestRefreshDelegate(SteamworksServerRemoteGameProvider.RemoteGameInfoRequestRefresh);
			remoteGameInfo.getRuleBookImplementation = new RemoteGameInfo.GetRuleBookDelegate(SteamworksServerRemoteGameProvider.RemoteGameInfoGetRuleBook);
			remoteGameInfo.currentSceneName = serverInfo.Map;
			SceneDef sceneDefFromSceneName = SceneCatalog.GetSceneDefFromSceneName(serverInfo.Map);
			remoteGameInfo.currentSceneIndex = ((sceneDefFromSceneName != null) ? new SceneIndex?(sceneDefFromSceneName.sceneDefIndex) : null);
			remoteGameInfo.CalcExtraFields();
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x0010A420 File Offset: 0x00108620
		private static void RemoteGameInfoRequestRefresh(in RemoteGameInfo remoteGameInfo, RemoteGameInfo.RequestRefreshSuccessCallback successCallback, Action failureCallback, bool fetchDetails)
		{
			SteamworksServerRemoteGameProvider.<>c__DisplayClass30_0 CS$<>8__locals1 = new SteamworksServerRemoteGameProvider.<>c__DisplayClass30_0();
			CS$<>8__locals1.fetchDetails = fetchDetails;
			CS$<>8__locals1.successCallback = successCallback;
			CS$<>8__locals1.failureCallback = failureCallback;
			if (remoteGameInfo.userData is ServerList.Server)
			{
				SteamworksServerRemoteGameProvider.<>c__DisplayClass30_1 CS$<>8__locals2 = new SteamworksServerRemoteGameProvider.<>c__DisplayClass30_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.filter = new ServerList.Filter();
				CS$<>8__locals2.filter.Add("and", "2");
				CS$<>8__locals2.filter.Add("appid", Client.Instance.AppId.ToString());
				ServerList.Filter filter = CS$<>8__locals2.filter;
				string k = "gameaddr";
				AddressPortPair? serverAddress = remoteGameInfo.serverAddress;
				filter.Add(k, serverAddress.ToString());
				ServerList.Request lanRefreshRequest = SteamworksServerRemoteGameProvider.CreateRequest(SteamworksServerRemoteGameProvider.Mode.Local, CS$<>8__locals2.filter);
				lanRefreshRequest.OnFinished = delegate()
				{
					ServerList.Server server = SteamworksServerRemoteGameProvider.<RemoteGameInfoRequestRefresh>g__GetRefreshedServerInfoFromRequest|30_1(lanRefreshRequest);
					if (server != null)
					{
						CS$<>8__locals2.CS$<>8__locals1.<RemoteGameInfoRequestRefresh>g__HandleNewInfo|0(server);
						return;
					}
					ServerList.Request internetRefreshRequest = SteamworksServerRemoteGameProvider.CreateRequest(SteamworksServerRemoteGameProvider.Mode.Internet, CS$<>8__locals2.filter);
					internetRefreshRequest.OnFinished = delegate()
					{
						ServerList.Server server2 = SteamworksServerRemoteGameProvider.<RemoteGameInfoRequestRefresh>g__GetRefreshedServerInfoFromRequest|30_1(internetRefreshRequest);
						if (server2 != null)
						{
							CS$<>8__locals2.CS$<>8__locals1.<RemoteGameInfoRequestRefresh>g__HandleNewInfo|0(server2);
							return;
						}
						Action failureCallback3 = CS$<>8__locals2.CS$<>8__locals1.failureCallback;
						if (failureCallback3 == null)
						{
							return;
						}
						failureCallback3();
					};
				};
				return;
			}
			Action failureCallback2 = CS$<>8__locals1.failureCallback;
			if (failureCallback2 == null)
			{
				return;
			}
			failureCallback2();
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x0010A520 File Offset: 0x00108720
		private static bool RemoteGameInfoGetRuleBook(in RemoteGameInfo remoteGameInfo, RuleBook dest)
		{
			ServerList.Server server;
			if ((server = (remoteGameInfo.userData as ServerList.Server)) != null && server.HasRules)
			{
				dest.SetToDefaults();
				KeyValueUnsplitter keyValueUnsplitter = new KeyValueUnsplitter("ruleBook");
				RuleBook.ReadBase64(keyValueUnsplitter.GetValue(server.Rules), dest);
				return true;
			}
			return false;
		}

		// Token: 0x06004072 RID: 16498 RVA: 0x0010A56C File Offset: 0x0010876C
		private static void RemoteGameInfoGetPlayers(in RemoteGameInfo remoteGameInfo, List<RemotePlayerInfo> output)
		{
			ServerList.Server server;
			if ((server = (remoteGameInfo.userData as ServerList.Server)) != null)
			{
				List<ServerList.PlayerInfo> playerInfos = server.PlayerInfos;
				if (playerInfos == null)
				{
					return;
				}
				for (int i = 0; i < playerInfos.Count; i++)
				{
					output.Add(new RemotePlayerInfo
					{
						name = playerInfos[i].name
					});
				}
			}
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x0010A5F8 File Offset: 0x001087F8
		[CompilerGenerated]
		internal static ref SteamworksServerRemoteGameProvider.FilterElement <BuildFilter>g__GetElement|17_0(int i, ref SteamworksServerRemoteGameProvider.<>c__DisplayClass17_0 A_1)
		{
			if (A_1.elements.Length < i)
			{
				Array.Resize<SteamworksServerRemoteGameProvider.FilterElement>(ref A_1.elements, A_1.elements.Length * 2);
			}
			return ref A_1.elements[i];
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x0010A628 File Offset: 0x00108828
		[CompilerGenerated]
		internal unsafe static void <BuildFilter>g__AddOperand|17_1(string key, string value, ref SteamworksServerRemoteGameProvider.<>c__DisplayClass17_0 A_2)
		{
			int num = A_2.currentOperandCount + 1;
			A_2.currentOperandCount = num;
			num = A_2.elementCount;
			A_2.elementCount = num + 1;
			*SteamworksServerRemoteGameProvider.<BuildFilter>g__GetElement|17_0(num, ref A_2) = new SteamworksServerRemoteGameProvider.FilterElement
			{
				key = key,
				value = value,
				isOperator = false
			};
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x0010A684 File Offset: 0x00108884
		[CompilerGenerated]
		internal unsafe static void <BuildFilter>g__PushOperation|17_2(string operationCode, ref SteamworksServerRemoteGameProvider.<>c__DisplayClass17_0 A_1)
		{
			int num = A_1.currentOperandCount + 1;
			A_1.currentOperandCount = num;
			num = A_1.elementCount;
			A_1.elementCount = num + 1;
			int num2 = num;
			*SteamworksServerRemoteGameProvider.<BuildFilter>g__GetElement|17_0(num2, ref A_1) = new SteamworksServerRemoteGameProvider.FilterElement
			{
				key = operationCode,
				isOperator = true
			};
			A_1.operatorIndicesAndCounts.Push(new ValueTuple<int, int>(num2, A_1.currentOperandCount));
			A_1.currentOperandCount = 0;
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x0010A6F8 File Offset: 0x001088F8
		[CompilerGenerated]
		internal static void <BuildFilter>g__PopOperation|17_3(ref SteamworksServerRemoteGameProvider.<>c__DisplayClass17_0 A_0)
		{
			int currentOperandCount = A_0.currentOperandCount;
			ValueTuple<int, int> valueTuple = A_0.operatorIndicesAndCounts.Pop();
			int item = valueTuple.Item1;
			A_0.currentOperandCount = valueTuple.Item2;
			A_0.currentOperandCount += currentOperandCount;
			SteamworksServerRemoteGameProvider.<BuildFilter>g__GetElement|17_0(item, ref A_0).operandCount = currentOperandCount;
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x0010A744 File Offset: 0x00108944
		[CompilerGenerated]
		internal static ServerList.Server <RemoteGameInfoRequestRefresh>g__GetRefreshedServerInfoFromRequest|30_1(ServerList.Request request)
		{
			if (request.Responded.Count > 0)
			{
				return request.Responded[0];
			}
			if (request.Unresponsive.Count > 0)
			{
				return request.Unresponsive[0];
			}
			return null;
		}

		// Token: 0x04003EA1 RID: 16033
		private SteamworksServerRemoteGameProvider.Mode mode;

		// Token: 0x04003EA2 RID: 16034
		private ServerList.Filter filter;

		// Token: 0x04003EA3 RID: 16035
		private ServerList.Request currentRequest;

		// Token: 0x04003EA4 RID: 16036
		public new bool refreshOnFiltersChanged;

		// Token: 0x04003EA5 RID: 16037
		private static readonly int k_cbMaxGameServerGameData = 2048;

		// Token: 0x04003EA6 RID: 16038
		private static readonly int k_cbMaxGameServerGameDescription = 64;

		// Token: 0x04003EA7 RID: 16039
		private static readonly int k_cbMaxGameServerGameDir = 32;

		// Token: 0x04003EA8 RID: 16040
		private static readonly int k_cbMaxGameServerMapName = 32;

		// Token: 0x04003EA9 RID: 16041
		private static readonly int k_cbMaxGameServerName = 64;

		// Token: 0x04003EAA RID: 16042
		private static readonly int k_cbMaxGameServerTags = 128;

		// Token: 0x04003EAB RID: 16043
		private new SteamworksServerRemoteGameProvider.SearchFilters searchFilters;

		// Token: 0x02000AF8 RID: 2808
		public new struct SearchFilters : IEquatable<SteamworksServerRemoteGameProvider.SearchFilters>
		{
			// Token: 0x06004079 RID: 16505 RVA: 0x0010A780 File Offset: 0x00108980
			public bool Equals(SteamworksServerRemoteGameProvider.SearchFilters other)
			{
				return this.allowDedicatedServers == other.allowDedicatedServers && this.allowListenServers == other.allowListenServers && this.mustHavePlayers == other.mustHavePlayers && this.mustNotBeFull == other.mustNotBeFull && this.allowInProgressGames == other.allowInProgressGames && this.allowMismatchedMods == other.allowMismatchedMods && string.Equals(this.requiredTags, other.requiredTags) && string.Equals(this.forbiddenTags, other.forbiddenTags);
			}

			// Token: 0x0600407A RID: 16506 RVA: 0x0010A808 File Offset: 0x00108A08
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is SteamworksServerRemoteGameProvider.SearchFilters)
				{
					SteamworksServerRemoteGameProvider.SearchFilters other = (SteamworksServerRemoteGameProvider.SearchFilters)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x0600407B RID: 16507 RVA: 0x0010A834 File Offset: 0x00108A34
			public override int GetHashCode()
			{
				return ((((((this.allowDedicatedServers.GetHashCode() * 397 ^ this.allowListenServers.GetHashCode()) * 397 ^ this.mustHavePlayers.GetHashCode()) * 397 ^ this.mustNotBeFull.GetHashCode()) * 397 ^ this.allowInProgressGames.GetHashCode()) * 397 ^ this.allowMismatchedMods.GetHashCode()) * 397 ^ ((this.requiredTags != null) ? this.requiredTags.GetHashCode() : 0)) * 397 ^ ((this.forbiddenTags != null) ? this.forbiddenTags.GetHashCode() : 0);
			}

			// Token: 0x04003EAC RID: 16044
			public bool allowDedicatedServers;

			// Token: 0x04003EAD RID: 16045
			public bool allowListenServers;

			// Token: 0x04003EAE RID: 16046
			public bool mustHavePlayers;

			// Token: 0x04003EAF RID: 16047
			public bool mustNotBeFull;

			// Token: 0x04003EB0 RID: 16048
			public bool allowInProgressGames;

			// Token: 0x04003EB1 RID: 16049
			public bool allowMismatchedMods;

			// Token: 0x04003EB2 RID: 16050
			public string requiredTags;

			// Token: 0x04003EB3 RID: 16051
			public string forbiddenTags;
		}

		// Token: 0x02000AF9 RID: 2809
		public enum Mode
		{
			// Token: 0x04003EB5 RID: 16053
			Internet,
			// Token: 0x04003EB6 RID: 16054
			Favorites,
			// Token: 0x04003EB7 RID: 16055
			History,
			// Token: 0x04003EB8 RID: 16056
			Local,
			// Token: 0x04003EB9 RID: 16057
			Friends
		}

		// Token: 0x02000AFA RID: 2810
		private struct FilterElement
		{
			// Token: 0x04003EBA RID: 16058
			public string key;

			// Token: 0x04003EBB RID: 16059
			public string value;

			// Token: 0x04003EBC RID: 16060
			public int operandCount;

			// Token: 0x04003EBD RID: 16061
			public bool isOperator;
		}
	}
}
