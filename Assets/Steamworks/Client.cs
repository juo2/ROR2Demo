using System;
using System.IO;
using Facepunch.Steamworks.Interop;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x0200015F RID: 351
	public class Client : BaseSteamworks, IDisposable
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00033A23 File Offset: 0x00031C23
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x00033A2A File Offset: 0x00031C2A
		public static Client Instance { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00033A32 File Offset: 0x00031C32
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x00033A3A File Offset: 0x00031C3A
		public string Username { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00033A43 File Offset: 0x00031C43
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x00033A4B File Offset: 0x00031C4B
		public ulong SteamId { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00033A54 File Offset: 0x00031C54
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x00033A5C File Offset: 0x00031C5C
		public ulong OwnerSteamId { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00033A65 File Offset: 0x00031C65
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x00033A6D File Offset: 0x00031C6D
		public string BetaName { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00033A76 File Offset: 0x00031C76
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x00033A7E File Offset: 0x00031C7E
		public int BuildId { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x00033A87 File Offset: 0x00031C87
		// (set) Token: 0x06000A1A RID: 2586 RVA: 0x00033A8F File Offset: 0x00031C8F
		public DirectoryInfo InstallFolder { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00033A98 File Offset: 0x00031C98
		public string CurrentLanguage { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x00033AA0 File Offset: 0x00031CA0
		public string[] AvailableLanguages { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00033AA8 File Offset: 0x00031CA8
		// (set) Token: 0x06000A1E RID: 2590 RVA: 0x00033AB0 File Offset: 0x00031CB0
		public Voice Voice { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00033AB9 File Offset: 0x00031CB9
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x00033AC1 File Offset: 0x00031CC1
		public ServerList ServerList { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00033ACA File Offset: 0x00031CCA
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x00033AD2 File Offset: 0x00031CD2
		public LobbyList LobbyList { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x00033ADB File Offset: 0x00031CDB
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x00033AE3 File Offset: 0x00031CE3
		public App App { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00033AEC File Offset: 0x00031CEC
		// (set) Token: 0x06000A26 RID: 2598 RVA: 0x00033AF4 File Offset: 0x00031CF4
		public Achievements Achievements { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x00033AFD File Offset: 0x00031CFD
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x00033B05 File Offset: 0x00031D05
		public Stats Stats { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00033B0E File Offset: 0x00031D0E
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x00033B16 File Offset: 0x00031D16
		public MicroTransactions MicroTransactions { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x00033B1F File Offset: 0x00031D1F
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x00033B27 File Offset: 0x00031D27
		public User User { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00033B30 File Offset: 0x00031D30
		// (set) Token: 0x06000A2E RID: 2606 RVA: 0x00033B38 File Offset: 0x00031D38
		public RemoteStorage RemoteStorage { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00033B41 File Offset: 0x00031D41
		// (set) Token: 0x06000A30 RID: 2608 RVA: 0x00033B49 File Offset: 0x00031D49
		public Overlay Overlay { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00033B52 File Offset: 0x00031D52
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x00033B5A File Offset: 0x00031D5A
		public Utils Utils { get; private set; }

		// Token: 0x06000A33 RID: 2611 RVA: 0x00033B64 File Offset: 0x00031D64
		public Client(uint appId) : base(appId)
		{
			if (Client.Instance != null)
			{
				throw new Exception("Only one Facepunch.Steamworks.Client can exist - dispose the old one before trying to create a new one.");
			}
			Client.Instance = this;
			this.native = new NativeInterface();
			if (!this.native.InitClient(this))
			{
				this.native.Dispose();
				this.native = null;
				Client.Instance = null;
				return;
			}
			Callbacks.RegisterCallbacks(this);
			base.SetupCommonInterfaces();
			this.Voice = new Voice(this);
			this.ServerList = new ServerList(this);
			this.LobbyList = new LobbyList(this);
			this.App = new App(this);
			this.Stats = new Stats(this);
			this.Achievements = new Achievements(this);
			this.MicroTransactions = new MicroTransactions(this);
			this.User = new User(this);
			this.RemoteStorage = new RemoteStorage(this);
			this.Overlay = new Overlay(this);
			this.Utils = new Utils(this);
			base.Workshop.friends = this.Friends;
			this.Stats.UpdateStats();
			base.AppId = appId;
			this.Username = this.native.friends.GetPersonaName();
			this.SteamId = this.native.user.GetSteamID();
			this.BetaName = this.native.apps.GetCurrentBetaName();
			this.OwnerSteamId = this.native.apps.GetAppOwner();
			string appInstallDir = this.native.apps.GetAppInstallDir(base.AppId);
			if (!string.IsNullOrEmpty(appInstallDir) && Directory.Exists(appInstallDir))
			{
				this.InstallFolder = new DirectoryInfo(appInstallDir);
			}
			this.BuildId = this.native.apps.GetAppBuildId();
			this.CurrentLanguage = this.native.apps.GetCurrentGameLanguage();
			this.AvailableLanguages = this.native.apps.GetAvailableGameLanguages().Split(new char[]
			{
				';'
			}, StringSplitOptions.RemoveEmptyEntries);
			this.Update();
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00033D60 File Offset: 0x00031F60
		~Client()
		{
			this.Dispose();
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00033D8C File Offset: 0x00031F8C
		public override void Update()
		{
			if (!base.IsValid)
			{
				return;
			}
			this.RunCallbacks();
			this.Voice.Update();
			this.Friends.Cycle();
			base.Update();
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00033DB9 File Offset: 0x00031FB9
		public void RunCallbacks()
		{
			this.native.api.SteamAPI_RunCallbacks();
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00033DCC File Offset: 0x00031FCC
		public override void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.Voice != null)
			{
				this.Voice = null;
			}
			if (this.ServerList != null)
			{
				this.ServerList.Dispose();
				this.ServerList = null;
			}
			if (this.LobbyList != null)
			{
				this.LobbyList.Dispose();
				this.LobbyList = null;
			}
			if (this.App != null)
			{
				this.App.Dispose();
				this.App = null;
			}
			if (this.Stats != null)
			{
				this.Stats.Dispose();
				this.Stats = null;
			}
			if (this.Achievements != null)
			{
				this.Achievements.Dispose();
				this.Achievements = null;
			}
			if (this.MicroTransactions != null)
			{
				this.MicroTransactions.Dispose();
				this.MicroTransactions = null;
			}
			if (this.User != null)
			{
				this.User.Dispose();
				this.User = null;
			}
			if (this.RemoteStorage != null)
			{
				this.RemoteStorage.Dispose();
				this.RemoteStorage = null;
			}
			if (this.Utils != null)
			{
				this.Utils.Dispose();
				this.Utils = null;
			}
			if (Client.Instance == this)
			{
				Client.Instance = null;
			}
			base.Dispose();
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00033EF0 File Offset: 0x000320F0
		public Leaderboard GetLeaderboard(string name, Client.LeaderboardSortMethod sortMethod = Client.LeaderboardSortMethod.None, Client.LeaderboardDisplayType displayType = Client.LeaderboardDisplayType.None)
		{
			Leaderboard leaderboard = new Leaderboard(this);
			this.native.userstats.FindOrCreateLeaderboard(name, (SteamNative.LeaderboardSortMethod)sortMethod, (SteamNative.LeaderboardDisplayType)displayType, new Action<LeaderboardFindResult_t, bool>(leaderboard.OnBoardCreated));
			return leaderboard;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00033F25 File Offset: 0x00032125
		public bool IsLoggedOn
		{
			get
			{
				return this.native.user.BLoggedOn();
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00033F37 File Offset: 0x00032137
		public bool IsSubscribed
		{
			get
			{
				return this.native.apps.BIsSubscribed();
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00033F49 File Offset: 0x00032149
		public bool IsCybercafe
		{
			get
			{
				return this.native.apps.BIsCybercafe();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00033F5B File Offset: 0x0003215B
		public bool IsSubscribedFromFreeWeekend
		{
			get
			{
				return this.native.apps.BIsSubscribedFromFreeWeekend();
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00033F6D File Offset: 0x0003216D
		public bool IsLowViolence
		{
			get
			{
				return this.native.apps.BIsLowViolence();
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00033F80 File Offset: 0x00032180
		public static bool RestartIfNecessary(uint appid)
		{
			bool result;
			using (SteamApi steamApi = new SteamApi())
			{
				result = steamApi.SteamAPI_RestartAppIfNecessary(appid);
			}
			return result;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00033FB8 File Offset: 0x000321B8
		public Auth Auth
		{
			get
			{
				if (this._auth == null)
				{
					this._auth = new Auth
					{
						client = this
					};
				}
				return this._auth;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00033FDA File Offset: 0x000321DA
		public Friends Friends
		{
			get
			{
				if (this._friends == null)
				{
					this._friends = new Friends(this);
				}
				return this._friends;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00033FF6 File Offset: 0x000321F6
		public Lobby Lobby
		{
			get
			{
				if (this._lobby == null)
				{
					this._lobby = new Lobby(this);
				}
				return this._lobby;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00034012 File Offset: 0x00032212
		public Screenshots Screenshots
		{
			get
			{
				if (this._screenshots == null)
				{
					this._screenshots = new Screenshots(this);
				}
				return this._screenshots;
			}
		}

		// Token: 0x040007CF RID: 1999
		private Auth _auth;

		// Token: 0x040007D0 RID: 2000
		private Friends _friends;

		// Token: 0x040007D1 RID: 2001
		private Lobby _lobby;

		// Token: 0x040007D2 RID: 2002
		private Screenshots _screenshots;

		// Token: 0x02000253 RID: 595
		public enum LeaderboardSortMethod
		{
			// Token: 0x04000B84 RID: 2948
			None,
			// Token: 0x04000B85 RID: 2949
			Ascending,
			// Token: 0x04000B86 RID: 2950
			Descending
		}

		// Token: 0x02000254 RID: 596
		public enum LeaderboardDisplayType
		{
			// Token: 0x04000B88 RID: 2952
			None,
			// Token: 0x04000B89 RID: 2953
			Numeric,
			// Token: 0x04000B8A RID: 2954
			TimeSeconds,
			// Token: 0x04000B8B RID: 2955
			TimeMilliSeconds
		}
	}
}
