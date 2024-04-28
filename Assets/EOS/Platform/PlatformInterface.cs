using System;
using Epic.OnlineServices.Achievements;
using Epic.OnlineServices.AntiCheatClient;
using Epic.OnlineServices.AntiCheatServer;
using Epic.OnlineServices.Auth;
using Epic.OnlineServices.Connect;
using Epic.OnlineServices.CustomInvites;
using Epic.OnlineServices.Ecom;
using Epic.OnlineServices.Friends;
using Epic.OnlineServices.KWS;
using Epic.OnlineServices.Leaderboards;
using Epic.OnlineServices.Lobby;
using Epic.OnlineServices.Metrics;
using Epic.OnlineServices.Mods;
using Epic.OnlineServices.P2P;
using Epic.OnlineServices.PlayerDataStorage;
using Epic.OnlineServices.Presence;
using Epic.OnlineServices.ProgressionSnapshot;
using Epic.OnlineServices.Reports;
using Epic.OnlineServices.RTC;
using Epic.OnlineServices.RTCAdmin;
using Epic.OnlineServices.Sanctions;
using Epic.OnlineServices.Sessions;
using Epic.OnlineServices.Stats;
using Epic.OnlineServices.TitleStorage;
using Epic.OnlineServices.UI;
using Epic.OnlineServices.UserInfo;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005DC RID: 1500
	public sealed class PlatformInterface : Handle
	{
		// Token: 0x0600243B RID: 9275 RVA: 0x00026384 File Offset: 0x00024584
		public static Result Initialize(AndroidInitializeOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<AndroidInitializeOptionsInternal, AndroidInitializeOptions>(ref zero, options);
			Result result = Bindings.EOS_Initialize(zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000036D3 File Offset: 0x000018D3
		public PlatformInterface()
		{
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000036DB File Offset: 0x000018DB
		public PlatformInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000263AE File Offset: 0x000245AE
		public Result CheckForLauncherAndRestart()
		{
			return Bindings.EOS_Platform_CheckForLauncherAndRestart(base.InnerHandle);
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000263BC File Offset: 0x000245BC
		public static PlatformInterface Create(Options options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<OptionsInternal, Options>(ref zero, options);
			IntPtr source = Bindings.EOS_Platform_Create(zero);
			Helper.TryMarshalDispose(ref zero);
			PlatformInterface result;
			Helper.TryMarshalGet<PlatformInterface>(source, out result);
			return result;
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000263F0 File Offset: 0x000245F0
		public AchievementsInterface GetAchievementsInterface()
		{
			AchievementsInterface result;
			Helper.TryMarshalGet<AchievementsInterface>(Bindings.EOS_Platform_GetAchievementsInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x00026414 File Offset: 0x00024614
		public Result GetActiveCountryCode(EpicAccountId localUserId, out string outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet(ref zero, localUserId);
			IntPtr zero2 = IntPtr.Zero;
			int size = 5;
			Helper.TryMarshalAllocate(ref zero2, size);
			Result result = Bindings.EOS_Platform_GetActiveCountryCode(base.InnerHandle, zero, zero2, ref size);
			Helper.TryMarshalGet(zero2, out outBuffer);
			Helper.TryMarshalDispose(ref zero2);
			return result;
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x00026460 File Offset: 0x00024660
		public Result GetActiveLocaleCode(EpicAccountId localUserId, out string outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet(ref zero, localUserId);
			IntPtr zero2 = IntPtr.Zero;
			int size = 10;
			Helper.TryMarshalAllocate(ref zero2, size);
			Result result = Bindings.EOS_Platform_GetActiveLocaleCode(base.InnerHandle, zero, zero2, ref size);
			Helper.TryMarshalGet(zero2, out outBuffer);
			Helper.TryMarshalDispose(ref zero2);
			return result;
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000264B0 File Offset: 0x000246B0
		public AntiCheatClientInterface GetAntiCheatClientInterface()
		{
			AntiCheatClientInterface result;
			Helper.TryMarshalGet<AntiCheatClientInterface>(Bindings.EOS_Platform_GetAntiCheatClientInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000264D4 File Offset: 0x000246D4
		public AntiCheatServerInterface GetAntiCheatServerInterface()
		{
			AntiCheatServerInterface result;
			Helper.TryMarshalGet<AntiCheatServerInterface>(Bindings.EOS_Platform_GetAntiCheatServerInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000264F8 File Offset: 0x000246F8
		public AuthInterface GetAuthInterface()
		{
			AuthInterface result;
			Helper.TryMarshalGet<AuthInterface>(Bindings.EOS_Platform_GetAuthInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x0002651C File Offset: 0x0002471C
		public ConnectInterface GetConnectInterface()
		{
			ConnectInterface result;
			Helper.TryMarshalGet<ConnectInterface>(Bindings.EOS_Platform_GetConnectInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x00026540 File Offset: 0x00024740
		public CustomInvitesInterface GetCustomInvitesInterface()
		{
			CustomInvitesInterface result;
			Helper.TryMarshalGet<CustomInvitesInterface>(Bindings.EOS_Platform_GetCustomInvitesInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x00026564 File Offset: 0x00024764
		public EcomInterface GetEcomInterface()
		{
			EcomInterface result;
			Helper.TryMarshalGet<EcomInterface>(Bindings.EOS_Platform_GetEcomInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x00026588 File Offset: 0x00024788
		public FriendsInterface GetFriendsInterface()
		{
			FriendsInterface result;
			Helper.TryMarshalGet<FriendsInterface>(Bindings.EOS_Platform_GetFriendsInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000265AC File Offset: 0x000247AC
		public KWSInterface GetKWSInterface()
		{
			KWSInterface result;
			Helper.TryMarshalGet<KWSInterface>(Bindings.EOS_Platform_GetKWSInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000265D0 File Offset: 0x000247D0
		public LeaderboardsInterface GetLeaderboardsInterface()
		{
			LeaderboardsInterface result;
			Helper.TryMarshalGet<LeaderboardsInterface>(Bindings.EOS_Platform_GetLeaderboardsInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000265F4 File Offset: 0x000247F4
		public LobbyInterface GetLobbyInterface()
		{
			LobbyInterface result;
			Helper.TryMarshalGet<LobbyInterface>(Bindings.EOS_Platform_GetLobbyInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x00026618 File Offset: 0x00024818
		public MetricsInterface GetMetricsInterface()
		{
			MetricsInterface result;
			Helper.TryMarshalGet<MetricsInterface>(Bindings.EOS_Platform_GetMetricsInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x0002663C File Offset: 0x0002483C
		public ModsInterface GetModsInterface()
		{
			ModsInterface result;
			Helper.TryMarshalGet<ModsInterface>(Bindings.EOS_Platform_GetModsInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x00026660 File Offset: 0x00024860
		public Result GetOverrideCountryCode(out string outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			int size = 5;
			Helper.TryMarshalAllocate(ref zero, size);
			Result result = Bindings.EOS_Platform_GetOverrideCountryCode(base.InnerHandle, zero, ref size);
			Helper.TryMarshalGet(zero, out outBuffer);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x0002669C File Offset: 0x0002489C
		public Result GetOverrideLocaleCode(out string outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			int size = 10;
			Helper.TryMarshalAllocate(ref zero, size);
			Result result = Bindings.EOS_Platform_GetOverrideLocaleCode(base.InnerHandle, zero, ref size);
			Helper.TryMarshalGet(zero, out outBuffer);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x000266DC File Offset: 0x000248DC
		public P2PInterface GetP2PInterface()
		{
			P2PInterface result;
			Helper.TryMarshalGet<P2PInterface>(Bindings.EOS_Platform_GetP2PInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x00026700 File Offset: 0x00024900
		public PlayerDataStorageInterface GetPlayerDataStorageInterface()
		{
			PlayerDataStorageInterface result;
			Helper.TryMarshalGet<PlayerDataStorageInterface>(Bindings.EOS_Platform_GetPlayerDataStorageInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x00026724 File Offset: 0x00024924
		public PresenceInterface GetPresenceInterface()
		{
			PresenceInterface result;
			Helper.TryMarshalGet<PresenceInterface>(Bindings.EOS_Platform_GetPresenceInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x00026748 File Offset: 0x00024948
		public ProgressionSnapshotInterface GetProgressionSnapshotInterface()
		{
			ProgressionSnapshotInterface result;
			Helper.TryMarshalGet<ProgressionSnapshotInterface>(Bindings.EOS_Platform_GetProgressionSnapshotInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x0002676C File Offset: 0x0002496C
		public RTCAdminInterface GetRTCAdminInterface()
		{
			RTCAdminInterface result;
			Helper.TryMarshalGet<RTCAdminInterface>(Bindings.EOS_Platform_GetRTCAdminInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x00026790 File Offset: 0x00024990
		public RTCInterface GetRTCInterface()
		{
			RTCInterface result;
			Helper.TryMarshalGet<RTCInterface>(Bindings.EOS_Platform_GetRTCInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000267B4 File Offset: 0x000249B4
		public ReportsInterface GetReportsInterface()
		{
			ReportsInterface result;
			Helper.TryMarshalGet<ReportsInterface>(Bindings.EOS_Platform_GetReportsInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000267D8 File Offset: 0x000249D8
		public SanctionsInterface GetSanctionsInterface()
		{
			SanctionsInterface result;
			Helper.TryMarshalGet<SanctionsInterface>(Bindings.EOS_Platform_GetSanctionsInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000267FC File Offset: 0x000249FC
		public SessionsInterface GetSessionsInterface()
		{
			SessionsInterface result;
			Helper.TryMarshalGet<SessionsInterface>(Bindings.EOS_Platform_GetSessionsInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x00026820 File Offset: 0x00024A20
		public StatsInterface GetStatsInterface()
		{
			StatsInterface result;
			Helper.TryMarshalGet<StatsInterface>(Bindings.EOS_Platform_GetStatsInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00026844 File Offset: 0x00024A44
		public TitleStorageInterface GetTitleStorageInterface()
		{
			TitleStorageInterface result;
			Helper.TryMarshalGet<TitleStorageInterface>(Bindings.EOS_Platform_GetTitleStorageInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x00026868 File Offset: 0x00024A68
		public UIInterface GetUIInterface()
		{
			UIInterface result;
			Helper.TryMarshalGet<UIInterface>(Bindings.EOS_Platform_GetUIInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x0002688C File Offset: 0x00024A8C
		public UserInfoInterface GetUserInfoInterface()
		{
			UserInfoInterface result;
			Helper.TryMarshalGet<UserInfoInterface>(Bindings.EOS_Platform_GetUserInfoInterface(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000268B0 File Offset: 0x00024AB0
		public static Result Initialize(InitializeOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<InitializeOptionsInternal, InitializeOptions>(ref zero, options);
			Result result = Bindings.EOS_Initialize(zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000268DA File Offset: 0x00024ADA
		public void Release()
		{
			Bindings.EOS_Platform_Release(base.InnerHandle);
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000268E8 File Offset: 0x00024AE8
		public Result SetOverrideCountryCode(string newCountryCode)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet(ref zero, newCountryCode);
			Result result = Bindings.EOS_Platform_SetOverrideCountryCode(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x00026918 File Offset: 0x00024B18
		public Result SetOverrideLocaleCode(string newLocaleCode)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet(ref zero, newLocaleCode);
			Result result = Bindings.EOS_Platform_SetOverrideLocaleCode(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x00026948 File Offset: 0x00024B48
		public static Result Shutdown()
		{
			return Bindings.EOS_Shutdown();
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x0002694F File Offset: 0x00024B4F
		public void Tick()
		{
			Bindings.EOS_Platform_Tick(base.InnerHandle);
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x0002695C File Offset: 0x00024B5C
		public static PlatformInterface Create(WindowsOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<WindowsOptionsInternal, WindowsOptions>(ref zero, options);
			IntPtr source = Bindings.EOS_Platform_Create(zero);
			Helper.TryMarshalDispose(ref zero);
			PlatformInterface result;
			Helper.TryMarshalGet<PlatformInterface>(source, out result);
			return result;
		}

		// Token: 0x04001123 RID: 4387
		public const int AndroidinitializeoptionssysteminitializeoptionsApiLatest = 2;

		// Token: 0x04001124 RID: 4388
		public const int CountrycodeMaxBufferLen = 5;

		// Token: 0x04001125 RID: 4389
		public const int CountrycodeMaxLength = 4;

		// Token: 0x04001126 RID: 4390
		public const int InitializeApiLatest = 4;

		// Token: 0x04001127 RID: 4391
		public const int InitializeThreadaffinityApiLatest = 1;

		// Token: 0x04001128 RID: 4392
		public const int LocalecodeMaxBufferLen = 10;

		// Token: 0x04001129 RID: 4393
		public const int LocalecodeMaxLength = 9;

		// Token: 0x0400112A RID: 4394
		public const int OptionsApiLatest = 11;

		// Token: 0x0400112B RID: 4395
		public const int RtcoptionsApiLatest = 1;

		// Token: 0x0400112C RID: 4396
		public const int PlatformWindowsrtcoptionsplatformspecificoptionsApiLatest = 1;
	}
}
