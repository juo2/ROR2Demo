using System;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x0200016B RID: 363
	public class Overlay
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x000368F2 File Offset: 0x00034AF2
		public bool Enabled
		{
			get
			{
				return this.client.native.utils.IsOverlayEnabled();
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00036909 File Offset: 0x00034B09
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x00036911 File Offset: 0x00034B11
		public bool IsOpen { get; private set; }

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0003691A File Offset: 0x00034B1A
		internal Overlay(Client c)
		{
			this.client = c;
			c.RegisterCallback<GameOverlayActivated_t>(new Action<GameOverlayActivated_t>(this.OverlayStateChange));
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0003693B File Offset: 0x00034B3B
		private void OverlayStateChange(GameOverlayActivated_t activation)
		{
			this.IsOpen = (activation.Active == 1);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0003694C File Offset: 0x00034B4C
		public void OpenUserPage(string name, ulong steamid)
		{
			this.client.native.friends.ActivateGameOverlayToUser(name, steamid);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0003696A File Offset: 0x00034B6A
		public void OpenProfile(ulong steamid)
		{
			this.OpenUserPage("steamid", steamid);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00036978 File Offset: 0x00034B78
		public void OpenChat(ulong steamid)
		{
			this.OpenUserPage("chat", steamid);
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00036986 File Offset: 0x00034B86
		public void OpenTrade(ulong steamid)
		{
			this.OpenUserPage("jointrade", steamid);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00036994 File Offset: 0x00034B94
		public void OpenStats(ulong steamid)
		{
			this.OpenUserPage("stats", steamid);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x000369A2 File Offset: 0x00034BA2
		public void OpenAchievements(ulong steamid)
		{
			this.OpenUserPage("achievements", steamid);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000369B0 File Offset: 0x00034BB0
		public void AddFriend(ulong steamid)
		{
			this.OpenUserPage("friendadd", steamid);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000369BE File Offset: 0x00034BBE
		public void RemoveFriend(ulong steamid)
		{
			this.OpenUserPage("friendremove", steamid);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000369CC File Offset: 0x00034BCC
		public void AcceptFriendRequest(ulong steamid)
		{
			this.OpenUserPage("friendrequestaccept", steamid);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000369DA File Offset: 0x00034BDA
		public void IgnoreFriendRequest(ulong steamid)
		{
			this.OpenUserPage("friendrequestignore", steamid);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x000369E8 File Offset: 0x00034BE8
		public void OpenInviteDialog(ulong steamid)
		{
			this.client.native.friends.ActivateGameOverlayInviteDialog(steamid);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00036A05 File Offset: 0x00034C05
		public void OpenUrl(string url)
		{
			this.client.native.friends.ActivateGameOverlayToWebPage(url);
		}

		// Token: 0x04000819 RID: 2073
		internal Client client;
	}
}
