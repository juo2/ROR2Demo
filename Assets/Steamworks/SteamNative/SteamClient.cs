using System;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200005B RID: 91
	internal class SteamClient : IDisposable
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002AB0 File Offset: 0x00000CB0
		internal SteamClient(BaseSteamworks steamworks, IntPtr pointer)
		{
			this.steamworks = steamworks;
			if (Platform.IsWindows64)
			{
				this.platform = new Platform.Win64(pointer);
				return;
			}
			if (Platform.IsWindows32)
			{
				this.platform = new Platform.Win32(pointer);
				return;
			}
			if (Platform.IsLinux32)
			{
				this.platform = new Platform.Linux32(pointer);
				return;
			}
			if (Platform.IsLinux64)
			{
				this.platform = new Platform.Linux64(pointer);
				return;
			}
			if (Platform.IsOsx)
			{
				this.platform = new Platform.Mac(pointer);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002B2D File Offset: 0x00000D2D
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B44 File Offset: 0x00000D44
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B60 File Offset: 0x00000D60
		public bool BReleaseSteamPipe(HSteamPipe hSteamPipe)
		{
			return this.platform.ISteamClient_BReleaseSteamPipe(hSteamPipe.Value);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B73 File Offset: 0x00000D73
		public bool BShutdownIfAllPipesClosed()
		{
			return this.platform.ISteamClient_BShutdownIfAllPipesClosed();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B80 File Offset: 0x00000D80
		public HSteamUser ConnectToGlobalUser(HSteamPipe hSteamPipe)
		{
			return this.platform.ISteamClient_ConnectToGlobalUser(hSteamPipe.Value);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002B93 File Offset: 0x00000D93
		public HSteamUser CreateLocalUser(out HSteamPipe phSteamPipe, AccountType eAccountType)
		{
			return this.platform.ISteamClient_CreateLocalUser(out phSteamPipe.Value, eAccountType);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002BA7 File Offset: 0x00000DA7
		public HSteamPipe CreateSteamPipe()
		{
			return this.platform.ISteamClient_CreateSteamPipe();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public uint GetIPCCallCount()
		{
			return this.platform.ISteamClient_GetIPCCallCount();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public SteamAppList GetISteamAppList(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamAppList(hSteamUser.Value, hSteamPipe.Value, pchVersion);
			return new SteamAppList(this.steamworks, pointer);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public SteamApps GetISteamApps(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamApps(hSteamUser.Value, hSteamPipe.Value, pchVersion);
			return new SteamApps(this.steamworks, pointer);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002C2C File Offset: 0x00000E2C
		public SteamController GetISteamController(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamController(hSteamUser.Value, hSteamPipe.Value, pchVersion);
			return new SteamController(this.steamworks, pointer);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002C60 File Offset: 0x00000E60
		public SteamFriends GetISteamFriends(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamFriends(hSteamUser.Value, hSteamPipe.Value, pchVersion);
			return new SteamFriends(this.steamworks, pointer);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002C94 File Offset: 0x00000E94
		public SteamGameServer GetISteamGameServer(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamGameServer(hSteamUser.Value, hSteamPipe.Value, pchVersion);
			return new SteamGameServer(this.steamworks, pointer);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public SteamGameServerStats GetISteamGameServerStats(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamGameServerStats(hSteamuser.Value, hSteamPipe.Value, pchVersion);
			return new SteamGameServerStats(this.steamworks, pointer);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002CFA File Offset: 0x00000EFA
		public IntPtr GetISteamGenericInterface(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion)
		{
			return this.platform.ISteamClient_GetISteamGenericInterface(hSteamUser.Value, hSteamPipe.Value, pchVersion);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002D14 File Offset: 0x00000F14
		public SteamHTMLSurface GetISteamHTMLSurface(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamHTMLSurface(hSteamuser.Value, hSteamPipe.Value, pchVersion);
			return new SteamHTMLSurface(this.steamworks, pointer);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002D48 File Offset: 0x00000F48
		public SteamHTTP GetISteamHTTP(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamHTTP(hSteamuser.Value, hSteamPipe.Value, pchVersion);
			return new SteamHTTP(this.steamworks, pointer);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002D7C File Offset: 0x00000F7C
		public SteamInventory GetISteamInventory(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamInventory(hSteamuser.Value, hSteamPipe.Value, pchVersion);
			return new SteamInventory(this.steamworks, pointer);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public SteamMatchmaking GetISteamMatchmaking(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamMatchmaking(hSteamUser.Value, hSteamPipe.Value, pchVersion);
			return new SteamMatchmaking(this.steamworks, pointer);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public SteamMatchmakingServers GetISteamMatchmakingServers(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamMatchmakingServers(hSteamUser.Value, hSteamPipe.Value, pchVersion);
			return new SteamMatchmakingServers(this.steamworks, pointer);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002E18 File Offset: 0x00001018
		public SteamMusic GetISteamMusic(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamMusic(hSteamuser.Value, hSteamPipe.Value, pchVersion);
			return new SteamMusic(this.steamworks, pointer);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002E4C File Offset: 0x0000104C
		public SteamMusicRemote GetISteamMusicRemote(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamMusicRemote(hSteamuser.Value, hSteamPipe.Value, pchVersion);
			return new SteamMusicRemote(this.steamworks, pointer);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002E80 File Offset: 0x00001080
		public SteamNetworking GetISteamNetworking(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamNetworking(hSteamUser.Value, hSteamPipe.Value, pchVersion);
			return new SteamNetworking(this.steamworks, pointer);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002EB4 File Offset: 0x000010B4
		public SteamParentalSettings GetISteamParentalSettings(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamParentalSettings(hSteamuser.Value, hSteamPipe.Value, pchVersion);
			return new SteamParentalSettings(this.steamworks, pointer);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002EE8 File Offset: 0x000010E8
		public SteamRemoteStorage GetISteamRemoteStorage(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamRemoteStorage(hSteamuser.Value, hSteamPipe.Value, pchVersion);
			return new SteamRemoteStorage(this.steamworks, pointer);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002F1C File Offset: 0x0000111C
		public SteamScreenshots GetISteamScreenshots(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamScreenshots(hSteamuser.Value, hSteamPipe.Value, pchVersion);
			return new SteamScreenshots(this.steamworks, pointer);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002F50 File Offset: 0x00001150
		public SteamUGC GetISteamUGC(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamUGC(hSteamUser.Value, hSteamPipe.Value, pchVersion);
			return new SteamUGC(this.steamworks, pointer);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002F84 File Offset: 0x00001184
		public SteamUser GetISteamUser(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamUser(hSteamUser.Value, hSteamPipe.Value, pchVersion);
			return new SteamUser(this.steamworks, pointer);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002FB8 File Offset: 0x000011B8
		public SteamUserStats GetISteamUserStats(HSteamUser hSteamUser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamUserStats(hSteamUser.Value, hSteamPipe.Value, pchVersion);
			return new SteamUserStats(this.steamworks, pointer);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002FEC File Offset: 0x000011EC
		public SteamUtils GetISteamUtils(HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamUtils(hSteamPipe.Value, pchVersion);
			return new SteamUtils(this.steamworks, pointer);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003018 File Offset: 0x00001218
		public SteamVideo GetISteamVideo(HSteamUser hSteamuser, HSteamPipe hSteamPipe, string pchVersion)
		{
			IntPtr pointer = this.platform.ISteamClient_GetISteamVideo(hSteamuser.Value, hSteamPipe.Value, pchVersion);
			return new SteamVideo(this.steamworks, pointer);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000304A File Offset: 0x0000124A
		public void ReleaseUser(HSteamPipe hSteamPipe, HSteamUser hUser)
		{
			this.platform.ISteamClient_ReleaseUser(hSteamPipe.Value, hUser.Value);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003063 File Offset: 0x00001263
		public void SetLocalIPBinding(uint unIP, ushort usPort)
		{
			this.platform.ISteamClient_SetLocalIPBinding(unIP, usPort);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003072 File Offset: 0x00001272
		public void SetWarningMessageHook(IntPtr pFunction)
		{
			this.platform.ISteamClient_SetWarningMessageHook(pFunction);
		}

		// Token: 0x04000466 RID: 1126
		internal Platform.Interface platform;

		// Token: 0x04000467 RID: 1127
		internal BaseSteamworks steamworks;
	}
}
