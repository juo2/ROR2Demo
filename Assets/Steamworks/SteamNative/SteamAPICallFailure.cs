using System;

namespace SteamNative
{
	// Token: 0x02000025 RID: 37
	internal enum SteamAPICallFailure
	{
		// Token: 0x040001F0 RID: 496
		None = -1,
		// Token: 0x040001F1 RID: 497
		SteamGone,
		// Token: 0x040001F2 RID: 498
		NetworkFailure,
		// Token: 0x040001F3 RID: 499
		InvalidHandle,
		// Token: 0x040001F4 RID: 500
		MismatchedCallback
	}
}
