using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;
using HG;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200058D RID: 1421
	public static class EOSLobbyDataGenerator
	{
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600197D RID: 6525 RVA: 0x0006E52C File Offset: 0x0006C72C
		// (remove) Token: 0x0600197E RID: 6526 RVA: 0x0006E560 File Offset: 0x0006C760
		public static event Action<List<KeyValuePair<string, string>>> getAdditionalKeyValues;

		// Token: 0x0600197F RID: 6527 RVA: 0x0006E594 File Offset: 0x0006C794
		[SystemInitializer(new Type[]
		{
			typeof(RuleBook)
		})]
		private static void Init()
		{
			EOSLobbyDataGenerator.cachedRuleBook = new RuleBook();
			if (PlatformSystems.EgsToggleConVar.value != 1)
			{
				return;
			}
			LobbyManager lobbyManager = PlatformSystems.lobbyManager;
			lobbyManager.onLobbyOwnershipGained = (Action)Delegate.Combine(lobbyManager.onLobbyOwnershipGained, new Action(EOSLobbyDataGenerator.OnLobbyOwnershipGained));
			LobbyManager lobbyManager2 = PlatformSystems.lobbyManager;
			lobbyManager2.onLobbyOwnershipLost = (Action)Delegate.Combine(lobbyManager2.onLobbyOwnershipLost, new Action(EOSLobbyDataGenerator.OnLobbyOwnershipLost));
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0006E608 File Offset: 0x0006C808
		private static void OnLobbyOwnershipGained()
		{
			EOSLobbyManager eoslobbyManager = PlatformSystems.lobbyManager as EOSLobbyManager;
			EOSLobbyDataGenerator.lobbyId = eoslobbyManager.CurrentLobbyId;
			EOSLobbyDataGenerator.edition = 0;
			LobbyDetails currentLobbyDetails = eoslobbyManager.CurrentLobbyDetails;
			Epic.OnlineServices.Lobby.Attribute attribute = new Epic.OnlineServices.Lobby.Attribute();
			if (currentLobbyDetails.CopyAttributeByKey(new LobbyDetailsCopyAttributeByKeyOptions
			{
				AttrKey = "v"
			}, out attribute) == Result.Success)
			{
				int.TryParse(attribute.Data.Value.AsUtf8, out EOSLobbyDataGenerator.edition);
			}
			LobbyManager lobbyManager = PlatformSystems.lobbyManager;
			lobbyManager.onLobbyMemberDataUpdated = (Action<UserID>)Delegate.Combine(lobbyManager.onLobbyMemberDataUpdated, new Action<UserID>(EOSLobbyDataGenerator.EOSLobbyManagerOnOnLobbyMemberDataUpdated));
			LobbyManager lobbyManager2 = PlatformSystems.lobbyManager;
			lobbyManager2.onLobbyStateChanged = (Action)Delegate.Combine(lobbyManager2.onLobbyStateChanged, new Action(EOSLobbyDataGenerator.EOSLobbyManagerOnOnLobbyStateChanged));
			NetworkManagerSystem.onStartClientGlobal += EOSLobbyDataGenerator.NetworkManagerSystemOnOnStartClientGlobal;
			NetworkManagerSystem.onStopClientGlobal += EOSLobbyDataGenerator.NetworkManagerSystemOnOnStopClientGlobal;
			SceneCatalog.onMostRecentSceneDefChanged += EOSLobbyDataGenerator.SceneCatalogOnOnMostRecentSceneDefChanged;
			NetworkUser.onNetworkUserDiscovered += EOSLobbyDataGenerator.NetworkUserOnOnNetworkUserDiscovered;
			NetworkUser.onNetworkUserLost += EOSLobbyDataGenerator.NetworkUserOnOnNetworkUserLost;
			PreGameController.onPreGameControllerSetRuleBookGlobal += EOSLobbyDataGenerator.OnPreGameControllerSetRuleBook;
			Run.onRunSetRuleBookGlobal += EOSLobbyDataGenerator.OnRunSetRuleBook;
			EOSLobbyDataGenerator.UpdateRuleBook();
			EOSLobbyDataGenerator.RebuildLobbyData();
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0006E73C File Offset: 0x0006C93C
		private static void OnLobbyOwnershipLost()
		{
			EOSLobbyDataGenerator.lobbyId = string.Empty;
			EOSLobbyDataGenerator.edition = 0;
			Run.onRunSetRuleBookGlobal -= EOSLobbyDataGenerator.OnRunSetRuleBook;
			PreGameController.onPreGameControllerSetRuleBookGlobal -= EOSLobbyDataGenerator.OnPreGameControllerSetRuleBook;
			NetworkUser.onNetworkUserLost -= EOSLobbyDataGenerator.NetworkUserOnOnNetworkUserLost;
			NetworkUser.onNetworkUserDiscovered -= EOSLobbyDataGenerator.NetworkUserOnOnNetworkUserDiscovered;
			SceneCatalog.onMostRecentSceneDefChanged -= EOSLobbyDataGenerator.SceneCatalogOnOnMostRecentSceneDefChanged;
			NetworkManagerSystem.onStopClientGlobal -= EOSLobbyDataGenerator.NetworkManagerSystemOnOnStopClientGlobal;
			NetworkManagerSystem.onStartClientGlobal -= EOSLobbyDataGenerator.NetworkManagerSystemOnOnStartClientGlobal;
			LobbyManager lobbyManager = PlatformSystems.lobbyManager;
			lobbyManager.onLobbyMemberDataUpdated = (Action<UserID>)Delegate.Remove(lobbyManager.onLobbyMemberDataUpdated, new Action<UserID>(EOSLobbyDataGenerator.EOSLobbyManagerOnOnLobbyMemberDataUpdated));
			LobbyManager lobbyManager2 = PlatformSystems.lobbyManager;
			lobbyManager2.onLobbyStateChanged = (Action)Delegate.Remove(lobbyManager2.onLobbyStateChanged, new Action(EOSLobbyDataGenerator.EOSLobbyManagerOnOnLobbyStateChanged));
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0006E81C File Offset: 0x0006CA1C
		private static void OnPreGameControllerSetRuleBook(PreGameController run, RuleBook ruleBook)
		{
			EOSLobbyDataGenerator.UpdateRuleBook();
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0006E81C File Offset: 0x0006CA1C
		private static void OnRunSetRuleBook(Run run, RuleBook ruleBook)
		{
			EOSLobbyDataGenerator.UpdateRuleBook();
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0006E823 File Offset: 0x0006CA23
		private static void EOSLobbyManagerOnOnLobbyMemberDataUpdated(UserID memberId)
		{
			EOSLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0006E823 File Offset: 0x0006CA23
		private static void EOSLobbyManagerOnOnLobbyStateChanged()
		{
			EOSLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0006E823 File Offset: 0x0006CA23
		private static void NetworkManagerSystemOnOnStartClientGlobal(NetworkClient networkClient)
		{
			EOSLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0006E823 File Offset: 0x0006CA23
		private static void NetworkManagerSystemOnOnStopClientGlobal()
		{
			EOSLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0006E823 File Offset: 0x0006CA23
		private static void SceneCatalogOnOnMostRecentSceneDefChanged(SceneDef sceneDef)
		{
			EOSLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0006E823 File Offset: 0x0006CA23
		private static void NetworkUserOnOnNetworkUserDiscovered(NetworkUser networkUser)
		{
			EOSLobbyDataGenerator.SetDirty();
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0006E823 File Offset: 0x0006CA23
		private static void NetworkUserOnOnNetworkUserLost(NetworkUser networkUser)
		{
			EOSLobbyDataGenerator.SetDirty();
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0006E82A File Offset: 0x0006CA2A
		public static void SetDirty()
		{
			if (EOSLobbyDataGenerator.dirty)
			{
				return;
			}
			EOSLobbyDataGenerator.dirty = true;
			RoR2Application.onNextUpdate += EOSLobbyDataGenerator.RebuildLobbyData;
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0006E84C File Offset: 0x0006CA4C
		public static void RebuildLobbyData()
		{
			try
			{
				EOSLobbyDataGenerator.dirty = false;
				LobbyDetails currentLobbyDetails = (PlatformSystems.lobbyManager as EOSLobbyManager).CurrentLobbyDetails;
				if (!(currentLobbyDetails == null))
				{
					LobbyDetails lhs = currentLobbyDetails;
					Dictionary<string, string> dictionary = CollectionPool<KeyValuePair<string, string>, Dictionary<string, string>>.RentCollection();
					EOSLobbyManager.GetAllCurrentLobbyKVPs(currentLobbyDetails, ref dictionary);
					if (!EOSLobbyManager.IsLobbyOwner(currentLobbyDetails) || dictionary == null || lhs == null)
					{
						CollectionPool<KeyValuePair<string, string>, Dictionary<string, string>>.ReturnCollection(dictionary);
					}
					else
					{
						LobbyModification currentLobbyModification = (PlatformSystems.lobbyManager as EOSLobbyManager).CurrentLobbyModification;
						string text = null;
						dictionary.TryGetValue("total_max_players", out text);
						int value;
						if (text == null || !int.TryParse(text, out value))
						{
							value = LobbyManager.cvSteamLobbyMaxMembers.value;
							EOSLobbyManager.SetLobbyStringValue(currentLobbyModification, "total_max_players", value.ToString());
						}
						int num = value - PlatformSystems.lobbyManager.calculatedExtraPlayersCount;
						ulong num2 = (ulong)currentLobbyDetails.GetMemberCount(new LobbyDetailsGetMemberCountOptions());
						long num3 = (long)num;
						EOSLobbyDataGenerator.<>c__DisplayClass24_0 CS$<>8__locals1;
						CS$<>8__locals1.newData = CollectionPool<KeyValuePair<string, string>, Dictionary<string, string>>.RentCollection();
						List<KeyValuePair<string, string>> list = CollectionPool<KeyValuePair<string, string>, List<KeyValuePair<string, string>>>.RentCollection();
						Action<List<KeyValuePair<string, string>>> action = EOSLobbyDataGenerator.getAdditionalKeyValues;
						if (action != null)
						{
							action(list);
						}
						for (int i = 0; i < list.Count; i++)
						{
							KeyValuePair<string, string> keyValuePair = list[i];
							EOSLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0(keyValuePair.Key, keyValuePair.Value, ref CS$<>8__locals1);
						}
						CollectionPool<KeyValuePair<string, string>, List<KeyValuePair<string, string>>>.ReturnCollection(list);
						EOSLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("build_id", RoR2Application.GetBuildId(), ref CS$<>8__locals1);
						EOSLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("_mh", NetworkModCompatibilityHelper.networkModHash, ref CS$<>8__locals1);
						string key = "player_count";
						int num4 = PlatformSystems.lobbyManager.calculatedTotalPlayerCount;
						EOSLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0(key, EOSLobbyDataGenerator.ToStringCache.playerCount.GetString(num4), ref CS$<>8__locals1);
						SceneDef sceneDefForCurrentScene = SceneCatalog.GetSceneDefForCurrentScene();
						string value2 = (sceneDefForCurrentScene != null) ? sceneDefForCurrentScene.baseSceneName : null;
						if (!string.IsNullOrEmpty(value2))
						{
							EOSLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("_map", value2, ref CS$<>8__locals1);
						}
						ProductUserId productUserId;
						AddressPortPair addressPortPair;
						bool flag;
						EOSLobbyDataGenerator.GetServerInfo(out productUserId, out addressPortPair, out flag);
						bool flag2 = false;
						bool flag3 = dictionary != null && dictionary.ContainsKey("server_id");
						bool flag4 = dictionary != null && dictionary.ContainsKey("server_address");
						bool flag5 = flag3 || flag4;
						bool flag6 = false;
						if (NetworkSession.instance)
						{
							flag2 = NetworkSession.instance.HasFlag(NetworkSession.Flags.HasPassword);
							EOSLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("_ds", NetworkSession.instance.HasFlag(NetworkSession.Flags.IsDedicatedServer) ? "1" : "0", ref CS$<>8__locals1);
							EOSLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("_pw", flag2 ? "1" : "0", ref CS$<>8__locals1);
							EOSLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("_svtags", (NetworkSession.instance.tagsString != null) ? NetworkSession.instance.tagsString : "", ref CS$<>8__locals1);
							string key2 = "_svmpl";
							num4 = (int)NetworkSession.instance.maxPlayers;
							EOSLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0(key2, EOSLobbyDataGenerator.ToStringCache.serverMaxPlayers.GetString(num4), ref CS$<>8__locals1);
							string key3 = "_svplc";
							num4 = NetworkUser.readOnlyInstancesList.Count;
							EOSLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0(key3, EOSLobbyDataGenerator.ToStringCache.serverPlayerCount.GetString(num4), ref CS$<>8__locals1);
							EOSLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("_svnm", NetworkSession.instance.serverName, ref CS$<>8__locals1);
							if (productUserId != null)
							{
								EOSLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("server_id", productUserId.ToString(), ref CS$<>8__locals1);
								flag6 = true;
							}
							if (addressPortPair.isValid)
							{
								EOSLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("server_address", EOSLobbyDataGenerator.ToStringCache.serverAddress.GetString(addressPortPair), ref CS$<>8__locals1);
								flag6 = true;
							}
						}
						GameModeIndex gameModeIndex = GameModeIndex.Invalid;
						if (Run.instance)
						{
							gameModeIndex = Run.instance.gameModeIndex;
						}
						else if (PreGameController.instance)
						{
							gameModeIndex = PreGameController.instance.gameModeIndex;
						}
						if (gameModeIndex != GameModeIndex.Invalid)
						{
							string gameModeName = GameModeCatalog.GetGameModeName(gameModeIndex);
							EOSLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("_svgm", gameModeName, ref CS$<>8__locals1);
						}
						if (flag6 && !flag5 && flag2)
						{
							string value3 = flag ? NetworkManagerSystem.SvPasswordConVar.instance.value : NetworkManagerSystem.cvClPassword.value;
							NetworkWriter networkWriter = new NetworkWriter();
							networkWriter.Write(value3);
							(PlatformSystems.lobbyManager as EOSLobbyManager).SendLobbyMessage(LobbyManager.LobbyMessageType.Password, networkWriter);
							Debug.Log("Password attempt shared with lobby members to facilitate game join.");
						}
						for (int j = 0; j < EOSLobbyDataGenerator.ruleBookKeyValues.Count; j++)
						{
							KeyValuePair<string, string> keyValuePair2 = EOSLobbyDataGenerator.ruleBookKeyValues[j];
							EOSLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0(keyValuePair2.Key, keyValuePair2.Value, ref CS$<>8__locals1);
						}
						for (int k = 0; k < EOSLobbyDataGenerator.specialKeys.Length; k++)
						{
							dictionary.Remove(EOSLobbyDataGenerator.specialKeys[k]);
						}
						bool flag7 = false;
						List<string> list2 = CollectionPool<string, List<string>>.RentCollection();
						if (dictionary != null)
						{
							foreach (KeyValuePair<string, string> keyValuePair3 in dictionary)
							{
								if (!CS$<>8__locals1.newData.ContainsKey(keyValuePair3.Key))
								{
									list2.Add(keyValuePair3.Key);
								}
							}
						}
						for (int l = 0; l < list2.Count; l++)
						{
							EOSLobbyManager.RemoveLobbyStringValue(currentLobbyModification, list2[l]);
						}
						if (list2.Count > 0)
						{
							flag7 = true;
						}
						CollectionPool<string, List<string>>.ReturnCollection(list2);
						foreach (KeyValuePair<string, string> keyValuePair4 in CS$<>8__locals1.newData)
						{
							string value4 = null;
							if (dictionary == null || !dictionary.TryGetValue(keyValuePair4.Key, out value4) || !keyValuePair4.Value.Equals(value4, StringComparison.Ordinal))
							{
								EOSLobbyManager.SetLobbyStringValue(currentLobbyModification, keyValuePair4.Key, keyValuePair4.Value);
								flag7 = true;
							}
						}
						if (flag7)
						{
							EOSLobbyDataGenerator.edition++;
						}
						EOSLobbyManager.SetLobbyStringValue(currentLobbyModification, "v", EOSLobbyDataGenerator.ToStringCache.edition.GetString(EOSLobbyDataGenerator.edition));
						EOSLobbyManager.UpdateLobby(currentLobbyModification);
						CollectionPool<KeyValuePair<string, string>, Dictionary<string, string>>.ReturnCollection(CS$<>8__locals1.newData);
						CollectionPool<KeyValuePair<string, string>, Dictionary<string, string>>.ReturnCollection(dictionary);
					}
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0006EE00 File Offset: 0x0006D000
		private static void GetServerInfo(out ProductUserId serverId, out AddressPortPair serverAddress, out bool isSelf)
		{
			serverId = new ProductUserId();
			serverAddress = default(AddressPortPair);
			isSelf = false;
			HostDescription desiredHost = NetworkManagerSystem.singleton.desiredHost;
			if (desiredHost.hostType != HostDescription.HostType.None)
			{
				if (desiredHost.hostType == HostDescription.HostType.Self)
				{
					if (NetworkServer.active)
					{
						serverId = NetworkManagerSystem.singleton.serverP2PId.egsValue;
						isSelf = true;
					}
					return;
				}
				if (desiredHost.hostType == HostDescription.HostType.Steam)
				{
					CSteamID cid = desiredHost.userID.CID;
					serverId = cid.egsValue;
					return;
				}
				if (desiredHost.hostType == HostDescription.HostType.IPv4)
				{
					serverAddress = desiredHost.addressPortPair;
					return;
				}
			}
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0006EE90 File Offset: 0x0006D090
		private static void UpdateRuleBook()
		{
			RuleBook ruleBook = null;
			if (Run.instance)
			{
				ruleBook = Run.instance.ruleBook;
			}
			else if (PreGameController.instance)
			{
				ruleBook = PreGameController.instance.readOnlyRuleBook;
			}
			if (ruleBook != null && !ruleBook.Equals(EOSLobbyDataGenerator.cachedRuleBook))
			{
				EOSLobbyDataGenerator.cachedRuleBook.Copy(ruleBook);
				StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
				RuleBook.WriteBase64ToStringBuilder(EOSLobbyDataGenerator.cachedRuleBook, stringBuilder);
				EOSLobbyDataGenerator.ruleBookKeyValueSplitter.SetValue(stringBuilder);
				HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
				return;
			}
			EOSLobbyDataGenerator.cachedRuleBook.SetToDefaults();
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0006EF18 File Offset: 0x0006D118
		private static void SetRuleBookKeyValue(string key, string value)
		{
			int num = -1;
			int i = 0;
			while (i < EOSLobbyDataGenerator.ruleBookKeyValues.Count)
			{
				if (EOSLobbyDataGenerator.ruleBookKeyValues[i].Key.Equals(key, StringComparison.Ordinal))
				{
					if (EOSLobbyDataGenerator.ruleBookKeyValues[i].Value.Equals(value, StringComparison.Ordinal))
					{
						return;
					}
					num = i;
					break;
				}
				else
				{
					i++;
				}
			}
			if (value == null)
			{
				if (num != -1)
				{
					EOSLobbyDataGenerator.ruleBookKeyValues.RemoveAt(num);
				}
			}
			else
			{
				KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(key, value);
				if (num != -1)
				{
					EOSLobbyDataGenerator.ruleBookKeyValues[num] = keyValuePair;
				}
				else
				{
					EOSLobbyDataGenerator.ruleBookKeyValues.Add(keyValuePair);
				}
			}
			EOSLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0006E823 File Offset: 0x0006CA23
		[ConCommand(commandName = "steam_lobby_data_regenerate", flags = ConVarFlags.None, helpText = "Forces the current lobby data to be regenerated.")]
		public static void CCSteamLobbyRegenerateData(ConCommandArgs args)
		{
			EOSLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0006F04F File Offset: 0x0006D24F
		[CompilerGenerated]
		internal static void <RebuildLobbyData>g__AddData|24_0(string key, string value, ref EOSLobbyDataGenerator.<>c__DisplayClass24_0 A_2)
		{
			A_2.newData.Add(key, (value != null) ? value : "");
		}

		// Token: 0x04001FEE RID: 8174
		private const int k_cubChatMetadataMax = 8192;

		// Token: 0x04001FEF RID: 8175
		private const int k_nMaxLobbyKeyLength = 255;

		// Token: 0x04001FF0 RID: 8176
		private static string lobbyId;

		// Token: 0x04001FF1 RID: 8177
		private static int edition;

		// Token: 0x04001FF3 RID: 8179
		private static RuleBook cachedRuleBook;

		// Token: 0x04001FF4 RID: 8180
		private static bool dirty = false;

		// Token: 0x04001FF5 RID: 8181
		private static readonly string[] specialKeys = new string[]
		{
			"joinable",
			"name",
			"appid",
			"lobbytype",
			"total_max_players",
			"v",
			"qp_cutoff_time",
			"qp",
			"starting"
		};

		// Token: 0x04001FF6 RID: 8182
		private static readonly List<KeyValuePair<string, string>> ruleBookKeyValues = new List<KeyValuePair<string, string>>(1);

		// Token: 0x04001FF7 RID: 8183
		private static readonly KeyValueSplitter ruleBookKeyValueSplitter = new KeyValueSplitter("rulebook", 255, 8192, new Action<string, string>(EOSLobbyDataGenerator.SetRuleBookKeyValue));

		// Token: 0x0200058E RID: 1422
		private static class ToStringCache
		{
			// Token: 0x04001FF8 RID: 8184
			public static MemoizedToString<CSteamID, ToStringDefault<CSteamID>> serverId;

			// Token: 0x04001FF9 RID: 8185
			public static MemoizedToString<AddressPortPair, ToStringDefault<AddressPortPair>> serverAddress;

			// Token: 0x04001FFA RID: 8186
			public static MemoizedToString<int, ToStringImplementationInvariant> playerCount;

			// Token: 0x04001FFB RID: 8187
			public static MemoizedToString<int, ToStringImplementationInvariant> edition;

			// Token: 0x04001FFC RID: 8188
			public static MemoizedToString<int, ToStringImplementationInvariant> serverMaxPlayers;

			// Token: 0x04001FFD RID: 8189
			public static MemoizedToString<int, ToStringImplementationInvariant> serverPlayerCount;
		}
	}
}
