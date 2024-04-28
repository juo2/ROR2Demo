using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Facepunch.Steamworks;
using HG;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000A6B RID: 2667
	public static class SteamworksLobbyDataGenerator
	{
		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x06003D2B RID: 15659 RVA: 0x000FCC2C File Offset: 0x000FAE2C
		// (remove) Token: 0x06003D2C RID: 15660 RVA: 0x000FCC60 File Offset: 0x000FAE60
		public static event Action<List<KeyValuePair<string, string>>> getAdditionalKeyValues;

		// Token: 0x06003D2D RID: 15661 RVA: 0x000FCC94 File Offset: 0x000FAE94
		[SystemInitializer(new Type[]
		{
			typeof(RuleBook)
		})]
		private static void Init()
		{
			SteamworksLobbyDataGenerator.cachedRuleBook = new RuleBook();
			if (PlatformSystems.EgsToggleConVar.value == 1)
			{
				return;
			}
			LobbyManager lobbyManager = PlatformSystems.lobbyManager;
			lobbyManager.onLobbyOwnershipGained = (Action)Delegate.Combine(lobbyManager.onLobbyOwnershipGained, new Action(SteamworksLobbyDataGenerator.OnLobbyOwnershipGained));
			LobbyManager lobbyManager2 = PlatformSystems.lobbyManager;
			lobbyManager2.onLobbyOwnershipLost = (Action)Delegate.Combine(lobbyManager2.onLobbyOwnershipLost, new Action(SteamworksLobbyDataGenerator.OnLobbyOwnershipLost));
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x000FCD08 File Offset: 0x000FAF08
		private static void OnLobbyOwnershipGained()
		{
			SteamworksLobbyDataGenerator.lobbyId = new CSteamID(Client.Instance.Lobby.CurrentLobby);
			string data = Client.Instance.Lobby.CurrentLobbyData.GetData("v");
			SteamworksLobbyDataGenerator.edition = 0;
			if (!string.IsNullOrEmpty(data))
			{
				int.TryParse(data, out SteamworksLobbyDataGenerator.edition);
			}
			LobbyManager lobbyManager = PlatformSystems.lobbyManager;
			lobbyManager.onLobbyMemberDataUpdated = (Action<UserID>)Delegate.Combine(lobbyManager.onLobbyMemberDataUpdated, new Action<UserID>(SteamworksLobbyDataGenerator.SteamworksLobbyManagerOnOnLobbyMemberDataUpdated));
			LobbyManager lobbyManager2 = PlatformSystems.lobbyManager;
			lobbyManager2.onLobbyStateChanged = (Action)Delegate.Combine(lobbyManager2.onLobbyStateChanged, new Action(SteamworksLobbyDataGenerator.SteamworksLobbyManagerOnOnLobbyStateChanged));
			NetworkManagerSystem.onStartClientGlobal += SteamworksLobbyDataGenerator.NetworkManagerSystemOnOnStartClientGlobal;
			NetworkManagerSystem.onStopClientGlobal += SteamworksLobbyDataGenerator.NetworkManagerSystemOnOnStopClientGlobal;
			SceneCatalog.onMostRecentSceneDefChanged += SteamworksLobbyDataGenerator.SceneCatalogOnOnMostRecentSceneDefChanged;
			NetworkUser.onNetworkUserDiscovered += SteamworksLobbyDataGenerator.NetworkUserOnOnNetworkUserDiscovered;
			NetworkUser.onNetworkUserLost += SteamworksLobbyDataGenerator.NetworkUserOnOnNetworkUserLost;
			PreGameController.onPreGameControllerSetRuleBookGlobal += SteamworksLobbyDataGenerator.OnPreGameControllerSetRuleBook;
			Run.onRunSetRuleBookGlobal += SteamworksLobbyDataGenerator.OnRunSetRuleBook;
			Client.Instance.Lobby.Name = Language.GetStringFormatted("LOBBY_DEFAULT_NAME_FORMAT", new object[]
			{
				Client.Instance.Username
			});
			SteamworksLobbyDataGenerator.UpdateRuleBook();
			SteamworksLobbyDataGenerator.RebuildLobbyData();
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x000FCE5C File Offset: 0x000FB05C
		private static void OnLobbyOwnershipLost()
		{
			SteamworksLobbyDataGenerator.lobbyId = CSteamID.nil;
			SteamworksLobbyDataGenerator.edition = 0;
			Run.onRunSetRuleBookGlobal -= SteamworksLobbyDataGenerator.OnRunSetRuleBook;
			PreGameController.onPreGameControllerSetRuleBookGlobal -= SteamworksLobbyDataGenerator.OnPreGameControllerSetRuleBook;
			NetworkUser.onNetworkUserLost -= SteamworksLobbyDataGenerator.NetworkUserOnOnNetworkUserLost;
			NetworkUser.onNetworkUserDiscovered -= SteamworksLobbyDataGenerator.NetworkUserOnOnNetworkUserDiscovered;
			SceneCatalog.onMostRecentSceneDefChanged -= SteamworksLobbyDataGenerator.SceneCatalogOnOnMostRecentSceneDefChanged;
			NetworkManagerSystem.onStopClientGlobal -= SteamworksLobbyDataGenerator.NetworkManagerSystemOnOnStopClientGlobal;
			NetworkManagerSystem.onStartClientGlobal -= SteamworksLobbyDataGenerator.NetworkManagerSystemOnOnStartClientGlobal;
			LobbyManager lobbyManager = PlatformSystems.lobbyManager;
			lobbyManager.onLobbyMemberDataUpdated = (Action<UserID>)Delegate.Remove(lobbyManager.onLobbyMemberDataUpdated, new Action<UserID>(SteamworksLobbyDataGenerator.SteamworksLobbyManagerOnOnLobbyMemberDataUpdated));
			LobbyManager lobbyManager2 = PlatformSystems.lobbyManager;
			lobbyManager2.onLobbyStateChanged = (Action)Delegate.Remove(lobbyManager2.onLobbyStateChanged, new Action(SteamworksLobbyDataGenerator.SteamworksLobbyManagerOnOnLobbyStateChanged));
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x000FCF3C File Offset: 0x000FB13C
		private static void OnPreGameControllerSetRuleBook(PreGameController run, RuleBook ruleBook)
		{
			SteamworksLobbyDataGenerator.UpdateRuleBook();
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x000FCF3C File Offset: 0x000FB13C
		private static void OnRunSetRuleBook(Run run, RuleBook ruleBook)
		{
			SteamworksLobbyDataGenerator.UpdateRuleBook();
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x000FCF43 File Offset: 0x000FB143
		private static void SteamworksLobbyManagerOnOnLobbyMemberDataUpdated(UserID memberId)
		{
			SteamworksLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x000FCF43 File Offset: 0x000FB143
		private static void SteamworksLobbyManagerOnOnLobbyStateChanged()
		{
			SteamworksLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x000FCF43 File Offset: 0x000FB143
		private static void NetworkManagerSystemOnOnStartClientGlobal(NetworkClient networkClient)
		{
			SteamworksLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x000FCF43 File Offset: 0x000FB143
		private static void NetworkManagerSystemOnOnStopClientGlobal()
		{
			SteamworksLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x000FCF43 File Offset: 0x000FB143
		private static void SceneCatalogOnOnMostRecentSceneDefChanged(SceneDef sceneDef)
		{
			SteamworksLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x000FCF43 File Offset: 0x000FB143
		private static void NetworkUserOnOnNetworkUserDiscovered(NetworkUser networkUser)
		{
			SteamworksLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x000FCF43 File Offset: 0x000FB143
		private static void NetworkUserOnOnNetworkUserLost(NetworkUser networkUser)
		{
			SteamworksLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x000FCF4A File Offset: 0x000FB14A
		public static void SetDirty()
		{
			if (SteamworksLobbyDataGenerator.dirty)
			{
				return;
			}
			SteamworksLobbyDataGenerator.dirty = true;
			RoR2Application.onNextUpdate += SteamworksLobbyDataGenerator.RebuildLobbyData;
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x000FCF6C File Offset: 0x000FB16C
		public static void RebuildLobbyData()
		{
			try
			{
				SteamworksLobbyDataGenerator.dirty = false;
				Client instance = Client.Instance;
				Lobby lobby = (instance != null) ? instance.Lobby : null;
				if (lobby != null)
				{
					Lobby.LobbyData currentLobbyData = lobby.CurrentLobbyData;
					Dictionary<string, string> dictionary = (currentLobbyData != null) ? currentLobbyData.GetAllData() : null;
					if (lobby.IsOwner && dictionary != null && currentLobbyData != null)
					{
						string text = null;
						dictionary.TryGetValue("total_max_players", out text);
						int value;
						if (text == null || !int.TryParse(text, out value))
						{
							value = LobbyManager.cvSteamLobbyMaxMembers.value;
							currentLobbyData.SetData("total_max_players", value.ToString());
						}
						int num = value - PlatformSystems.lobbyManager.calculatedExtraPlayersCount;
						if (lobby.MaxMembers != num)
						{
							lobby.MaxMembers = num;
						}
						SteamworksLobbyDataGenerator.<>c__DisplayClass24_0 CS$<>8__locals1;
						CS$<>8__locals1.newData = CollectionPool<KeyValuePair<string, string>, Dictionary<string, string>>.RentCollection();
						List<KeyValuePair<string, string>> list = CollectionPool<KeyValuePair<string, string>, List<KeyValuePair<string, string>>>.RentCollection();
						Action<List<KeyValuePair<string, string>>> action = SteamworksLobbyDataGenerator.getAdditionalKeyValues;
						if (action != null)
						{
							action(list);
						}
						for (int i = 0; i < list.Count; i++)
						{
							KeyValuePair<string, string> keyValuePair = list[i];
							SteamworksLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0(keyValuePair.Key, keyValuePair.Value, ref CS$<>8__locals1);
						}
						CollectionPool<KeyValuePair<string, string>, List<KeyValuePair<string, string>>>.ReturnCollection(list);
						SteamworksLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("build_id", RoR2Application.GetBuildId(), ref CS$<>8__locals1);
						SteamworksLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("_mh", NetworkModCompatibilityHelper.networkModHash, ref CS$<>8__locals1);
						string key = "player_count";
						int num2 = PlatformSystems.lobbyManager.calculatedTotalPlayerCount;
						SteamworksLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0(key, SteamworksLobbyDataGenerator.ToStringCache.playerCount.GetString(num2), ref CS$<>8__locals1);
						SceneDef sceneDefForCurrentScene = SceneCatalog.GetSceneDefForCurrentScene();
						string value2 = (sceneDefForCurrentScene != null) ? sceneDefForCurrentScene.baseSceneName : null;
						if (!string.IsNullOrEmpty(value2))
						{
							SteamworksLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("_map", value2, ref CS$<>8__locals1);
						}
						CSteamID a;
						AddressPortPair addressPortPair;
						bool flag;
						SteamworksLobbyDataGenerator.GetServerInfo(out a, out addressPortPair, out flag);
						bool flag2 = false;
						bool flag3 = dictionary != null && dictionary.ContainsKey("server_id");
						bool flag4 = dictionary != null && dictionary.ContainsKey("server_address");
						bool flag5 = flag3 || flag4;
						bool flag6 = false;
						if (NetworkSession.instance)
						{
							flag2 = NetworkSession.instance.HasFlag(NetworkSession.Flags.HasPassword);
							SteamworksLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("_ds", NetworkSession.instance.HasFlag(NetworkSession.Flags.IsDedicatedServer) ? "1" : "0", ref CS$<>8__locals1);
							SteamworksLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("_pw", flag2 ? "1" : "0", ref CS$<>8__locals1);
							SteamworksLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("_svtags", NetworkSession.instance.tagsString, ref CS$<>8__locals1);
							string key2 = "_svmpl";
							num2 = (int)NetworkSession.instance.maxPlayers;
							SteamworksLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0(key2, SteamworksLobbyDataGenerator.ToStringCache.serverMaxPlayers.GetString(num2), ref CS$<>8__locals1);
							string key3 = "_svplc";
							num2 = NetworkUser.readOnlyInstancesList.Count;
							SteamworksLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0(key3, SteamworksLobbyDataGenerator.ToStringCache.serverPlayerCount.GetString(num2), ref CS$<>8__locals1);
							SteamworksLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("_svnm", NetworkSession.instance.serverName, ref CS$<>8__locals1);
							if (a != CSteamID.nil)
							{
								SteamworksLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("server_id", SteamworksLobbyDataGenerator.ToStringCache.serverId.GetString(a), ref CS$<>8__locals1);
								flag6 = true;
							}
							if (addressPortPair.isValid)
							{
								SteamworksLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("server_address", SteamworksLobbyDataGenerator.ToStringCache.serverAddress.GetString(addressPortPair), ref CS$<>8__locals1);
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
							SteamworksLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0("_svgm", gameModeName, ref CS$<>8__locals1);
						}
						if (flag6 && !flag5 && flag2)
						{
							string value3 = flag ? NetworkManagerSystem.SvPasswordConVar.instance.value : NetworkManagerSystem.cvClPassword.value;
							NetworkWriter networkWriter = new NetworkWriter();
							networkWriter.Write(value3);
							(PlatformSystems.lobbyManager as SteamworksLobbyManager).SendLobbyMessage(LobbyManager.LobbyMessageType.Password, networkWriter);
							Debug.Log("Password attempt shared with lobby members to facilitate game join.");
						}
						for (int j = 0; j < SteamworksLobbyDataGenerator.ruleBookKeyValues.Count; j++)
						{
							KeyValuePair<string, string> keyValuePair2 = SteamworksLobbyDataGenerator.ruleBookKeyValues[j];
							SteamworksLobbyDataGenerator.<RebuildLobbyData>g__AddData|24_0(keyValuePair2.Key, keyValuePair2.Value, ref CS$<>8__locals1);
						}
						for (int k = 0; k < SteamworksLobbyDataGenerator.specialKeys.Length; k++)
						{
							dictionary.Remove(SteamworksLobbyDataGenerator.specialKeys[k]);
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
							currentLobbyData.RemoveData(list2[l]);
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
								currentLobbyData.SetData(keyValuePair4.Key, keyValuePair4.Value);
								flag7 = true;
							}
						}
						if (flag7)
						{
							SteamworksLobbyDataGenerator.edition++;
						}
						currentLobbyData.SetData("v", SteamworksLobbyDataGenerator.ToStringCache.edition.GetString(SteamworksLobbyDataGenerator.edition));
						CollectionPool<KeyValuePair<string, string>, Dictionary<string, string>>.ReturnCollection(CS$<>8__locals1.newData);
					}
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x000FD4E4 File Offset: 0x000FB6E4
		private static void GetServerInfo(out CSteamID serverId, out AddressPortPair serverAddress, out bool isSelf)
		{
			serverId = CSteamID.nil;
			serverAddress = default(AddressPortPair);
			isSelf = false;
			HostDescription desiredHost = NetworkManagerSystem.singleton.desiredHost;
			if (desiredHost.hostType != HostDescription.HostType.None)
			{
				if (desiredHost.hostType == HostDescription.HostType.Self)
				{
					if (NetworkServer.active)
					{
						serverId = NetworkManagerSystem.singleton.serverP2PId;
						isSelf = true;
					}
					return;
				}
				if (desiredHost.hostType == HostDescription.HostType.Steam)
				{
					serverId = desiredHost.userID.CID;
					return;
				}
				if (desiredHost.hostType == HostDescription.HostType.IPv4)
				{
					serverAddress = desiredHost.addressPortPair;
					return;
				}
			}
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x000FD570 File Offset: 0x000FB770
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
			if (ruleBook != null && !ruleBook.Equals(SteamworksLobbyDataGenerator.cachedRuleBook))
			{
				SteamworksLobbyDataGenerator.cachedRuleBook.Copy(ruleBook);
				StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
				RuleBook.WriteBase64ToStringBuilder(SteamworksLobbyDataGenerator.cachedRuleBook, stringBuilder);
				SteamworksLobbyDataGenerator.ruleBookKeyValueSplitter.SetValue(stringBuilder);
				HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
				return;
			}
			SteamworksLobbyDataGenerator.cachedRuleBook.SetToDefaults();
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x000FD5F8 File Offset: 0x000FB7F8
		private static void SetRuleBookKeyValue(string key, string value)
		{
			int num = -1;
			int i = 0;
			while (i < SteamworksLobbyDataGenerator.ruleBookKeyValues.Count)
			{
				if (SteamworksLobbyDataGenerator.ruleBookKeyValues[i].Key.Equals(key, StringComparison.Ordinal))
				{
					if (SteamworksLobbyDataGenerator.ruleBookKeyValues[i].Value.Equals(value, StringComparison.Ordinal))
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
					SteamworksLobbyDataGenerator.ruleBookKeyValues.RemoveAt(num);
				}
			}
			else
			{
				KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(key, value);
				if (num != -1)
				{
					SteamworksLobbyDataGenerator.ruleBookKeyValues[num] = keyValuePair;
				}
				else
				{
					SteamworksLobbyDataGenerator.ruleBookKeyValues.Add(keyValuePair);
				}
			}
			SteamworksLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x000FCF43 File Offset: 0x000FB143
		[ConCommand(commandName = "steam_lobby_data_regenerate", flags = ConVarFlags.None, helpText = "Forces the current Steamworks lobby data to be regenerated.")]
		public static void CCSteamLobbyRegenerateData(ConCommandArgs args)
		{
			SteamworksLobbyDataGenerator.SetDirty();
		}

		// Token: 0x06003D40 RID: 15680 RVA: 0x000FD72F File Offset: 0x000FB92F
		[CompilerGenerated]
		internal static void <RebuildLobbyData>g__AddData|24_0(string key, string value, ref SteamworksLobbyDataGenerator.<>c__DisplayClass24_0 A_2)
		{
			A_2.newData.Add(key, value);
		}

		// Token: 0x04003C48 RID: 15432
		private const int k_cubChatMetadataMax = 8192;

		// Token: 0x04003C49 RID: 15433
		private const int k_nMaxLobbyKeyLength = 255;

		// Token: 0x04003C4A RID: 15434
		private static CSteamID lobbyId;

		// Token: 0x04003C4B RID: 15435
		private static int edition;

		// Token: 0x04003C4D RID: 15437
		private static RuleBook cachedRuleBook;

		// Token: 0x04003C4E RID: 15438
		private static bool dirty = false;

		// Token: 0x04003C4F RID: 15439
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

		// Token: 0x04003C50 RID: 15440
		private static readonly List<KeyValuePair<string, string>> ruleBookKeyValues = new List<KeyValuePair<string, string>>(1);

		// Token: 0x04003C51 RID: 15441
		private static readonly KeyValueSplitter ruleBookKeyValueSplitter = new KeyValueSplitter("rulebook", 255, 8192, new Action<string, string>(SteamworksLobbyDataGenerator.SetRuleBookKeyValue));

		// Token: 0x02000A6C RID: 2668
		private static class ToStringCache
		{
			// Token: 0x04003C52 RID: 15442
			public static MemoizedToString<CSteamID, ToStringDefault<CSteamID>> serverId;

			// Token: 0x04003C53 RID: 15443
			public static MemoizedToString<AddressPortPair, ToStringDefault<AddressPortPair>> serverAddress;

			// Token: 0x04003C54 RID: 15444
			public static MemoizedToString<int, ToStringImplementationInvariant> playerCount;

			// Token: 0x04003C55 RID: 15445
			public static MemoizedToString<int, ToStringImplementationInvariant> edition;

			// Token: 0x04003C56 RID: 15446
			public static MemoizedToString<int, ToStringImplementationInvariant> serverMaxPlayers;

			// Token: 0x04003C57 RID: 15447
			public static MemoizedToString<int, ToStringImplementationInvariant> serverPlayerCount;
		}
	}
}
