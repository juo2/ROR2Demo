using System;
using System.Text;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000059 RID: 89
	internal class SteamAppList : IDisposable
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002624 File Offset: 0x00000824
		internal SteamAppList(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000026A1 File Offset: 0x000008A1
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026B8 File Offset: 0x000008B8
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026D4 File Offset: 0x000008D4
		public int GetAppBuildId(AppId_t nAppID)
		{
			return this.platform.ISteamAppList_GetAppBuildId(nAppID.Value);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000026E8 File Offset: 0x000008E8
		public string GetAppInstallDir(AppId_t nAppID)
		{
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			int cchNameMax = 4096;
			if (this.platform.ISteamAppList_GetAppInstallDir(nAppID.Value, stringBuilder, cchNameMax) <= 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002720 File Offset: 0x00000920
		public string GetAppName(AppId_t nAppID)
		{
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			int cchNameMax = 4096;
			if (this.platform.ISteamAppList_GetAppName(nAppID.Value, stringBuilder, cchNameMax) <= 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002758 File Offset: 0x00000958
		public unsafe uint GetInstalledApps(AppId_t[] pvecAppID)
		{
			uint unMaxAppIDs = (uint)pvecAppID.Length;
			AppId_t* value;
			if (pvecAppID == null || pvecAppID.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &pvecAppID[0];
			}
			return this.platform.ISteamAppList_GetInstalledApps((IntPtr)((void*)value), unMaxAppIDs);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002793 File Offset: 0x00000993
		public uint GetNumInstalledApps()
		{
			return this.platform.ISteamAppList_GetNumInstalledApps();
		}

		// Token: 0x04000462 RID: 1122
		internal Platform.Interface platform;

		// Token: 0x04000463 RID: 1123
		internal BaseSteamworks steamworks;
	}
}
