using System;

namespace SteamNative
{
	// Token: 0x0200000D RID: 13
	internal enum BeginAuthSessionResult
	{
		// Token: 0x040000EF RID: 239
		OK,
		// Token: 0x040000F0 RID: 240
		InvalidTicket,
		// Token: 0x040000F1 RID: 241
		DuplicateRequest,
		// Token: 0x040000F2 RID: 242
		InvalidVersion,
		// Token: 0x040000F3 RID: 243
		GameMismatch,
		// Token: 0x040000F4 RID: 244
		ExpiredTicket
	}
}
