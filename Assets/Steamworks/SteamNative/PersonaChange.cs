using System;

namespace SteamNative
{
	// Token: 0x02000024 RID: 36
	internal enum PersonaChange
	{
		// Token: 0x040001E1 RID: 481
		Name = 1,
		// Token: 0x040001E2 RID: 482
		Status,
		// Token: 0x040001E3 RID: 483
		ComeOnline = 4,
		// Token: 0x040001E4 RID: 484
		GoneOffline = 8,
		// Token: 0x040001E5 RID: 485
		GamePlayed = 16,
		// Token: 0x040001E6 RID: 486
		GameServer = 32,
		// Token: 0x040001E7 RID: 487
		Avatar = 64,
		// Token: 0x040001E8 RID: 488
		JoinedSource = 128,
		// Token: 0x040001E9 RID: 489
		LeftSource = 256,
		// Token: 0x040001EA RID: 490
		RelationshipChanged = 512,
		// Token: 0x040001EB RID: 491
		NameFirstSet = 1024,
		// Token: 0x040001EC RID: 492
		FacebookInfo = 2048,
		// Token: 0x040001ED RID: 493
		Nickname = 4096,
		// Token: 0x040001EE RID: 494
		SteamLevel = 8192
	}
}
