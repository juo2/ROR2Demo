using System;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000068 RID: 104
	internal class SteamParentalSettings : IDisposable
	{
		// Token: 0x06000210 RID: 528 RVA: 0x00005B8C File Offset: 0x00003D8C
		internal SteamParentalSettings(BaseSteamworks steamworks, IntPtr pointer)
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00005C09 File Offset: 0x00003E09
		public bool IsValid
		{
			get
			{
				return this.platform != null && this.platform.IsValid;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00005C20 File Offset: 0x00003E20
		public virtual void Dispose()
		{
			if (this.platform != null)
			{
				this.platform.Dispose();
				this.platform = null;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00005C3C File Offset: 0x00003E3C
		public bool BIsAppBlocked(AppId_t nAppID)
		{
			return this.platform.ISteamParentalSettings_BIsAppBlocked(nAppID.Value);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00005C4F File Offset: 0x00003E4F
		public bool BIsAppInBlockList(AppId_t nAppID)
		{
			return this.platform.ISteamParentalSettings_BIsAppInBlockList(nAppID.Value);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00005C62 File Offset: 0x00003E62
		public bool BIsFeatureBlocked(ParentalFeature eFeature)
		{
			return this.platform.ISteamParentalSettings_BIsFeatureBlocked(eFeature);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00005C70 File Offset: 0x00003E70
		public bool BIsFeatureInBlockList(ParentalFeature eFeature)
		{
			return this.platform.ISteamParentalSettings_BIsFeatureInBlockList(eFeature);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00005C7E File Offset: 0x00003E7E
		public bool BIsParentalLockEnabled()
		{
			return this.platform.ISteamParentalSettings_BIsParentalLockEnabled();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00005C8B File Offset: 0x00003E8B
		public bool BIsParentalLockLocked()
		{
			return this.platform.ISteamParentalSettings_BIsParentalLockLocked();
		}

		// Token: 0x04000480 RID: 1152
		internal Platform.Interface platform;

		// Token: 0x04000481 RID: 1153
		internal BaseSteamworks steamworks;
	}
}
