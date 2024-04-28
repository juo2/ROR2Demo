using System;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200005F RID: 95
	internal class SteamGameServerStats : IDisposable
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00003F18 File Offset: 0x00002118
		internal SteamGameServerStats(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00003F95 File Offset: 0x00002195
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003FAC File Offset: 0x000021AC
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003FC8 File Offset: 0x000021C8
		public bool ClearUserAchievement(CSteamID steamIDUser, string pchName)
		{
			return this.platform.ISteamGameServerStats_ClearUserAchievement(steamIDUser.Value, pchName);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003FDC File Offset: 0x000021DC
		public bool GetUserAchievement(CSteamID steamIDUser, string pchName, ref bool pbAchieved)
		{
			return this.platform.ISteamGameServerStats_GetUserAchievement(steamIDUser.Value, pchName, ref pbAchieved);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003FF1 File Offset: 0x000021F1
		public bool GetUserStat(CSteamID steamIDUser, string pchName, out int pData)
		{
			return this.platform.ISteamGameServerStats_GetUserStat(steamIDUser.Value, pchName, out pData);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004006 File Offset: 0x00002206
		public bool GetUserStat0(CSteamID steamIDUser, string pchName, out float pData)
		{
			return this.platform.ISteamGameServerStats_GetUserStat0(steamIDUser.Value, pchName, out pData);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000401C File Offset: 0x0000221C
		public CallbackHandle RequestUserStats(CSteamID steamIDUser, Action<GSStatsReceived_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamGameServerStats_RequestUserStats(steamIDUser.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return GSStatsReceived_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000405F File Offset: 0x0000225F
		public bool SetUserAchievement(CSteamID steamIDUser, string pchName)
		{
			return this.platform.ISteamGameServerStats_SetUserAchievement(steamIDUser.Value, pchName);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004073 File Offset: 0x00002273
		public bool SetUserStat(CSteamID steamIDUser, string pchName, int nData)
		{
			return this.platform.ISteamGameServerStats_SetUserStat(steamIDUser.Value, pchName, nData);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004088 File Offset: 0x00002288
		public bool SetUserStat0(CSteamID steamIDUser, string pchName, float fData)
		{
			return this.platform.ISteamGameServerStats_SetUserStat0(steamIDUser.Value, pchName, fData);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000040A0 File Offset: 0x000022A0
		public CallbackHandle StoreUserStats(CSteamID steamIDUser, Action<GSStatsStored_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamGameServerStats_StoreUserStats(steamIDUser.Value);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return GSStatsStored_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000040E3 File Offset: 0x000022E3
		public bool UpdateUserAvgRateStat(CSteamID steamIDUser, string pchName, float flCountThisSession, double dSessionLength)
		{
			return this.platform.ISteamGameServerStats_UpdateUserAvgRateStat(steamIDUser.Value, pchName, flCountThisSession, dSessionLength);
		}

		// Token: 0x0400046E RID: 1134
		internal Platform.Interface platform;

		// Token: 0x0400046F RID: 1135
		internal BaseSteamworks steamworks;
	}
}
