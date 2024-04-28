using System;
using RoR2.EntitlementManagement;

namespace RoR2
{
	// Token: 0x020009CB RID: 2507
	public interface IUserEntitementsResolverNetworkAndLocal : IUserEntitlementResolver<NetworkUser>, IUserEntitlementResolver<LocalUser>
	{
		// Token: 0x06003982 RID: 14722
		string[] BuildEntitlements();
	}
}
