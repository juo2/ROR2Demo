using System;
using JetBrains.Annotations;

namespace RoR2.EntitlementManagement
{
	// Token: 0x02000C92 RID: 3218
	public interface IUserEntitlementResolver<TUser> where TUser : class
	{
		// Token: 0x060049A4 RID: 18852
		bool CheckUserHasEntitlement([NotNull] TUser user, [NotNull] EntitlementDef entitlementDef);

		// Token: 0x140000FF RID: 255
		// (add) Token: 0x060049A5 RID: 18853
		// (remove) Token: 0x060049A6 RID: 18854
		event Action onEntitlementsChanged;
	}
}
