using System;
using System.Collections.Generic;
using EntityStates;
using Facepunch.Steamworks;
using RoR2.UI.MainMenu;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C4D RID: 3149
	public class SteamLobbyFinder : MonoBehaviour
	{
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06004747 RID: 18247 RVA: 0x000F054C File Offset: 0x000EE74C
		private static Client steamClient
		{
			get
			{
				return Client.Instance;
			}
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x0012655A File Offset: 0x0012475A
		private void Awake()
		{
			this.stateMachine = base.gameObject.AddComponent<EntityStateMachine>();
			this.stateMachine.initialStateType = new SerializableEntityStateType(typeof(SteamLobbyFinder.LobbyStateStart));
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x00126587 File Offset: 0x00124787
		private void OnDestroy()
		{
			UnityEngine.Object.Destroy(this.stateMachine);
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x00126594 File Offset: 0x00124794
		private void Update()
		{
			if (SteamLobbyFinder.steamClient.Lobby.IsValid && !PlatformSystems.lobbyManager.ownsLobby)
			{
				if (this.stateMachine.state.GetType() != typeof(SteamLobbyFinder.LobbyStateNonLeader))
				{
					this.stateMachine.SetNextState(new SteamLobbyFinder.LobbyStateNonLeader());
				}
				return;
			}
			this.refreshTimer -= Time.unscaledDeltaTime;
			this.age += Time.unscaledDeltaTime;
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x00126614 File Offset: 0x00124814
		private void RequestLobbyListRefresh()
		{
			LobbyList.Filter filter = new LobbyList.Filter();
			filter.StringFilters["appid"] = TextSerialization.ToStringInvariant(SteamLobbyFinder.steamClient.AppId);
			filter.StringFilters["build_id"] = RoR2Application.GetBuildId();
			filter.StringFilters["qp"] = "1";
			filter.StringFilters["total_max_players"] = TextSerialization.ToStringInvariant(RoR2Application.maxPlayers);
			LobbyList.Filter filter2 = filter;
			(PlatformSystems.lobbyManager as SteamworksLobbyManager).RequestLobbyList(this, filter2, new Action<List<LobbyList.Lobby>>(this.OnLobbyListReceived));
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x001266A8 File Offset: 0x001248A8
		private void OnLobbyListReceived(List<LobbyList.Lobby> newLobbies)
		{
			if (!this)
			{
				return;
			}
			SteamLobbyFinder.LobbyStateBase lobbyStateBase = this.stateMachine.state as SteamLobbyFinder.LobbyStateBase;
			if (lobbyStateBase == null)
			{
				return;
			}
			lobbyStateBase.OnLobbiesUpdated();
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x001266CD File Offset: 0x001248CD
		private static bool CanJoinLobby(int currentLobbySize, LobbyList.Lobby lobby)
		{
			return currentLobbySize + SteamLobbyFinder.GetRealLobbyPlayerCount(lobby) <= lobby.MemberLimit;
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x001266E4 File Offset: 0x001248E4
		private static int GetRealLobbyPlayerCount(LobbyList.Lobby lobby)
		{
			string data = lobby.GetData("player_count");
			int result;
			if (data != null && TextSerialization.TryParseInvariant(data, out result))
			{
				return result;
			}
			return SteamLobbyFinder.steamClient.Lobby.MaxMembers;
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x0012671B File Offset: 0x0012491B
		private static int GetCurrentLobbyRealPlayerCount()
		{
			return PlatformSystems.lobbyManager.newestLobbyData.totalPlayerCount;
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06004750 RID: 18256 RVA: 0x0012672C File Offset: 0x0012492C
		// (set) Token: 0x06004751 RID: 18257 RVA: 0x00126733 File Offset: 0x00124933
		public static bool userRequestedQuickplayQueue
		{
			get
			{
				return SteamLobbyFinder._userRequestedQuickplayQueue;
			}
			set
			{
				if (SteamLobbyFinder._userRequestedQuickplayQueue != value)
				{
					SteamLobbyFinder._userRequestedQuickplayQueue = value;
					SteamLobbyFinder.running = (SteamLobbyFinder._lobbyIsInQuickplayQueue || SteamLobbyFinder._userRequestedQuickplayQueue);
				}
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06004752 RID: 18258 RVA: 0x00126757 File Offset: 0x00124957
		// (set) Token: 0x06004753 RID: 18259 RVA: 0x0012675E File Offset: 0x0012495E
		private static bool lobbyIsInQuickplayQueue
		{
			get
			{
				return SteamLobbyFinder._lobbyIsInQuickplayQueue;
			}
			set
			{
				if (SteamLobbyFinder._lobbyIsInQuickplayQueue != value)
				{
					SteamLobbyFinder._lobbyIsInQuickplayQueue = value;
					SteamLobbyFinder.running = (SteamLobbyFinder._lobbyIsInQuickplayQueue || SteamLobbyFinder._userRequestedQuickplayQueue);
				}
			}
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x00126782 File Offset: 0x00124982
		[ConCommand(commandName = "steam_quickplay_start")]
		private static void CCSteamQuickplayStart(ConCommandArgs args)
		{
			NetworkManagerSystemSteam.CheckSteamworks();
			SteamLobbyFinder.userRequestedQuickplayQueue = true;
			(PlatformSystems.lobbyManager as SteamworksLobbyManager).SetLobbyQuickPlayQueuedIfOwner(true);
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x0012679F File Offset: 0x0012499F
		[ConCommand(commandName = "steam_quickplay_stop")]
		private static void CCSteamQuickplayStop(ConCommandArgs args)
		{
			NetworkManagerSystemSteam.CheckSteamworks();
			SteamLobbyFinder.userRequestedQuickplayQueue = false;
			PlatformSystems.lobbyManager.CreateLobby();
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x001267B8 File Offset: 0x001249B8
		public static void Init()
		{
			LobbyManager lobbyManager = PlatformSystems.lobbyManager;
			lobbyManager.onLobbyDataUpdated = (Action)Delegate.Combine(lobbyManager.onLobbyDataUpdated, new Action(SteamLobbyFinder.OnLobbyDataUpdated));
			NetworkManagerSystem.onStartClientGlobal += delegate(NetworkClient client)
			{
				(PlatformSystems.lobbyManager as SteamworksLobbyManager).SetLobbyQuickPlayQueuedIfOwner(false);
				SteamLobbyFinder.userRequestedQuickplayQueue = false;
			};
			LobbyManager lobbyManager2 = PlatformSystems.lobbyManager;
			lobbyManager2.onLobbyOwnershipGained = (Action)Delegate.Combine(lobbyManager2.onLobbyOwnershipGained, new Action(delegate()
			{
				if (PlatformSystems.lobbyManager.newestLobbyData.quickplayQueued)
				{
					SteamLobbyFinder.userRequestedQuickplayQueue = true;
				}
			}));
			LobbyManager lobbyManager3 = PlatformSystems.lobbyManager;
			lobbyManager3.onLobbyOwnershipLost = (Action)Delegate.Combine(lobbyManager3.onLobbyOwnershipLost, new Action(delegate()
			{
				SteamLobbyFinder.userRequestedQuickplayQueue = false;
			}));
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x00126881 File Offset: 0x00124A81
		private static void OnLobbyDataUpdated()
		{
			SteamLobbyFinder.lobbyIsInQuickplayQueue = PlatformSystems.lobbyManager.newestLobbyData.quickplayQueued;
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x00126898 File Offset: 0x00124A98
		public static string GetResolvedStateString()
		{
			if (!SteamLobbyFinder.steamClient.Lobby.IsValid)
			{
				return Language.GetString("STEAM_LOBBY_STATUS_NOT_IN_LOBBY");
			}
			bool flag = SteamLobbyFinder.steamClient.Lobby.LobbyType == Lobby.Type.Public;
			if (SteamLobbyFinder.instance)
			{
				bool flag2 = SteamLobbyFinder.instance.stateMachine.state is SteamLobbyFinder.LobbyStateSingleSearch;
			}
			int totalPlayerCount = PlatformSystems.lobbyManager.newestLobbyData.totalPlayerCount;
			int totalMaxPlayers = PlatformSystems.lobbyManager.newestLobbyData.totalMaxPlayers;
			bool isFull = PlatformSystems.lobbyManager.isFull;
			string token = string.Empty;
			object[] args = Array.Empty<object>();
			if (NetworkManagerSystem.singleton.isHost || (MultiplayerMenuController.instance && MultiplayerMenuController.instance.isInHostingState))
			{
				token = "STEAM_LOBBY_STATUS_STARTING_SERVER";
			}
			else if (PlatformSystems.lobbyManager.newestLobbyData.starting)
			{
				token = "STEAM_LOBBY_STATUS_GAME_STARTING";
			}
			else if (PlatformSystems.lobbyManager.newestLobbyData.shouldConnect)
			{
				token = "STEAM_LOBBY_STATUS_CONNECTING_TO_HOST";
			}
			else if (SteamLobbyFinder.instance && SteamLobbyFinder.instance.stateMachine.state is SteamLobbyFinder.LobbyStateStart)
			{
				token = "STEAM_LOBBY_STATUS_LAUNCHING_QUICKPLAY";
			}
			else if (PlatformSystems.lobbyManager.isInLobby)
			{
				if (PlatformSystems.lobbyManager.newestLobbyData.quickplayQueued)
				{
					if (!flag)
					{
						token = "STEAM_LOBBY_STATUS_QUICKPLAY_SEARCHING_FOR_EXISTING_LOBBY";
					}
					else
					{
						DateTime d = Util.UnixTimeStampToDateTimeUtc(SteamLobbyFinder.steamClient.Utils.GetServerRealTime());
						DateTime? quickplayCutoffTime = PlatformSystems.lobbyManager.newestLobbyData.quickplayCutoffTime;
						if (quickplayCutoffTime == null)
						{
							token = "STEAM_LOBBY_STATUS_QUICKPLAY_WAITING_BELOW_MINIMUM_PLAYERS";
							args = new object[]
							{
								PlatformSystems.lobbyManager.newestLobbyData.totalPlayerCount,
								PlatformSystems.lobbyManager.newestLobbyData.totalMaxPlayers
							};
						}
						else
						{
							TimeSpan timeSpan = quickplayCutoffTime.Value - d;
							token = "STEAM_LOBBY_STATUS_QUICKPLAY_WAITING_ABOVE_MINIMUM_PLAYERS";
							args = new object[]
							{
								PlatformSystems.lobbyManager.newestLobbyData.totalPlayerCount,
								PlatformSystems.lobbyManager.newestLobbyData.totalMaxPlayers,
								Math.Max(0.0, timeSpan.TotalSeconds)
							};
						}
					}
				}
				else
				{
					token = "STEAM_LOBBY_STATUS_OUT_OF_QUICKPLAY";
				}
			}
			return Language.GetStringFormatted(token, args);
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06004759 RID: 18265 RVA: 0x00126AD9 File Offset: 0x00124CD9
		// (set) Token: 0x0600475A RID: 18266 RVA: 0x00126AE8 File Offset: 0x00124CE8
		public static bool running
		{
			get
			{
				return SteamLobbyFinder.instance;
			}
			private set
			{
				if (SteamLobbyFinder.instance != value)
				{
					if (value)
					{
						SteamLobbyFinder.instance = new GameObject("SteamLobbyFinder", new Type[]
						{
							typeof(SteamLobbyFinder),
							typeof(SetDontDestroyOnLoad)
						}).GetComponent<SteamLobbyFinder>();
						return;
					}
					UnityEngine.Object.Destroy(SteamLobbyFinder.instance.gameObject);
					SteamLobbyFinder.instance = null;
				}
			}
		}

		// Token: 0x040044E0 RID: 17632
		private float age;

		// Token: 0x040044E1 RID: 17633
		public float joinOnlyDuration = 5f;

		// Token: 0x040044E2 RID: 17634
		public float waitForFullDuration = 30f;

		// Token: 0x040044E3 RID: 17635
		public float startDelayDuration = 1f;

		// Token: 0x040044E4 RID: 17636
		public float refreshInterval = 2f;

		// Token: 0x040044E5 RID: 17637
		private float refreshTimer;

		// Token: 0x040044E6 RID: 17638
		private EntityStateMachine stateMachine;

		// Token: 0x040044E7 RID: 17639
		private static SteamLobbyFinder instance;

		// Token: 0x040044E8 RID: 17640
		private static bool _userRequestedQuickplayQueue;

		// Token: 0x040044E9 RID: 17641
		private static bool _lobbyIsInQuickplayQueue;

		// Token: 0x02000C4E RID: 3150
		private class LobbyStateBase : BaseState
		{
			// Token: 0x0600475D RID: 18269 RVA: 0x00126B84 File Offset: 0x00124D84
			public override void OnEnter()
			{
				base.OnEnter();
				this.lobbyFinder = base.GetComponent<SteamLobbyFinder>();
				LobbyManager lobbyManager = PlatformSystems.lobbyManager;
				lobbyManager.onLobbyOwnershipGained = (Action)Delegate.Combine(lobbyManager.onLobbyOwnershipGained, new Action(this.OnLobbyOwnershipGained));
				LobbyManager lobbyManager2 = PlatformSystems.lobbyManager;
				lobbyManager2.onLobbyOwnershipLost = (Action)Delegate.Combine(lobbyManager2.onLobbyOwnershipLost, new Action(this.OnLobbyOwnershipLost));
			}

			// Token: 0x0600475E RID: 18270 RVA: 0x00126BF0 File Offset: 0x00124DF0
			public override void OnExit()
			{
				LobbyManager lobbyManager = PlatformSystems.lobbyManager;
				lobbyManager.onLobbyOwnershipGained = (Action)Delegate.Remove(lobbyManager.onLobbyOwnershipGained, new Action(this.OnLobbyOwnershipGained));
				LobbyManager lobbyManager2 = PlatformSystems.lobbyManager;
				lobbyManager2.onLobbyOwnershipLost = (Action)Delegate.Remove(lobbyManager2.onLobbyOwnershipLost, new Action(this.OnLobbyOwnershipLost));
				base.OnExit();
			}

			// Token: 0x0600475F RID: 18271 RVA: 0x000026ED File Offset: 0x000008ED
			public virtual void OnLobbiesUpdated()
			{
			}

			// Token: 0x06004760 RID: 18272 RVA: 0x00126C4F File Offset: 0x00124E4F
			private void OnLobbyOwnershipGained()
			{
				this.outer.SetNextState(new SteamLobbyFinder.LobbyStateStart());
			}

			// Token: 0x06004761 RID: 18273 RVA: 0x00126C61 File Offset: 0x00124E61
			private void OnLobbyOwnershipLost()
			{
				this.outer.SetNextState(new SteamLobbyFinder.LobbyStateNonLeader());
			}

			// Token: 0x040044EA RID: 17642
			protected SteamLobbyFinder lobbyFinder;
		}

		// Token: 0x02000C4F RID: 3151
		private class LobbyStateNonLeader : SteamLobbyFinder.LobbyStateBase
		{
			// Token: 0x06004763 RID: 18275 RVA: 0x00126C74 File Offset: 0x00124E74
			public override void Update()
			{
				base.Update();
				if (PlatformSystems.lobbyManager.ownsLobby)
				{
					if (PlatformSystems.lobbyManager.hasMinimumPlayerCount)
					{
						this.outer.SetNextState(new SteamLobbyFinder.LobbyStateMultiSearch());
						return;
					}
					this.outer.SetNextState(new SteamLobbyFinder.LobbyStateSingleSearch());
				}
			}
		}

		// Token: 0x02000C50 RID: 3152
		private class LobbyStateStart : SteamLobbyFinder.LobbyStateBase
		{
			// Token: 0x06004765 RID: 18277 RVA: 0x00126CC8 File Offset: 0x00124EC8
			public override void Update()
			{
				base.Update();
				if (this.lobbyFinder.startDelayDuration <= base.age)
				{
					this.outer.SetNextState(PlatformSystems.lobbyManager.hasMinimumPlayerCount ? new SteamLobbyFinder.LobbyStateMultiSearch() : new SteamLobbyFinder.LobbyStateSingleSearch());
				}
			}
		}

		// Token: 0x02000C51 RID: 3153
		private class LobbyStateSingleSearch : SteamLobbyFinder.LobbyStateBase
		{
			// Token: 0x06004767 RID: 18279 RVA: 0x00126D08 File Offset: 0x00124F08
			public override void OnEnter()
			{
				base.OnEnter();
				(PlatformSystems.lobbyManager as SteamworksLobbyManager).SetLobbyQuickPlayCutoffTimeIfOwner(null);
			}

			// Token: 0x06004768 RID: 18280 RVA: 0x00126D34 File Offset: 0x00124F34
			public override void Update()
			{
				base.Update();
				if (PlatformSystems.lobbyManager.hasMinimumPlayerCount)
				{
					this.outer.SetNextState(new SteamLobbyFinder.LobbyStateMultiSearch());
					return;
				}
				if (this.lobbyFinder.refreshTimer <= 0f)
				{
					if (base.age >= this.lobbyFinder.joinOnlyDuration && SteamLobbyFinder.steamClient.Lobby.LobbyType != Lobby.Type.Public)
					{
						Debug.LogFormat("Unable to find joinable lobby after {0} seconds. Setting lobby to public.", new object[]
						{
							this.lobbyFinder.age
						});
						SteamLobbyFinder.steamClient.Lobby.LobbyType = Lobby.Type.Public;
					}
					this.lobbyFinder.refreshTimer = this.lobbyFinder.refreshInterval;
					this.lobbyFinder.RequestLobbyListRefresh();
				}
			}

			// Token: 0x06004769 RID: 18281 RVA: 0x00126DF0 File Offset: 0x00124FF0
			public override void OnLobbiesUpdated()
			{
				base.OnLobbiesUpdated();
				if (SteamLobbyFinder.steamClient.IsValid)
				{
					List<LobbyList.Lobby> lobbies = SteamLobbyFinder.steamClient.LobbyList.Lobbies;
					List<LobbyList.Lobby> list = new List<LobbyList.Lobby>();
					ulong currentLobby = SteamLobbyFinder.steamClient.Lobby.CurrentLobby;
					bool isValid = SteamLobbyFinder.steamClient.Lobby.IsValid;
					int currentLobbySize = isValid ? SteamLobbyFinder.GetCurrentLobbyRealPlayerCount() : LocalUserManager.readOnlyLocalUsersList.Count;
					if (PlatformSystems.lobbyManager.ownsLobby || !isValid)
					{
						for (int i = 0; i < lobbies.Count; i++)
						{
							if ((!isValid || lobbies[i].LobbyID < currentLobby) && SteamLobbyFinder.CanJoinLobby(currentLobbySize, lobbies[i]))
							{
								list.Add(lobbies[i]);
							}
						}
						if (list.Count > 0)
						{
							UserID join = new UserID(list[0].LobbyID);
							PlatformSystems.lobbyManager.JoinLobby(join);
						}
					}
					Debug.LogFormat("Found {0} lobbies, {1} joinable.", new object[]
					{
						lobbies.Count,
						list.Count
					});
				}
			}
		}

		// Token: 0x02000C52 RID: 3154
		private class LobbyStateMultiSearch : SteamLobbyFinder.LobbyStateBase
		{
			// Token: 0x0600476B RID: 18283 RVA: 0x00126F08 File Offset: 0x00125108
			public override void OnEnter()
			{
				base.OnEnter();
				SteamLobbyFinder.steamClient.Lobby.LobbyType = Lobby.Type.Public;
				TimeSpan timeSpan = Util.UnixTimeStampToDateTimeUtc(SteamLobbyFinder.steamClient.Utils.GetServerRealTime()) + TimeSpan.FromSeconds((double)this.lobbyFinder.waitForFullDuration) - Util.dateZero;
				(PlatformSystems.lobbyManager as SteamworksLobbyManager).SetLobbyQuickPlayCutoffTimeIfOwner(new uint?((uint)timeSpan.TotalSeconds));
			}

			// Token: 0x0600476C RID: 18284 RVA: 0x00126F7C File Offset: 0x0012517C
			public override void OnExit()
			{
				(PlatformSystems.lobbyManager as SteamworksLobbyManager).SetLobbyQuickPlayCutoffTimeIfOwner(null);
				base.OnExit();
			}

			// Token: 0x0600476D RID: 18285 RVA: 0x00126FA8 File Offset: 0x001251A8
			public override void Update()
			{
				base.Update();
				if (!PlatformSystems.lobbyManager.hasMinimumPlayerCount)
				{
					this.outer.SetNextState(new SteamLobbyFinder.LobbyStateSingleSearch());
					return;
				}
				if (this.lobbyFinder.waitForFullDuration <= base.age)
				{
					this.outer.SetNextState(new SteamLobbyFinder.LobbyStateBeginGame());
				}
			}
		}

		// Token: 0x02000C53 RID: 3155
		private class LobbyStateBeginGame : SteamLobbyFinder.LobbyStateBase
		{
			// Token: 0x0600476F RID: 18287 RVA: 0x00126FFC File Offset: 0x001251FC
			public override void OnEnter()
			{
				base.OnEnter();
				SteamLobbyFinder.steamClient.Lobby.LobbyType = (Lobby.Type)PlatformSystems.lobbyManager.preferredLobbyType;
				(PlatformSystems.lobbyManager as SteamworksLobbyManager).SetStartingIfOwner(true);
				string arg = "ClassicRun";
				Console.instance.SubmitCmd(null, string.Format("transition_command \"gamemode {0}; host 1;\"", arg), false);
			}
		}
	}
}
