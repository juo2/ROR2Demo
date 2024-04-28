using System;

namespace SteamNative
{
	// Token: 0x02000016 RID: 22
	internal enum ChatRoomEnterResponse
	{
		// Token: 0x04000156 RID: 342
		Success = 1,
		// Token: 0x04000157 RID: 343
		DoesntExist,
		// Token: 0x04000158 RID: 344
		NotAllowed,
		// Token: 0x04000159 RID: 345
		Full,
		// Token: 0x0400015A RID: 346
		Error,
		// Token: 0x0400015B RID: 347
		Banned,
		// Token: 0x0400015C RID: 348
		Limited,
		// Token: 0x0400015D RID: 349
		ClanDisabled,
		// Token: 0x0400015E RID: 350
		CommunityBan,
		// Token: 0x0400015F RID: 351
		MemberBlockedYou,
		// Token: 0x04000160 RID: 352
		YouBlockedMember,
		// Token: 0x04000161 RID: 353
		RatelimitExceeded = 15
	}
}
