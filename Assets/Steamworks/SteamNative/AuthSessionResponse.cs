using System;

namespace SteamNative
{
	// Token: 0x0200000E RID: 14
	internal enum AuthSessionResponse
	{
		// Token: 0x040000F6 RID: 246
		OK,
		// Token: 0x040000F7 RID: 247
		UserNotConnectedToSteam,
		// Token: 0x040000F8 RID: 248
		NoLicenseOrExpired,
		// Token: 0x040000F9 RID: 249
		VACBanned,
		// Token: 0x040000FA RID: 250
		LoggedInElseWhere,
		// Token: 0x040000FB RID: 251
		VACCheckTimedOut,
		// Token: 0x040000FC RID: 252
		AuthTicketCanceled,
		// Token: 0x040000FD RID: 253
		AuthTicketInvalidAlreadyUsed,
		// Token: 0x040000FE RID: 254
		AuthTicketInvalid,
		// Token: 0x040000FF RID: 255
		PublisherIssuedBan
	}
}
