using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;
using HG;
using RoR2.Networking;

namespace RoR2.RemoteGameBrowser
{
	// Token: 0x02000AD5 RID: 2773
	public class EOSLobbyRemoteGameProvider : BaseAsyncRemoteGameProvider
	{
		// Token: 0x06003FB4 RID: 16308 RVA: 0x0010732C File Offset: 0x0010552C
		private EOSLobbyManager.Filter BuildFilter()
		{
			EOSLobbyManager.Filter filter = new EOSLobbyManager.Filter();
			if (!this.searchFilters.allowMismatchedMods)
			{
				AttributeData item = new AttributeData
				{
					Key = "_mh",
					Value = NetworkModCompatibilityHelper.networkModHash
				};
				filter.SearchData.Add(item);
			}
			return filter;
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x0010737C File Offset: 0x0010557C
		public override bool RequestRefresh()
		{
			EOSLobbyManager.Filter filter = this.BuildFilter();
			bool flag = (PlatformSystems.lobbyManager as EOSLobbyManager).RequestLobbyList(this, filter, new Action<List<LobbyDetails>>(this.OnLobbyListReceived));
			if (flag)
			{
				this.waitingForLobbyCount++;
			}
			return flag;
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x001073C0 File Offset: 0x001055C0
		private void OnLobbyListReceived(List<LobbyDetails> lobbies)
		{
			if (base.disposed)
			{
				return;
			}
			this.waitingForLobbyCount--;
			List<LobbyDetails> obj = this.lobbyList;
			lock (obj)
			{
				this.lobbyList.Clear();
				this.lobbyList.AddRange(lobbies);
				base.SetDirty();
			}
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x00107430 File Offset: 0x00105630
		protected override Task<RemoteGameInfo[]> CreateTask(CancellationToken cancellationToken)
		{
			List<LobbyDetails> obj = this.lobbyList;
			LobbyDetails[] lobbies;
			lock (obj)
			{
				lobbies = this.lobbyList.ToArray();
			}
			return new Task<RemoteGameInfo[]>(delegate()
			{
				RemoteGameInfo[] array = new RemoteGameInfo[lobbies.Length];
				for (int i = 0; i < lobbies.Length; i++)
				{
					EOSLobbyRemoteGameProvider.CreateRemoteGameInfo(lobbies[i], out array[i]);
				}
				return array;
			}, cancellationToken);
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x00107494 File Offset: 0x00105694
		private static void CreateRemoteGameInfo(LobbyDetails lobby, out RemoteGameInfo result)
		{
			EOSLobbyRemoteGameProvider.<>c__DisplayClass7_0 CS$<>8__locals1;
			CS$<>8__locals1.lobby = lobby;
			result = default(RemoteGameInfo);
			LobbyDetailsInfo lobbyDetailsInfo;
			if (CS$<>8__locals1.lobby.CopyInfo(new LobbyDetailsCopyInfoOptions(), out lobbyDetailsInfo) != Result.Success)
			{
				return;
			}
			result.name = lobbyDetailsInfo.LobbyOwnerUserId.ToString();
			result.lobbyName = lobbyDetailsInfo.LobbyId;
			result.lobbyIdStr = lobbyDetailsInfo.LobbyId;
			string lobbyStringValue = EOSLobbyManager.GetLobbyStringValue(CS$<>8__locals1.lobby, "_map");
			result.serverIdStr = EOSLobbyManager.GetLobbyStringValue(CS$<>8__locals1.lobby, "server_id");
			result.serverAddress = EOSLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetAddressPortPair|7_1("server_address", ref CS$<>8__locals1);
			result.serverName = EOSLobbyManager.GetLobbyStringValue(CS$<>8__locals1.lobby, "_svnm");
			result.lobbyPlayerCount = new int?(EOSLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetInt|7_2("player_count", 1, int.MaxValue, ref CS$<>8__locals1) ?? 1);
			result.lobbyMaxPlayers = new int?(EOSLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetInt|7_2("total_max_players", 1, int.MaxValue, ref CS$<>8__locals1) ?? 1);
			result.serverPlayerCount = EOSLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetInt|7_2("_svplc", 0, int.MaxValue, ref CS$<>8__locals1);
			result.serverMaxPlayers = EOSLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetInt|7_2("_svmpl", 0, int.MaxValue, ref CS$<>8__locals1);
			result.inGame = new bool?(result.IsServerIdValid() || result.serverAddress != null);
			result.currentSceneName = lobbyStringValue;
			if (lobbyStringValue != null)
			{
				SceneDef sceneDefFromSceneName = SceneCatalog.GetSceneDefFromSceneName(lobbyStringValue);
				result.currentSceneIndex = ((sceneDefFromSceneName != null) ? new SceneIndex?(sceneDefFromSceneName.sceneDefIndex) : null);
			}
			result.requestRefreshImplementation = new RemoteGameInfo.RequestRefreshDelegate(EOSLobbyRemoteGameProvider.RemoteGameInfoRequestRefresh);
			result.getPlayersImplementation = new RemoteGameInfo.GetPlayersDelegate(EOSLobbyRemoteGameProvider.RemoteGameInfoGetPlayers);
			result.getRuleBookImplementation = new RemoteGameInfo.GetRuleBookDelegate(EOSLobbyRemoteGameProvider.RemoteGameInfoGetRuleBook);
			result.userData = CS$<>8__locals1.lobby;
			result.hasPassword = EOSLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetBool|7_3("_pw", ref CS$<>8__locals1);
			result.gameModeName = EOSLobbyManager.GetLobbyStringValue(CS$<>8__locals1.lobby, "_svgm");
			result.buildId = (EOSLobbyManager.GetLobbyStringValue(CS$<>8__locals1.lobby, "build_id") ?? "UNKNOWN");
			result.modHash = (EOSLobbyManager.GetLobbyStringValue(CS$<>8__locals1.lobby, "_mh") ?? "UNKNOWN");
			result.SetTags(EOSLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetTags|7_4("_svtags", ref CS$<>8__locals1));
			result.CalcExtraFields();
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x001076E0 File Offset: 0x001058E0
		private static void RemoteGameInfoRequestRefresh(in RemoteGameInfo remoteGameInfo, RemoteGameInfo.RequestRefreshSuccessCallback successCallback, Action failureCallback, bool fetchDetails)
		{
			if (remoteGameInfo.userData is LobbyDetails)
			{
				RemoteGameInfo remoteGameInfo2;
				EOSLobbyRemoteGameProvider.CreateRemoteGameInfo(remoteGameInfo.userData as LobbyDetails, out remoteGameInfo2);
				if (successCallback != null)
				{
					successCallback(remoteGameInfo2);
				}
			}
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x00107718 File Offset: 0x00105918
		private static bool RemoteGameInfoGetRuleBook(in RemoteGameInfo remoteGameInfo, RuleBook dest)
		{
			LobbyDetails lobbyDetails;
			if ((lobbyDetails = (remoteGameInfo.userData as LobbyDetails)) != null)
			{
				KeyValueUnsplitter keyValueUnsplitter = new KeyValueUnsplitter("rulebook");
				List<KeyValuePair<string, string>> list = CollectionPool<KeyValuePair<string, string>, List<KeyValuePair<string, string>>>.RentCollection();
				try
				{
					uint num = 0U;
					uint attributeCount = lobbyDetails.GetAttributeCount(new LobbyDetailsGetAttributeCountOptions());
					while (num < attributeCount)
					{
						Epic.OnlineServices.Lobby.Attribute attribute = new Epic.OnlineServices.Lobby.Attribute();
						if (lobbyDetails.CopyAttributeByIndex(new LobbyDetailsCopyAttributeByIndexOptions
						{
							AttrIndex = num
						}, out attribute) == Result.Success)
						{
							list.Add(new KeyValuePair<string, string>(attribute.Data.Key.ToLower(), attribute.Data.Value.AsUtf8));
						}
						num += 1U;
					}
					string value = keyValueUnsplitter.GetValue(list);
					if (!string.IsNullOrEmpty(value))
					{
						RuleBook.ReadBase64(value, dest);
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

		// Token: 0x06003FBB RID: 16315 RVA: 0x001077F0 File Offset: 0x001059F0
		private static void RemoteGameInfoGetPlayers(in RemoteGameInfo remoteGameInfo, List<RemotePlayerInfo> output)
		{
			LobbyDetails lobbyDetails;
			if ((lobbyDetails = (remoteGameInfo.userData as LobbyDetails)) != null)
			{
				for (uint num = 0U; num < lobbyDetails.GetMemberCount(new LobbyDetailsGetMemberCountOptions()); num += 1U)
				{
					output.Add(new RemotePlayerInfo
					{
						id = 0UL,
						name = "???"
					});
				}
			}
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x00107846 File Offset: 0x00105A46
		public override bool IsBusy()
		{
			return base.IsBusy() || this.waitingForLobbyCount > 0;
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x00107880 File Offset: 0x00105A80
		[CompilerGenerated]
		internal static ulong? <CreateRemoteGameInfo>g__GetULong|7_0(string key, ref EOSLobbyRemoteGameProvider.<>c__DisplayClass7_0 A_1)
		{
			ulong value;
			if (!ulong.TryParse(EOSLobbyManager.GetLobbyStringValue(A_1.lobby, key), out value))
			{
				return null;
			}
			return new ulong?(value);
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x001078B4 File Offset: 0x00105AB4
		[CompilerGenerated]
		internal static AddressPortPair? <CreateRemoteGameInfo>g__GetAddressPortPair|7_1(string key, ref EOSLobbyRemoteGameProvider.<>c__DisplayClass7_0 A_1)
		{
			AddressPortPair value;
			if (!AddressPortPair.TryParse(EOSLobbyManager.GetLobbyStringValue(A_1.lobby, key), out value))
			{
				return null;
			}
			return new AddressPortPair?(value);
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x001078E8 File Offset: 0x00105AE8
		[CompilerGenerated]
		internal static int? <CreateRemoteGameInfo>g__GetInt|7_2(string key, int min, int max, ref EOSLobbyRemoteGameProvider.<>c__DisplayClass7_0 A_3)
		{
			int num;
			if (int.TryParse(EOSLobbyManager.GetLobbyStringValue(A_3.lobby, key), out num) && min <= num && num <= max)
			{
				return new int?(num);
			}
			return null;
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x00107924 File Offset: 0x00105B24
		[CompilerGenerated]
		internal static bool? <CreateRemoteGameInfo>g__GetBool|7_3(string key, ref EOSLobbyRemoteGameProvider.<>c__DisplayClass7_0 A_1)
		{
			int? num = EOSLobbyRemoteGameProvider.<CreateRemoteGameInfo>g__GetInt|7_2(key, int.MinValue, int.MaxValue, ref A_1);
			int num2 = 0;
			return new bool?(num.GetValueOrDefault() > num2 & num != null);
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x0010795C File Offset: 0x00105B5C
		[CompilerGenerated]
		internal static string[] <CreateRemoteGameInfo>g__GetTags|7_4(string key, ref EOSLobbyRemoteGameProvider.<>c__DisplayClass7_0 A_1)
		{
			string lobbyStringValue = EOSLobbyManager.GetLobbyStringValue(A_1.lobby, key);
			if (lobbyStringValue == null)
			{
				return null;
			}
			return lobbyStringValue.Split(EOSLobbyRemoteGameProvider.tagsSeparator, StringSplitOptions.None);
		}

		// Token: 0x04003E08 RID: 15880
		private readonly List<LobbyDetails> lobbyList = new List<LobbyDetails>();

		// Token: 0x04003E09 RID: 15881
		private int waitingForLobbyCount;

		// Token: 0x04003E0A RID: 15882
		private static readonly char[] tagsSeparator = new char[]
		{
			','
		};
	}
}
