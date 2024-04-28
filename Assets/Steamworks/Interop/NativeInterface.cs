using System;
using SteamNative;

namespace Facepunch.Steamworks.Interop
{
	// Token: 0x02000183 RID: 387
	internal class NativeInterface : IDisposable
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x0003B2A0 File Offset: 0x000394A0
		internal bool InitClient(BaseSteamworks steamworks)
		{
			if (Server.Instance != null)
			{
				throw new Exception("Steam client should be initialized before steam server - or there's big trouble.");
			}
			this.isServer = false;
			this.api = new SteamApi();
			if (!this.api.SteamAPI_Init())
			{
				Console.Error.WriteLine("InitClient: SteamAPI_Init returned false");
				return false;
			}
			HSteamUser value = this.api.SteamAPI_GetHSteamUser();
			HSteamPipe value2 = this.api.SteamAPI_GetHSteamPipe();
			if (value2 == 0)
			{
				Console.Error.WriteLine("InitClient: hPipe == 0");
				return false;
			}
			this.FillInterfaces(steamworks, value, value2);
			if (!this.user.IsValid)
			{
				Console.Error.WriteLine("InitClient: ISteamUser is null");
				return false;
			}
			return true;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0003B354 File Offset: 0x00039554
		internal bool InitServer(BaseSteamworks steamworks, uint IpAddress, ushort usPort, ushort GamePort, ushort QueryPort, int eServerMode, string pchVersionString)
		{
			this.isServer = true;
			this.api = new SteamApi();
			if (!this.api.SteamInternal_GameServer_Init(IpAddress, usPort, GamePort, QueryPort, eServerMode, pchVersionString))
			{
				Console.Error.WriteLine("InitServer: GameServer_Init returned false");
				return false;
			}
			HSteamUser value = this.api.SteamGameServer_GetHSteamUser();
			HSteamPipe value2 = this.api.SteamGameServer_GetHSteamPipe();
			if (value2 == 0)
			{
				Console.Error.WriteLine("InitServer: hPipe == 0");
				return false;
			}
			this.FillInterfaces(steamworks, value2, value);
			if (!this.gameServer.IsValid)
			{
				this.gameServer = null;
				throw new Exception("Steam Server: Couldn't load SteamGameServer012");
			}
			return true;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0003B400 File Offset: 0x00039600
		public void FillInterfaces(BaseSteamworks steamworks, int hpipe, int huser)
		{
			IntPtr intPtr = this.api.SteamInternal_CreateInterface("SteamClient017");
			if (intPtr == IntPtr.Zero)
			{
				throw new Exception("Steam Server: Couldn't load SteamClient017");
			}
			this.client = new SteamClient(steamworks, intPtr);
			this.user = this.client.GetISteamUser(huser, hpipe, "SteamUser019");
			this.utils = this.client.GetISteamUtils(hpipe, "SteamUtils009");
			this.networking = this.client.GetISteamNetworking(huser, hpipe, "SteamNetworking005");
			this.gameServerStats = this.client.GetISteamGameServerStats(huser, hpipe, "SteamGameServerStats001");
			this.http = this.client.GetISteamHTTP(huser, hpipe, "STEAMHTTP_INTERFACE_VERSION002");
			this.inventory = this.client.GetISteamInventory(huser, hpipe, "STEAMINVENTORY_INTERFACE_V002");
			this.ugc = this.client.GetISteamUGC(huser, hpipe, "STEAMUGC_INTERFACE_VERSION010");
			this.apps = this.client.GetISteamApps(huser, hpipe, "STEAMAPPS_INTERFACE_VERSION008");
			this.gameServer = this.client.GetISteamGameServer(huser, hpipe, "SteamGameServer012");
			this.friends = this.client.GetISteamFriends(huser, hpipe, "SteamFriends015");
			this.servers = this.client.GetISteamMatchmakingServers(huser, hpipe, "SteamMatchMakingServers002");
			this.userstats = this.client.GetISteamUserStats(huser, hpipe, "STEAMUSERSTATS_INTERFACE_VERSION011");
			this.screenshots = this.client.GetISteamScreenshots(huser, hpipe, "STEAMSCREENSHOTS_INTERFACE_VERSION003");
			this.remoteStorage = this.client.GetISteamRemoteStorage(huser, hpipe, "STEAMREMOTESTORAGE_INTERFACE_VERSION014");
			this.matchmaking = this.client.GetISteamMatchmaking(huser, hpipe, "SteamMatchMaking009");
			this.applist = this.client.GetISteamAppList(huser, hpipe, "STEAMAPPLIST_INTERFACE_VERSION001");
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0003B660 File Offset: 0x00039860
		public void Dispose()
		{
			if (this.user != null)
			{
				this.user.Dispose();
				this.user = null;
			}
			if (this.utils != null)
			{
				this.utils.Dispose();
				this.utils = null;
			}
			if (this.networking != null)
			{
				this.networking.Dispose();
				this.networking = null;
			}
			if (this.gameServerStats != null)
			{
				this.gameServerStats.Dispose();
				this.gameServerStats = null;
			}
			if (this.http != null)
			{
				this.http.Dispose();
				this.http = null;
			}
			if (this.inventory != null)
			{
				this.inventory.Dispose();
				this.inventory = null;
			}
			if (this.ugc != null)
			{
				this.ugc.Dispose();
				this.ugc = null;
			}
			if (this.apps != null)
			{
				this.apps.Dispose();
				this.apps = null;
			}
			if (this.gameServer != null)
			{
				this.gameServer.Dispose();
				this.gameServer = null;
			}
			if (this.friends != null)
			{
				this.friends.Dispose();
				this.friends = null;
			}
			if (this.servers != null)
			{
				this.servers.Dispose();
				this.servers = null;
			}
			if (this.userstats != null)
			{
				this.userstats.Dispose();
				this.userstats = null;
			}
			if (this.screenshots != null)
			{
				this.screenshots.Dispose();
				this.screenshots = null;
			}
			if (this.remoteStorage != null)
			{
				this.remoteStorage.Dispose();
				this.remoteStorage = null;
			}
			if (this.matchmaking != null)
			{
				this.matchmaking.Dispose();
				this.matchmaking = null;
			}
			if (this.applist != null)
			{
				this.applist.Dispose();
				this.applist = null;
			}
			if (this.client != null)
			{
				this.client.Dispose();
				this.client = null;
			}
			if (this.api != null)
			{
				if (this.isServer)
				{
					this.api.SteamGameServer_Shutdown();
				}
				else
				{
					this.api.SteamAPI_Shutdown();
				}
				this.api.Dispose();
				this.api = null;
			}
		}

		// Token: 0x040008A4 RID: 2212
		internal SteamApi api;

		// Token: 0x040008A5 RID: 2213
		internal SteamClient client;

		// Token: 0x040008A6 RID: 2214
		internal SteamUser user;

		// Token: 0x040008A7 RID: 2215
		internal SteamApps apps;

		// Token: 0x040008A8 RID: 2216
		internal SteamAppList applist;

		// Token: 0x040008A9 RID: 2217
		internal SteamFriends friends;

		// Token: 0x040008AA RID: 2218
		internal SteamMatchmakingServers servers;

		// Token: 0x040008AB RID: 2219
		internal SteamMatchmaking matchmaking;

		// Token: 0x040008AC RID: 2220
		internal SteamInventory inventory;

		// Token: 0x040008AD RID: 2221
		internal SteamNetworking networking;

		// Token: 0x040008AE RID: 2222
		internal SteamUserStats userstats;

		// Token: 0x040008AF RID: 2223
		internal SteamUtils utils;

		// Token: 0x040008B0 RID: 2224
		internal SteamScreenshots screenshots;

		// Token: 0x040008B1 RID: 2225
		internal SteamHTTP http;

		// Token: 0x040008B2 RID: 2226
		internal SteamUGC ugc;

		// Token: 0x040008B3 RID: 2227
		internal SteamGameServer gameServer;

		// Token: 0x040008B4 RID: 2228
		internal SteamGameServerStats gameServerStats;

		// Token: 0x040008B5 RID: 2229
		internal SteamRemoteStorage remoteStorage;

		// Token: 0x040008B6 RID: 2230
		private bool isServer;
	}
}
