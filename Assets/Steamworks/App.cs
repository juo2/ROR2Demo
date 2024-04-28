using System;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x02000162 RID: 354
	public class App : IDisposable
	{
		// Token: 0x06000A64 RID: 2660 RVA: 0x000345E7 File Offset: 0x000327E7
		internal App(Client c)
		{
			this.client = c;
			this.client.RegisterCallback<DlcInstalled_t>(new Action<DlcInstalled_t>(this.DlcInstalled));
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000A65 RID: 2661 RVA: 0x00034610 File Offset: 0x00032810
		// (remove) Token: 0x06000A66 RID: 2662 RVA: 0x00034648 File Offset: 0x00032848
		public event App.DlcInstalledDelegate OnDlcInstalled;

		// Token: 0x06000A67 RID: 2663 RVA: 0x0003467D File Offset: 0x0003287D
		private void DlcInstalled(DlcInstalled_t data)
		{
			if (this.OnDlcInstalled != null)
			{
				this.OnDlcInstalled(data.AppID);
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00034698 File Offset: 0x00032898
		public void Dispose()
		{
			this.client = null;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000346A1 File Offset: 0x000328A1
		public void MarkContentCorrupt(bool missingFilesOnly = false)
		{
			this.client.native.apps.MarkContentCorrupt(missingFilesOnly);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000346BA File Offset: 0x000328BA
		public void InstallDlc(uint appId)
		{
			this.client.native.apps.InstallDLC(appId);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000346D7 File Offset: 0x000328D7
		public void UninstallDlc(uint appId)
		{
			this.client.native.apps.UninstallDLC(appId);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000346F4 File Offset: 0x000328F4
		public DateTime PurchaseTime(uint appId)
		{
			uint earliestPurchaseUnixTime = this.client.native.apps.GetEarliestPurchaseUnixTime(appId);
			if (earliestPurchaseUnixTime == 0U)
			{
				return DateTime.MinValue;
			}
			return Utility.Epoch.ToDateTime(earliestPurchaseUnixTime);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00034731 File Offset: 0x00032931
		public bool IsSubscribed(uint appId)
		{
			return this.client.native.apps.BIsSubscribedApp(appId);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0003474E File Offset: 0x0003294E
		public bool IsInstalled(uint appId)
		{
			return this.client.native.apps.BIsAppInstalled(appId);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0003476B File Offset: 0x0003296B
		public bool IsDlcInstalled(uint appId)
		{
			return this.client.native.apps.BIsDlcInstalled(appId);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00034788 File Offset: 0x00032988
		public string GetName(uint appId)
		{
			string appName = this.client.native.applist.GetAppName(appId);
			if (appName == null)
			{
				return "error";
			}
			return appName;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x000347BB File Offset: 0x000329BB
		public string GetInstallFolder(uint appId)
		{
			return this.client.native.applist.GetAppInstallDir(appId);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x000347D8 File Offset: 0x000329D8
		public int GetBuildId(uint appId)
		{
			return this.client.native.applist.GetAppBuildId(appId);
		}

		// Token: 0x040007E1 RID: 2017
		internal Client client;

		// Token: 0x02000258 RID: 600
		// (Invoke) Token: 0x06001D97 RID: 7575
		public delegate void DlcInstalledDelegate(uint appid);
	}
}
