using System;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200006A RID: 106
	internal class SteamScreenshots : IDisposable
	{
		// Token: 0x06000253 RID: 595 RVA: 0x00006880 File Offset: 0x00004A80
		internal SteamScreenshots(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000254 RID: 596 RVA: 0x000068FD File Offset: 0x00004AFD
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00006914 File Offset: 0x00004B14
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00006930 File Offset: 0x00004B30
		public ScreenshotHandle AddScreenshotToLibrary(string pchFilename, string pchThumbnailFilename, int nWidth, int nHeight)
		{
			return this.platform.ISteamScreenshots_AddScreenshotToLibrary(pchFilename, pchThumbnailFilename, nWidth, nHeight);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00006942 File Offset: 0x00004B42
		public ScreenshotHandle AddVRScreenshotToLibrary(VRScreenshotType eType, string pchFilename, string pchVRFilename)
		{
			return this.platform.ISteamScreenshots_AddVRScreenshotToLibrary(eType, pchFilename, pchVRFilename);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00006952 File Offset: 0x00004B52
		public void HookScreenshots(bool bHook)
		{
			this.platform.ISteamScreenshots_HookScreenshots(bHook);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00006960 File Offset: 0x00004B60
		public bool IsScreenshotsHooked()
		{
			return this.platform.ISteamScreenshots_IsScreenshotsHooked();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000696D File Offset: 0x00004B6D
		public bool SetLocation(ScreenshotHandle hScreenshot, string pchLocation)
		{
			return this.platform.ISteamScreenshots_SetLocation(hScreenshot.Value, pchLocation);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00006981 File Offset: 0x00004B81
		public bool TagPublishedFile(ScreenshotHandle hScreenshot, PublishedFileId_t unPublishedFileID)
		{
			return this.platform.ISteamScreenshots_TagPublishedFile(hScreenshot.Value, unPublishedFileID.Value);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000699A File Offset: 0x00004B9A
		public bool TagUser(ScreenshotHandle hScreenshot, CSteamID steamID)
		{
			return this.platform.ISteamScreenshots_TagUser(hScreenshot.Value, steamID.Value);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000069B3 File Offset: 0x00004BB3
		public void TriggerScreenshot()
		{
			this.platform.ISteamScreenshots_TriggerScreenshot();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000069C0 File Offset: 0x00004BC0
		public ScreenshotHandle WriteScreenshot(IntPtr pubRGB, uint cubRGB, int nWidth, int nHeight)
		{
			return this.platform.ISteamScreenshots_WriteScreenshot(pubRGB, cubRGB, nWidth, nHeight);
		}

		// Token: 0x04000484 RID: 1156
		internal Platform.Interface platform;

		// Token: 0x04000485 RID: 1157
		internal BaseSteamworks steamworks;
	}
}
