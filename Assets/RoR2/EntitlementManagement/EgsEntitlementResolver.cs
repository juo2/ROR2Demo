using System;
using System.Collections.Generic;
using System.Linq;
using Epic.OnlineServices;
using Epic.OnlineServices.Ecom;
using JetBrains.Annotations;
using RoR2.ContentManagement;
using RoR2.Networking;

namespace RoR2.EntitlementManagement
{
	// Token: 0x02000C8B RID: 3211
	public class EgsEntitlementResolver : IUserEntitementsResolverNetworkAndLocal, IUserEntitlementResolver<NetworkUser>, IUserEntitlementResolver<LocalUser>
	{
		// Token: 0x140000F8 RID: 248
		// (add) Token: 0x06004977 RID: 18807 RVA: 0x0012E47C File Offset: 0x0012C67C
		// (remove) Token: 0x06004978 RID: 18808 RVA: 0x0012E4B0 File Offset: 0x0012C6B0
		private static event Action onDlcInstalled;

		// Token: 0x06004979 RID: 18809 RVA: 0x0012E4E4 File Offset: 0x0012C6E4
		public EgsEntitlementResolver()
		{
			if (EgsEntitlementResolver.EOS_Ecom == null)
			{
				EgsEntitlementResolver.EOS_Ecom = EOSPlatformManager.GetPlatformInterface().GetEcomInterface();
			}
			RoR2Application.onLoad = (Action)Delegate.Combine(RoR2Application.onLoad, new Action(this.GetEGSEntitlements));
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x0012E53E File Offset: 0x0012C73E
		private void GetEGSEntitlements(EpicAccountId accountId)
		{
			this.GetEGSEntitlements();
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x0012E548 File Offset: 0x0012C748
		private void GetEGSEntitlements()
		{
			if (EOSLoginManager.loggedInAuthId != null)
			{
				string[] catalogItemIds = (from x in EntitlementCatalog.entitlementDefs
				select x.eosItemId).ToArray<string>();
				QueryOwnershipOptions queryOwnershipOptions = new QueryOwnershipOptions();
				queryOwnershipOptions.LocalUserId = EOSLoginManager.loggedInAuthId;
				queryOwnershipOptions.CatalogItemIds = catalogItemIds;
				EgsEntitlementResolver.EOS_Ecom.QueryOwnership(queryOwnershipOptions, null, new OnQueryOwnershipCallback(this.HandleQueryOwnershipCallback));
				return;
			}
			EOSLoginManager.OnAuthLoggedIn += this.GetEGSEntitlements;
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x0012E5D8 File Offset: 0x0012C7D8
		private void HandleQueryOwnershipCallback(QueryOwnershipCallbackInfo data)
		{
			Result? resultCode = data.GetResultCode();
			Result result = Result.Success;
			if (resultCode.GetValueOrDefault() == result & resultCode != null)
			{
				this.ownedEntitlementIDs.Clear();
				for (int i = 0; i < data.ItemOwnership.Length; i++)
				{
					if (data.ItemOwnership[i].OwnershipStatus == OwnershipStatus.Owned)
					{
						this.ownedEntitlementIDs.Add(data.ItemOwnership[i].Id);
					}
				}
			}
			EgsEntitlementResolver.onDlcInstalled();
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x0012E652 File Offset: 0x0012C852
		bool IUserEntitlementResolver<LocalUser>.CheckUserHasEntitlement([NotNull] LocalUser localUser, [NotNull] EntitlementDef entitlementDef)
		{
			return this.CheckLocalUserHasEntitlement(entitlementDef);
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x0012E65C File Offset: 0x0012C85C
		private bool CheckLocalUserHasEntitlement(EntitlementDef entitlementDef)
		{
			for (int i = 0; i < this.ownedEntitlementIDs.Count; i++)
			{
				if (this.ownedEntitlementIDs[i] == entitlementDef.eosItemId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x140000F9 RID: 249
		// (add) Token: 0x0600497F RID: 18815 RVA: 0x0012E69B File Offset: 0x0012C89B
		// (remove) Token: 0x06004980 RID: 18816 RVA: 0x0012E6A3 File Offset: 0x0012C8A3
		event Action IUserEntitlementResolver<LocalUser>.onEntitlementsChanged
		{
			add
			{
				EgsEntitlementResolver.onDlcInstalled += value;
			}
			remove
			{
				EgsEntitlementResolver.onDlcInstalled -= value;
			}
		}

		// Token: 0x140000FA RID: 250
		// (add) Token: 0x06004981 RID: 18817 RVA: 0x0012E69B File Offset: 0x0012C89B
		// (remove) Token: 0x06004982 RID: 18818 RVA: 0x0012E6A3 File Offset: 0x0012C8A3
		event Action IUserEntitlementResolver<NetworkUser>.onEntitlementsChanged
		{
			add
			{
				EgsEntitlementResolver.onDlcInstalled += value;
			}
			remove
			{
				EgsEntitlementResolver.onDlcInstalled -= value;
			}
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x0012E6AC File Offset: 0x0012C8AC
		bool IUserEntitlementResolver<NetworkUser>.CheckUserHasEntitlement([NotNull] NetworkUser networkUser, [NotNull] EntitlementDef entitlementDef)
		{
			if (!networkUser)
			{
				return false;
			}
			if (networkUser.isLocalPlayer)
			{
				return this.CheckLocalUserHasEntitlement(entitlementDef);
			}
			ClientAuthData clientAuthData = ServerAuthManager.FindAuthData(networkUser.connectionToClient);
			if (clientAuthData == null)
			{
				return false;
			}
			CSteamID steamId = clientAuthData.steamId;
			return steamId.isValid && EntitlementAbstractions.VerifyRemoteUser(clientAuthData, entitlementDef);
		}

		// Token: 0x06004984 RID: 18820 RVA: 0x0012E700 File Offset: 0x0012C900
		public string[] BuildEntitlements()
		{
			string[] array = new string[this.ownedEntitlementIDs.Count];
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				foreach (EntitlementDef entitlementDef in ContentManager.entitlementDefs)
				{
					if (entitlementDef.eosItemId == this.ownedEntitlementIDs[i] || "8fc64849a03741faaf51824d6e727cc1" == this.ownedEntitlementIDs[i])
					{
						array[i] = entitlementDef.name;
						break;
					}
				}
			}
			return array;
		}

		// Token: 0x0400463A RID: 17978
		private const string DevAudienceEntitlementName = "8fc64849a03741faaf51824d6e727cc1";

		// Token: 0x0400463C RID: 17980
		private static EcomInterface EOS_Ecom;

		// Token: 0x0400463D RID: 17981
		private List<string> ownedEntitlementIDs = new List<string>();
	}
}
