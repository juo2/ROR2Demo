using System;

namespace SteamNative
{
	// Token: 0x02000021 RID: 33
	internal enum FriendFlags
	{
		// Token: 0x040001C7 RID: 455
		None,
		// Token: 0x040001C8 RID: 456
		Blocked,
		// Token: 0x040001C9 RID: 457
		FriendshipRequested,
		// Token: 0x040001CA RID: 458
		Immediate = 4,
		// Token: 0x040001CB RID: 459
		ClanMember = 8,
		// Token: 0x040001CC RID: 460
		OnGameServer = 16,
		// Token: 0x040001CD RID: 461
		RequestingFriendship = 128,
		// Token: 0x040001CE RID: 462
		RequestingInfo = 256,
		// Token: 0x040001CF RID: 463
		Ignored = 512,
		// Token: 0x040001D0 RID: 464
		IgnoredFriend = 1024,
		// Token: 0x040001D1 RID: 465
		ChatMember = 4096,
		// Token: 0x040001D2 RID: 466
		All = 65535
	}
}
