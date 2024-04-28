using System;

namespace SteamNative
{
	// Token: 0x0200002D RID: 45
	internal enum ChatMemberStateChange
	{
		// Token: 0x04000217 RID: 535
		Entered = 1,
		// Token: 0x04000218 RID: 536
		Left,
		// Token: 0x04000219 RID: 537
		Disconnected = 4,
		// Token: 0x0400021A RID: 538
		Kicked = 8,
		// Token: 0x0400021B RID: 539
		Banned = 16
	}
}
