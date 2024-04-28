using System;
using System.Runtime.InteropServices;
using System.Text;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200005A RID: 90
	internal class SteamApps : IDisposable
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000027A0 File Offset: 0x000009A0
		internal SteamApps(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000281D File Offset: 0x00000A1D
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002834 File Offset: 0x00000A34
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002850 File Offset: 0x00000A50
		public bool BGetDLCDataByIndex(int iDLC, ref AppId_t pAppID, ref bool pbAvailable, out string pchName)
		{
			pchName = string.Empty;
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			int cchNameBufferSize = 4096;
			bool flag = this.platform.ISteamApps_BGetDLCDataByIndex(iDLC, ref pAppID.Value, ref pbAvailable, stringBuilder, cchNameBufferSize);
			if (!flag)
			{
				return flag;
			}
			pchName = stringBuilder.ToString();
			return flag;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002898 File Offset: 0x00000A98
		public bool BIsAppInstalled(AppId_t appID)
		{
			return this.platform.ISteamApps_BIsAppInstalled(appID.Value);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028AB File Offset: 0x00000AAB
		public bool BIsCybercafe()
		{
			return this.platform.ISteamApps_BIsCybercafe();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000028B8 File Offset: 0x00000AB8
		public bool BIsDlcInstalled(AppId_t appID)
		{
			return this.platform.ISteamApps_BIsDlcInstalled(appID.Value);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000028CB File Offset: 0x00000ACB
		public bool BIsLowViolence()
		{
			return this.platform.ISteamApps_BIsLowViolence();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000028D8 File Offset: 0x00000AD8
		public bool BIsSubscribed()
		{
			return this.platform.ISteamApps_BIsSubscribed();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000028E5 File Offset: 0x00000AE5
		public bool BIsSubscribedApp(AppId_t appID)
		{
			return this.platform.ISteamApps_BIsSubscribedApp(appID.Value);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028F8 File Offset: 0x00000AF8
		public bool BIsSubscribedFromFreeWeekend()
		{
			return this.platform.ISteamApps_BIsSubscribedFromFreeWeekend();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002905 File Offset: 0x00000B05
		public bool BIsVACBanned()
		{
			return this.platform.ISteamApps_BIsVACBanned();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002912 File Offset: 0x00000B12
		public int GetAppBuildId()
		{
			return this.platform.ISteamApps_GetAppBuildId();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002920 File Offset: 0x00000B20
		public string GetAppInstallDir(AppId_t appID)
		{
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			uint cchFolderBufferSize = 4096U;
			if (this.platform.ISteamApps_GetAppInstallDir(appID.Value, stringBuilder, cchFolderBufferSize) <= 0U)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002957 File Offset: 0x00000B57
		public ulong GetAppOwner()
		{
			return this.platform.ISteamApps_GetAppOwner();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002969 File Offset: 0x00000B69
		public string GetAvailableGameLanguages()
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamApps_GetAvailableGameLanguages());
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000297C File Offset: 0x00000B7C
		public string GetCurrentBetaName()
		{
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			int cchNameBufferSize = 4096;
			if (!this.platform.ISteamApps_GetCurrentBetaName(stringBuilder, cchNameBufferSize))
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029AC File Offset: 0x00000BAC
		public string GetCurrentGameLanguage()
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamApps_GetCurrentGameLanguage());
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029BE File Offset: 0x00000BBE
		public int GetDLCCount()
		{
			return this.platform.ISteamApps_GetDLCCount();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029CB File Offset: 0x00000BCB
		public bool GetDlcDownloadProgress(AppId_t nAppID, out ulong punBytesDownloaded, out ulong punBytesTotal)
		{
			return this.platform.ISteamApps_GetDlcDownloadProgress(nAppID.Value, out punBytesDownloaded, out punBytesTotal);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029E0 File Offset: 0x00000BE0
		public uint GetEarliestPurchaseUnixTime(AppId_t nAppID)
		{
			return this.platform.ISteamApps_GetEarliestPurchaseUnixTime(nAppID.Value);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029F4 File Offset: 0x00000BF4
		public CallbackHandle GetFileDetails(string pszFileName, Action<FileDetailsResult_t, bool> CallbackFunction = null)
		{
			SteamAPICall_t steamAPICall_t = 0UL;
			steamAPICall_t = this.platform.ISteamApps_GetFileDetails(pszFileName);
			if (CallbackFunction == null)
			{
				return null;
			}
			if (steamAPICall_t == 0UL)
			{
				return null;
			}
			return FileDetailsResult_t.CallResult(this.steamworks, steamAPICall_t, CallbackFunction);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A32 File Offset: 0x00000C32
		public uint GetInstalledDepots(AppId_t appID, IntPtr pvecDepots, uint cMaxDepots)
		{
			return this.platform.ISteamApps_GetInstalledDepots(appID.Value, pvecDepots, cMaxDepots);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A47 File Offset: 0x00000C47
		public string GetLaunchQueryParam(string pchKey)
		{
			return Marshal.PtrToStringAnsi(this.platform.ISteamApps_GetLaunchQueryParam(pchKey));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A5A File Offset: 0x00000C5A
		public void InstallDLC(AppId_t nAppID)
		{
			this.platform.ISteamApps_InstallDLC(nAppID.Value);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A6D File Offset: 0x00000C6D
		public bool MarkContentCorrupt(bool bMissingFilesOnly)
		{
			return this.platform.ISteamApps_MarkContentCorrupt(bMissingFilesOnly);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A7B File Offset: 0x00000C7B
		public void RequestAllProofOfPurchaseKeys()
		{
			this.platform.ISteamApps_RequestAllProofOfPurchaseKeys();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A88 File Offset: 0x00000C88
		public void RequestAppProofOfPurchaseKey(AppId_t nAppID)
		{
			this.platform.ISteamApps_RequestAppProofOfPurchaseKey(nAppID.Value);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A9B File Offset: 0x00000C9B
		public void UninstallDLC(AppId_t nAppID)
		{
			this.platform.ISteamApps_UninstallDLC(nAppID.Value);
		}

		// Token: 0x04000464 RID: 1124
		internal Platform.Interface platform;

		// Token: 0x04000465 RID: 1125
		internal BaseSteamworks steamworks;
	}
}
