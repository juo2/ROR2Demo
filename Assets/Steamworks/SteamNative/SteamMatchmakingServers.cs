using System;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000064 RID: 100
	internal class SteamMatchmakingServers : IDisposable
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x00005344 File Offset: 0x00003544
		internal SteamMatchmakingServers(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000053C1 File Offset: 0x000035C1
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000053D8 File Offset: 0x000035D8
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000053F4 File Offset: 0x000035F4
		public void CancelQuery(HServerListRequest hRequest)
		{
			this.platform.ISteamMatchmakingServers_CancelQuery(hRequest.Value);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00005407 File Offset: 0x00003607
		public void CancelServerQuery(HServerQuery hServerQuery)
		{
			this.platform.ISteamMatchmakingServers_CancelServerQuery(hServerQuery.Value);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000541A File Offset: 0x0000361A
		public int GetServerCount(HServerListRequest hRequest)
		{
			return this.platform.ISteamMatchmakingServers_GetServerCount(hRequest.Value);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00005430 File Offset: 0x00003630
		public gameserveritem_t GetServerDetails(HServerListRequest hRequest, int iServer)
		{
			IntPtr intPtr = this.platform.ISteamMatchmakingServers_GetServerDetails(hRequest.Value, iServer);
			if (intPtr == IntPtr.Zero)
			{
				return default(gameserveritem_t);
			}
			return gameserveritem_t.FromPointer(intPtr);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000546D File Offset: 0x0000366D
		public bool IsRefreshing(HServerListRequest hRequest)
		{
			return this.platform.ISteamMatchmakingServers_IsRefreshing(hRequest.Value);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00005480 File Offset: 0x00003680
		public HServerQuery PingServer(uint unIP, ushort usPort, IntPtr pRequestServersResponse)
		{
			return this.platform.ISteamMatchmakingServers_PingServer(unIP, usPort, pRequestServersResponse);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005490 File Offset: 0x00003690
		public HServerQuery PlayerDetails(uint unIP, ushort usPort, IntPtr pRequestServersResponse)
		{
			return this.platform.ISteamMatchmakingServers_PlayerDetails(unIP, usPort, pRequestServersResponse);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000054A0 File Offset: 0x000036A0
		public void RefreshQuery(HServerListRequest hRequest)
		{
			this.platform.ISteamMatchmakingServers_RefreshQuery(hRequest.Value);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000054B3 File Offset: 0x000036B3
		public void RefreshServer(HServerListRequest hRequest, int iServer)
		{
			this.platform.ISteamMatchmakingServers_RefreshServer(hRequest.Value, iServer);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000054C7 File Offset: 0x000036C7
		public void ReleaseRequest(HServerListRequest hServerListRequest)
		{
			this.platform.ISteamMatchmakingServers_ReleaseRequest(hServerListRequest.Value);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000054DA File Offset: 0x000036DA
		public HServerListRequest RequestFavoritesServerList(AppId_t iApp, IntPtr ppchFilters, uint nFilters, IntPtr pRequestServersResponse)
		{
			return this.platform.ISteamMatchmakingServers_RequestFavoritesServerList(iApp.Value, ppchFilters, nFilters, pRequestServersResponse);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000054F1 File Offset: 0x000036F1
		public HServerListRequest RequestFriendsServerList(AppId_t iApp, IntPtr ppchFilters, uint nFilters, IntPtr pRequestServersResponse)
		{
			return this.platform.ISteamMatchmakingServers_RequestFriendsServerList(iApp.Value, ppchFilters, nFilters, pRequestServersResponse);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005508 File Offset: 0x00003708
		public HServerListRequest RequestHistoryServerList(AppId_t iApp, IntPtr ppchFilters, uint nFilters, IntPtr pRequestServersResponse)
		{
			return this.platform.ISteamMatchmakingServers_RequestHistoryServerList(iApp.Value, ppchFilters, nFilters, pRequestServersResponse);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000551F File Offset: 0x0000371F
		public HServerListRequest RequestInternetServerList(AppId_t iApp, IntPtr ppchFilters, uint nFilters, IntPtr pRequestServersResponse)
		{
			return this.platform.ISteamMatchmakingServers_RequestInternetServerList(iApp.Value, ppchFilters, nFilters, pRequestServersResponse);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005536 File Offset: 0x00003736
		public HServerListRequest RequestLANServerList(AppId_t iApp, IntPtr pRequestServersResponse)
		{
			return this.platform.ISteamMatchmakingServers_RequestLANServerList(iApp.Value, pRequestServersResponse);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000554A File Offset: 0x0000374A
		public HServerListRequest RequestSpectatorServerList(AppId_t iApp, IntPtr ppchFilters, uint nFilters, IntPtr pRequestServersResponse)
		{
			return this.platform.ISteamMatchmakingServers_RequestSpectatorServerList(iApp.Value, ppchFilters, nFilters, pRequestServersResponse);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005561 File Offset: 0x00003761
		public HServerQuery ServerRules(uint unIP, ushort usPort, IntPtr pRequestServersResponse)
		{
			return this.platform.ISteamMatchmakingServers_ServerRules(unIP, usPort, pRequestServersResponse);
		}

		// Token: 0x04000478 RID: 1144
		internal Platform.Interface platform;

		// Token: 0x04000479 RID: 1145
		internal BaseSteamworks steamworks;
	}
}
