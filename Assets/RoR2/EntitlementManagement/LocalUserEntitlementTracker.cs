using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace RoR2.EntitlementManagement
{
	// Token: 0x02000C93 RID: 3219
	public class LocalUserEntitlementTracker : BaseUserEntitlementTracker<LocalUser>, IDisposable
	{
		// Token: 0x060049A7 RID: 18855 RVA: 0x0012EC10 File Offset: 0x0012CE10
		protected override void SubscribeToUserDiscovered()
		{
			LocalUserManager.onUserSignIn += this.OnUserDiscovered;
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x0012EC24 File Offset: 0x0012CE24
		protected override void SubscribeToUserLost()
		{
			LocalUserManager.onUserSignOut += this.OnUserLost;
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x0012EC38 File Offset: 0x0012CE38
		protected override void UnsubscribeFromUserDiscovered()
		{
			LocalUserManager.onUserSignIn -= this.OnUserDiscovered;
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x0012EC4C File Offset: 0x0012CE4C
		protected override void UnsubscribeFromUserLost()
		{
			LocalUserManager.onUserSignOut -= this.OnUserLost;
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x0012EC60 File Offset: 0x0012CE60
		protected override IList<LocalUser> GetCurrentUsers()
		{
			return LocalUserManager.readOnlyLocalUsersList;
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x0012EC67 File Offset: 0x0012CE67
		public LocalUserEntitlementTracker([NotNull] IUserEntitlementResolver<LocalUser>[] entitlementResolvers) : base(entitlementResolvers)
		{
		}
	}
}
