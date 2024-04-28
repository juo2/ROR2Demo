using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Facepunch.Steamworks;
using RoR2.ConVar;
using RoR2.Networking;

namespace RoR2
{
	// Token: 0x020009B7 RID: 2487
	public abstract class LobbyManager
	{
		// Token: 0x060038C4 RID: 14532
		public abstract MPFeatures GetPlatformMPFeatureFlags();

		// Token: 0x060038C5 RID: 14533
		public abstract MPLobbyFeatures GetPlatformMPLobbyFeatureFlags();

		// Token: 0x060038C6 RID: 14534 RVA: 0x000EDFC7 File Offset: 0x000EC1C7
		public bool HasMPLobbyUI()
		{
			return this.GetPlatformMPLobbyFeatureFlags() > MPLobbyFeatures.None;
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x000EDFD2 File Offset: 0x000EC1D2
		public bool HasMPFeature(MPFeatures flags)
		{
			return this.GetPlatformMPFeatureFlags().HasFlag(flags);
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x000EDFEA File Offset: 0x000EC1EA
		public bool HasMPLobbyFeature(MPLobbyFeatures flags)
		{
			return this.GetPlatformMPLobbyFeatureFlags().HasFlag(flags);
		}

		// Token: 0x060038C9 RID: 14537
		public abstract int GetLobbyMemberPlayerCountByIndex(int memberIndex);

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060038CA RID: 14538
		// (set) Token: 0x060038CB RID: 14539
		public abstract int calculatedTotalPlayerCount { get; protected set; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060038CC RID: 14540
		// (set) Token: 0x060038CD RID: 14541
		public abstract int calculatedExtraPlayersCount { get; protected set; }

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060038CE RID: 14542
		// (set) Token: 0x060038CF RID: 14543
		public abstract bool isInLobby { get; protected set; }

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060038D0 RID: 14544
		// (set) Token: 0x060038D1 RID: 14545
		public abstract bool ownsLobby { get; protected set; }

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060038D2 RID: 14546
		// (set) Token: 0x060038D3 RID: 14547
		public abstract bool IsBusy { get; set; }

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000EE002 File Offset: 0x000EC202
		// (set) Token: 0x060038D5 RID: 14549 RVA: 0x000EE00A File Offset: 0x000EC20A
		public bool awaitingJoin { get; protected set; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x000EE013 File Offset: 0x000EC213
		// (set) Token: 0x060038D7 RID: 14551 RVA: 0x000EE01B File Offset: 0x000EC21B
		public bool awaitingCreate { get; protected set; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060038D8 RID: 14552 RVA: 0x000EE024 File Offset: 0x000EC224
		// (set) Token: 0x060038D9 RID: 14553 RVA: 0x000EE02C File Offset: 0x000EC22C
		public bool isFull { get; protected set; }

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060038DA RID: 14554
		public abstract bool hasMinimumPlayerCount { get; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060038DB RID: 14555
		// (set) Token: 0x060038DC RID: 14556
		public abstract LobbyType currentLobbyType { get; set; }

		// Token: 0x060038DD RID: 14557
		public abstract void CreateLobby();

		// Token: 0x060038DE RID: 14558
		public abstract string GetLobbyID();

		// Token: 0x060038DF RID: 14559 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void SetNetworkType(bool isInternet)
		{
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public virtual bool IsNetworkTypeInternet()
		{
			return true;
		}

		// Token: 0x060038E1 RID: 14561
		public abstract void LeaveLobby();

		// Token: 0x060038E2 RID: 14562
		public abstract void JoinLobby(UserID join);

		// Token: 0x060038E3 RID: 14563 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void Shutdown()
		{
		}

		// Token: 0x060038E4 RID: 14564
		public abstract void OnStartPrivateGame();

		// Token: 0x060038E5 RID: 14565
		public abstract void AutoMatchmake();

		// Token: 0x060038E6 RID: 14566
		public abstract void ToggleQuickplay();

		// Token: 0x060038E7 RID: 14567 RVA: 0x000EE035 File Offset: 0x000EC235
		public void OnMultiplayerMenuEnabled(Action<UserID> onLobbyLeave)
		{
			if (!PlatformSystems.lobbyManager.isInLobby)
			{
				PlatformSystems.lobbyManager.CreateLobby();
			}
			LobbyManager lobbyManager = PlatformSystems.lobbyManager;
			lobbyManager.onLobbyLeave = (Action<UserID>)Delegate.Combine(lobbyManager.onLobbyLeave, onLobbyLeave);
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060038E8 RID: 14568
		// (set) Token: 0x060038E9 RID: 14569
		public abstract LobbyManager.LobbyData newestLobbyData { get; protected set; }

		// Token: 0x060038EA RID: 14570
		public abstract bool IsLobbyOwner(UserID user);

		// Token: 0x060038EB RID: 14571
		public abstract string GetUserDisplayName(UserID user);

		// Token: 0x060038EC RID: 14572
		public abstract bool IsLobbyOwner();

		// Token: 0x060038ED RID: 14573
		public abstract UserID[] GetLobbyMembers();

		// Token: 0x060038EE RID: 14574
		public abstract bool ShouldShowPromoteButton();

		// Token: 0x060038EF RID: 14575
		public abstract void CheckIfInitializedAndValid();

		// Token: 0x060038F0 RID: 14576
		public abstract void CheckIfInvited();

		// Token: 0x060038F1 RID: 14577
		public abstract bool CanInvite();

		// Token: 0x060038F2 RID: 14578
		public abstract void CheckBusyTimer();

		// Token: 0x060038F3 RID: 14579
		public abstract bool ShouldEnableQuickplayButton();

		// Token: 0x060038F4 RID: 14580
		public abstract bool ShouldEnableStartPrivateGameButton();

		// Token: 0x060038F5 RID: 14581
		public abstract void OpenInviteOverlay();

		// Token: 0x060038F6 RID: 14582
		public abstract void SetQuickplayCutoffTime(double cutoffTime);

		// Token: 0x060038F7 RID: 14583
		public abstract double GetQuickplayCutoffTime();

		// Token: 0x060038F8 RID: 14584
		public abstract void OnCutoffTimerComplete();

		// Token: 0x04003891 RID: 14481
		public LobbyManager.State state;

		// Token: 0x04003892 RID: 14482
		public LobbyType preferredLobbyType;

		// Token: 0x04003893 RID: 14483
		public Action onPlayerCountUpdated;

		// Token: 0x04003894 RID: 14484
		public Action<bool> onLobbyJoined;

		// Token: 0x04003895 RID: 14485
		public Action onLobbiesUpdated;

		// Token: 0x04003896 RID: 14486
		public Action onLobbyOwnershipGained;

		// Token: 0x04003897 RID: 14487
		public Action onLobbyOwnershipLost;

		// Token: 0x04003898 RID: 14488
		public Action onLobbyChanged;

		// Token: 0x04003899 RID: 14489
		public Action onLobbyDataUpdated;

		// Token: 0x0400389A RID: 14490
		public Action<UserID> onLobbyLeave;

		// Token: 0x0400389B RID: 14491
		public Action<UserID> onLobbyMemberDataUpdated;

		// Token: 0x0400389C RID: 14492
		public Action onLobbyStateChanged;

		// Token: 0x040038A0 RID: 14496
		public static readonly IntConVar cvSteamLobbyMaxMembers = new IntConVar("steam_lobby_max_members", ConVarFlags.None, RoR2Application.maxPlayers.ToString(CultureInfo.InvariantCulture), "Sets the maximum number of players allowed in steam lobbies created by this machine.");

		// Token: 0x020009B8 RID: 2488
		public enum State
		{
			// Token: 0x040038A2 RID: 14498
			Idle,
			// Token: 0x040038A3 RID: 14499
			Hosting,
			// Token: 0x040038A4 RID: 14500
			Waiting
		}

		// Token: 0x020009B9 RID: 2489
		public enum LobbyMessageType : byte
		{
			// Token: 0x040038A6 RID: 14502
			Chat,
			// Token: 0x040038A7 RID: 14503
			Password
		}

		// Token: 0x020009BA RID: 2490
		public class LobbyDataSetupState
		{
			// Token: 0x040038A8 RID: 14504
			public int totalMaxPlayers;

			// Token: 0x040038A9 RID: 14505
			public int totalPlayerCount;

			// Token: 0x040038AA RID: 14506
			public bool quickplayQueued;

			// Token: 0x040038AB RID: 14507
			public UserID lobbyId;

			// Token: 0x040038AC RID: 14508
			public UserID serverId;

			// Token: 0x040038AD RID: 14509
			public AddressPortPair serverAddressPortPair;

			// Token: 0x040038AE RID: 14510
			public bool starting;

			// Token: 0x040038AF RID: 14511
			public string buildId = "0";

			// Token: 0x040038B0 RID: 14512
			public DateTime? quickplayCutoffTime;

			// Token: 0x040038B1 RID: 14513
			public bool shouldConnect;

			// Token: 0x040038B2 RID: 14514
			public bool joinable;
		}

		// Token: 0x020009BB RID: 2491
		public class LobbyData
		{
			// Token: 0x060038FC RID: 14588 RVA: 0x000EE0B0 File Offset: 0x000EC2B0
			public LobbyData(LobbyManager.LobbyDataSetupState setupState)
			{
				this.totalMaxPlayers = setupState.totalMaxPlayers;
				this.totalPlayerCount = setupState.totalPlayerCount;
				this.quickplayQueued = setupState.quickplayQueued;
				this.lobbyId = setupState.lobbyId;
				this.serverId = setupState.serverId;
				this.serverAddressPortPair = setupState.serverAddressPortPair;
				this.starting = setupState.starting;
				this.buildId = setupState.buildId;
				this.quickplayCutoffTime = setupState.quickplayCutoffTime;
				this.shouldConnect = setupState.shouldConnect;
				this.joinable = setupState.joinable;
			}

			// Token: 0x060038FD RID: 14589 RVA: 0x000EE152 File Offset: 0x000EC352
			public LobbyData()
			{
			}

			// Token: 0x060038FE RID: 14590 RVA: 0x000EE168 File Offset: 0x000EC368
			public static bool TryParseUserID(string str, out UserID result)
			{
				CSteamID cid;
				if (CSteamID.TryParse(str, out cid))
				{
					result = new UserID(cid);
					return true;
				}
				result.CID = CSteamID.nil;
				return false;
			}

			// Token: 0x060038FF RID: 14591 RVA: 0x000EE19C File Offset: 0x000EC39C
			public LobbyData(Lobby lobby)
			{
				UserID userID = new UserID(new CSteamID(lobby.CurrentLobby));
				Lobby.LobbyData currentLobbyData = lobby.CurrentLobbyData;
				LobbyManager.LobbyData.<>c__DisplayClass14_0 CS$<>8__locals1;
				CS$<>8__locals1.lobbyDataDictionary = currentLobbyData.GetAllData();
				this.lobbyId = userID;
				LobbyManager.LobbyData.<.ctor>g__ReadCSteamID|14_3("server_id", ref this.serverId, ref CS$<>8__locals1);
				LobbyManager.LobbyData.<.ctor>g__ReadAddressPortPair|14_4("server_address", ref this.serverAddressPortPair, ref CS$<>8__locals1);
				LobbyManager.LobbyData.<.ctor>g__ReadInt|14_2("total_max_players", ref this.totalMaxPlayers, ref CS$<>8__locals1);
				LobbyManager.LobbyData.<.ctor>g__ReadInt|14_2("player_count", ref this.totalPlayerCount, ref CS$<>8__locals1);
				LobbyManager.LobbyData.<.ctor>g__ReadBool|14_1("qp", ref this.quickplayQueued, ref CS$<>8__locals1);
				LobbyManager.LobbyData.<.ctor>g__ReadBool|14_1("starting", ref this.starting, ref CS$<>8__locals1);
				LobbyManager.LobbyData.<.ctor>g__ReadString|14_0("build_id", ref this.buildId, ref CS$<>8__locals1);
				LobbyManager.LobbyData.<.ctor>g__ReadNullableDate|14_5("qp_cutoff_time", out this.quickplayCutoffTime, ref CS$<>8__locals1);
				this.joinable = true;
				this.joinable &= (this.totalPlayerCount < this.totalMaxPlayers);
				this.joinable &= (lobby.LobbyType == Lobby.Type.Public);
				this.shouldConnect = (this.serverId.CID.isValid || this.serverAddressPortPair.isValid);
			}

			// Token: 0x06003900 RID: 14592 RVA: 0x000EE2E0 File Offset: 0x000EC4E0
			[CompilerGenerated]
			internal static bool <.ctor>g__ReadString|14_0(string metaDataName, ref string field, ref LobbyManager.LobbyData.<>c__DisplayClass14_0 A_2)
			{
				string text;
				if (A_2.lobbyDataDictionary.TryGetValue(metaDataName, out text))
				{
					field = text;
					return true;
				}
				return false;
			}

			// Token: 0x06003901 RID: 14593 RVA: 0x000EE304 File Offset: 0x000EC504
			[CompilerGenerated]
			internal static bool <.ctor>g__ReadBool|14_1(string metaDataName, ref bool field, ref LobbyManager.LobbyData.<>c__DisplayClass14_0 A_2)
			{
				string s;
				int num;
				if (A_2.lobbyDataDictionary.TryGetValue(metaDataName, out s) && TextSerialization.TryParseInvariant(s, out num))
				{
					field = (num != 0);
					return true;
				}
				return false;
			}

			// Token: 0x06003902 RID: 14594 RVA: 0x000EE334 File Offset: 0x000EC534
			[CompilerGenerated]
			internal static bool <.ctor>g__ReadInt|14_2(string metaDataName, ref int field, ref LobbyManager.LobbyData.<>c__DisplayClass14_0 A_2)
			{
				string s;
				int num;
				if (A_2.lobbyDataDictionary.TryGetValue(metaDataName, out s) && TextSerialization.TryParseInvariant(s, out num))
				{
					field = num;
					return true;
				}
				return false;
			}

			// Token: 0x06003903 RID: 14595 RVA: 0x000EE364 File Offset: 0x000EC564
			[CompilerGenerated]
			internal static bool <.ctor>g__ReadCSteamID|14_3(string metaDataName, ref UserID field, ref LobbyManager.LobbyData.<>c__DisplayClass14_0 A_2)
			{
				string str;
				UserID userID;
				if (A_2.lobbyDataDictionary.TryGetValue(metaDataName, out str) && LobbyManager.LobbyData.TryParseUserID(str, out userID))
				{
					field = userID;
					return true;
				}
				return false;
			}

			// Token: 0x06003904 RID: 14596 RVA: 0x000EE398 File Offset: 0x000EC598
			[CompilerGenerated]
			internal static bool <.ctor>g__ReadAddressPortPair|14_4(string metaDataName, ref AddressPortPair field, ref LobbyManager.LobbyData.<>c__DisplayClass14_0 A_2)
			{
				string str;
				AddressPortPair addressPortPair;
				if (A_2.lobbyDataDictionary.TryGetValue(metaDataName, out str) && AddressPortPair.TryParse(str, out addressPortPair))
				{
					field = addressPortPair;
					return true;
				}
				return false;
			}

			// Token: 0x06003905 RID: 14597 RVA: 0x000EE3CC File Offset: 0x000EC5CC
			[CompilerGenerated]
			internal static bool <.ctor>g__ReadNullableDate|14_5(string metaDataName, out DateTime? field, ref LobbyManager.LobbyData.<>c__DisplayClass14_0 A_2)
			{
				string s;
				uint num;
				if (A_2.lobbyDataDictionary.TryGetValue(metaDataName, out s) && TextSerialization.TryParseInvariant(s, out num))
				{
					field = new DateTime?(Util.dateZero + TimeSpan.FromSeconds(num));
					return true;
				}
				field = null;
				return false;
			}

			// Token: 0x040038B3 RID: 14515
			public readonly int totalMaxPlayers;

			// Token: 0x040038B4 RID: 14516
			public readonly int totalPlayerCount;

			// Token: 0x040038B5 RID: 14517
			public readonly bool quickplayQueued;

			// Token: 0x040038B6 RID: 14518
			public readonly UserID lobbyId;

			// Token: 0x040038B7 RID: 14519
			public readonly UserID serverId;

			// Token: 0x040038B8 RID: 14520
			public readonly AddressPortPair serverAddressPortPair;

			// Token: 0x040038B9 RID: 14521
			public readonly bool starting;

			// Token: 0x040038BA RID: 14522
			public readonly string buildId = "0";

			// Token: 0x040038BB RID: 14523
			public readonly DateTime? quickplayCutoffTime;

			// Token: 0x040038BC RID: 14524
			public readonly bool shouldConnect;

			// Token: 0x040038BD RID: 14525
			public readonly bool joinable;
		}
	}
}
