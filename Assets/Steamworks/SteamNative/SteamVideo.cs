using System;
using System.Text;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200006F RID: 111
	internal class SteamVideo : IDisposable
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0000823C File Offset: 0x0000643C
		internal SteamVideo(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600031B RID: 795 RVA: 0x000082B9 File Offset: 0x000064B9
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000082D0 File Offset: 0x000064D0
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000082EC File Offset: 0x000064EC
		public void GetOPFSettings(AppId_t unVideoAppID)
		{
			this.platform.ISteamVideo_GetOPFSettings(unVideoAppID.Value);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00008300 File Offset: 0x00006500
		public string GetOPFStringForApp(AppId_t unVideoAppID)
		{
			StringBuilder stringBuilder = Helpers.TakeStringBuilder();
			int num = 4096;
			if (!this.platform.ISteamVideo_GetOPFStringForApp(unVideoAppID.Value, stringBuilder, out num))
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00008337 File Offset: 0x00006537
		public void GetVideoURL(AppId_t unVideoAppID)
		{
			this.platform.ISteamVideo_GetVideoURL(unVideoAppID.Value);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000834A File Offset: 0x0000654A
		public bool IsBroadcasting(IntPtr pnNumViewers)
		{
			return this.platform.ISteamVideo_IsBroadcasting(pnNumViewers);
		}

		// Token: 0x0400048E RID: 1166
		internal Platform.Interface platform;

		// Token: 0x0400048F RID: 1167
		internal BaseSteamworks steamworks;
	}
}
