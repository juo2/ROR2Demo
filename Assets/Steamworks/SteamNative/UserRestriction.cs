using System;

namespace SteamNative
{
	// Token: 0x02000022 RID: 34
	internal enum UserRestriction
	{
		// Token: 0x040001D4 RID: 468
		None,
		// Token: 0x040001D5 RID: 469
		Unknown,
		// Token: 0x040001D6 RID: 470
		AnyChat,
		// Token: 0x040001D7 RID: 471
		VoiceChat = 4,
		// Token: 0x040001D8 RID: 472
		GroupChat = 8,
		// Token: 0x040001D9 RID: 473
		Rating = 16,
		// Token: 0x040001DA RID: 474
		GameInvites = 32,
		// Token: 0x040001DB RID: 475
		Trading = 64
	}
}
