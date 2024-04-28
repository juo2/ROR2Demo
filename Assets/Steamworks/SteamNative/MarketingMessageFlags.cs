using System;

namespace SteamNative
{
	// Token: 0x02000018 RID: 24
	internal enum MarketingMessageFlags
	{
		// Token: 0x04000168 RID: 360
		None,
		// Token: 0x04000169 RID: 361
		HighPriority,
		// Token: 0x0400016A RID: 362
		PlatformWindows,
		// Token: 0x0400016B RID: 363
		PlatformMac = 4,
		// Token: 0x0400016C RID: 364
		PlatformLinux = 8,
		// Token: 0x0400016D RID: 365
		PlatformRestrictions = 14
	}
}
