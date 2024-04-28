using System;
using Facepunch.Steamworks;
using RoR2.Networking;

namespace RoR2.EntitlementManagement
{
	// Token: 0x02000C97 RID: 3223
	public static class EntitlementAbstractions
	{
		// Token: 0x060049C4 RID: 18884 RVA: 0x0012EED8 File Offset: 0x0012D0D8
		public static bool VerifyLocalSteamUser(EntitlementDef entitlementDef)
		{
			return Client.Instance.App.IsDlcInstalled(entitlementDef.steamAppId);
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x0012EEF0 File Offset: 0x0012D0F0
		public static bool VerifyRemoteUser(ClientAuthData authData, EntitlementDef entitlementDef)
		{
			for (int i = 0; i < authData.entitlements.Length; i++)
			{
				if (authData.entitlements[i].Equals(entitlementDef.name))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060049C6 RID: 18886 RVA: 0x000026ED File Offset: 0x000008ED
		public static void OpenShopPage(EntitlementDef entitlementDef)
		{
		}

		// Token: 0x04004655 RID: 18005
		public static EntitlementAbstractions.LoggedInPlatform loggedInPlatform;

		// Token: 0x02000C98 RID: 3224
		public enum LoggedInPlatform
		{
			// Token: 0x04004657 RID: 18007
			NONE_ERROR,
			// Token: 0x04004658 RID: 18008
			STEAMWORKS,
			// Token: 0x04004659 RID: 18009
			EPIC_ONLINE_SERVICES
		}
	}
}
