using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000510 RID: 1296
	[Flags]
	public enum AuthScopeFlags
	{
		// Token: 0x04000E7F RID: 3711
		NoFlags = 0,
		// Token: 0x04000E80 RID: 3712
		BasicProfile = 1,
		// Token: 0x04000E81 RID: 3713
		FriendsList = 2,
		// Token: 0x04000E82 RID: 3714
		Presence = 4,
		// Token: 0x04000E83 RID: 3715
		FriendsManagement = 8,
		// Token: 0x04000E84 RID: 3716
		Email = 16
	}
}
