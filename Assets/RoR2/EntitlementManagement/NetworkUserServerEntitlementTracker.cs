using System;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace RoR2.EntitlementManagement
{
	// Token: 0x02000C94 RID: 3220
	public class NetworkUserServerEntitlementTracker : BaseUserEntitlementTracker<NetworkUser>, IDisposable
	{
		// Token: 0x060049AD RID: 18861 RVA: 0x0012EC70 File Offset: 0x0012CE70
		protected override void SubscribeToUserDiscovered()
		{
			NetworkUser.onNetworkUserDiscovered += this.OnUserDiscovered;
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x0012EC84 File Offset: 0x0012CE84
		protected override void SubscribeToUserLost()
		{
			NetworkUser.onNetworkUserLost += this.OnUserLost;
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x0012EC98 File Offset: 0x0012CE98
		protected override void UnsubscribeFromUserDiscovered()
		{
			NetworkUser.onNetworkUserDiscovered -= this.OnUserDiscovered;
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x0012ECAC File Offset: 0x0012CEAC
		protected override void UnsubscribeFromUserLost()
		{
			NetworkUser.onNetworkUserLost -= this.OnUserLost;
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x0012ECC0 File Offset: 0x0012CEC0
		protected override IList<NetworkUser> GetCurrentUsers()
		{
			return NetworkUser.readOnlyInstancesList;
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x0012ECC7 File Offset: 0x0012CEC7
		public NetworkUserServerEntitlementTracker(IUserEntitlementResolver<NetworkUser>[] entitlementResolvers) : base(entitlementResolvers)
		{
			NetworkUser.onPostNetworkUserStart += this.OnPostNetworkUserStart;
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x0012ECE1 File Offset: 0x0012CEE1
		private void OnPostNetworkUserStart(NetworkUser networkUser)
		{
			if (NetworkServer.active)
			{
				base.UpdateUserEntitlements(networkUser);
			}
		}
	}
}
