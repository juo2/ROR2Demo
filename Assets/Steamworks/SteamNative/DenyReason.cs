using System;

namespace SteamNative
{
	// Token: 0x0200000C RID: 12
	internal enum DenyReason
	{
		// Token: 0x040000DE RID: 222
		Invalid,
		// Token: 0x040000DF RID: 223
		InvalidVersion,
		// Token: 0x040000E0 RID: 224
		Generic,
		// Token: 0x040000E1 RID: 225
		NotLoggedOn,
		// Token: 0x040000E2 RID: 226
		NoLicense,
		// Token: 0x040000E3 RID: 227
		Cheater,
		// Token: 0x040000E4 RID: 228
		LoggedInElseWhere,
		// Token: 0x040000E5 RID: 229
		UnknownText,
		// Token: 0x040000E6 RID: 230
		IncompatibleAnticheat,
		// Token: 0x040000E7 RID: 231
		MemoryCorruption,
		// Token: 0x040000E8 RID: 232
		IncompatibleSoftware,
		// Token: 0x040000E9 RID: 233
		SteamConnectionLost,
		// Token: 0x040000EA RID: 234
		SteamConnectionError,
		// Token: 0x040000EB RID: 235
		SteamResponseTimedOut,
		// Token: 0x040000EC RID: 236
		SteamValidationStalled,
		// Token: 0x040000ED RID: 237
		SteamOwnerLeftGuestUser
	}
}
