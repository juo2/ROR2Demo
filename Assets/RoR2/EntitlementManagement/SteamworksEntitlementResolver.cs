using System;
using System.Collections.Generic;
using Facepunch.Steamworks;
using JetBrains.Annotations;
using RoR2.ContentManagement;
using RoR2.Networking;
using UnityEngine;

namespace RoR2.EntitlementManagement
{
	// Token: 0x02000C95 RID: 3221
	public class SteamworksEntitlementResolver : IUserEntitementsResolverNetworkAndLocal, IUserEntitlementResolver<NetworkUser>, IUserEntitlementResolver<LocalUser>
	{
		// Token: 0x060049B4 RID: 18868 RVA: 0x0012ECF1 File Offset: 0x0012CEF1
		private static void OnDlcInstalled(uint appId)
		{
			Debug.Log(string.Format("OnDlcInstalled appId={0}", appId));
			Action action = SteamworksEntitlementResolver.onDlcInstalled;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x14000100 RID: 256
		// (add) Token: 0x060049B5 RID: 18869 RVA: 0x0012ED18 File Offset: 0x0012CF18
		// (remove) Token: 0x060049B6 RID: 18870 RVA: 0x0012ED4C File Offset: 0x0012CF4C
		private static event Action onDlcInstalled;

		// Token: 0x060049B7 RID: 18871 RVA: 0x0012ED7F File Offset: 0x0012CF7F
		bool IUserEntitlementResolver<LocalUser>.CheckUserHasEntitlement([NotNull] LocalUser localUser, [NotNull] EntitlementDef entitlementDef)
		{
			return EntitlementAbstractions.VerifyLocalSteamUser(entitlementDef);
		}

		// Token: 0x14000101 RID: 257
		// (add) Token: 0x060049B8 RID: 18872 RVA: 0x0012ED87 File Offset: 0x0012CF87
		// (remove) Token: 0x060049B9 RID: 18873 RVA: 0x0012ED8F File Offset: 0x0012CF8F
		event Action IUserEntitlementResolver<LocalUser>.onEntitlementsChanged
		{
			add
			{
				SteamworksEntitlementResolver.onDlcInstalled += value;
			}
			remove
			{
				SteamworksEntitlementResolver.onDlcInstalled -= value;
			}
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x0012ED98 File Offset: 0x0012CF98
		bool IUserEntitlementResolver<NetworkUser>.CheckUserHasEntitlement([NotNull] NetworkUser networkUser, [NotNull] EntitlementDef entitlementDef)
		{
			if (!networkUser)
			{
				return false;
			}
			if (networkUser.isLocalPlayer)
			{
				return EntitlementAbstractions.VerifyLocalSteamUser(entitlementDef);
			}
			ClientAuthData clientAuthData = ServerAuthManager.FindAuthData(networkUser.connectionToClient);
			if (clientAuthData == null)
			{
				return false;
			}
			CSteamID steamId = clientAuthData.steamId;
			return steamId.isValid && EntitlementAbstractions.VerifyRemoteUser(clientAuthData, entitlementDef);
		}

		// Token: 0x14000102 RID: 258
		// (add) Token: 0x060049BB RID: 18875 RVA: 0x000026ED File Offset: 0x000008ED
		// (remove) Token: 0x060049BC RID: 18876 RVA: 0x000026ED File Offset: 0x000008ED
		event Action IUserEntitlementResolver<NetworkUser>.onEntitlementsChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x0012EDE8 File Offset: 0x0012CFE8
		public SteamworksEntitlementResolver()
		{
			EntitlementManager.collectLocalUserEntitlementResolvers += delegate(Action<IUserEntitlementResolver<LocalUser>> add)
			{
				add(this);
			};
			EntitlementManager.collectNetworkUserEntitlementResolvers += delegate(Action<IUserEntitlementResolver<NetworkUser>> add)
			{
				add(this);
			};
			SteamworksClientManager.onLoaded += delegate()
			{
				SteamworksClientManager.instance.steamworksClient.App.OnDlcInstalled += SteamworksEntitlementResolver.OnDlcInstalled;
			};
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x0012EE44 File Offset: 0x0012D044
		public string[] BuildEntitlements()
		{
			List<string> list = new List<string>();
			foreach (EntitlementDef entitlementDef in ContentManager.entitlementDefs)
			{
				if (Client.Instance.App.IsDlcInstalled(entitlementDef.steamAppId))
				{
					list.Add(entitlementDef.name);
				}
			}
			return list.ToArray();
		}
	}
}
