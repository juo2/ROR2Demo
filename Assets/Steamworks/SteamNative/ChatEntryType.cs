using System;

namespace SteamNative
{
	// Token: 0x02000015 RID: 21
	internal enum ChatEntryType
	{
		// Token: 0x04000149 RID: 329
		Invalid,
		// Token: 0x0400014A RID: 330
		ChatMsg,
		// Token: 0x0400014B RID: 331
		Typing,
		// Token: 0x0400014C RID: 332
		InviteGame,
		// Token: 0x0400014D RID: 333
		Emote,
		// Token: 0x0400014E RID: 334
		LeftConversation = 6,
		// Token: 0x0400014F RID: 335
		Entered,
		// Token: 0x04000150 RID: 336
		WasKicked,
		// Token: 0x04000151 RID: 337
		WasBanned,
		// Token: 0x04000152 RID: 338
		Disconnected,
		// Token: 0x04000153 RID: 339
		HistoricalChat,
		// Token: 0x04000154 RID: 340
		LinkBlocked = 14
	}
}
