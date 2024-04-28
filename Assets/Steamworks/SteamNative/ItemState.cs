using System;

namespace SteamNative
{
	// Token: 0x0200004E RID: 78
	internal enum ItemState
	{
		// Token: 0x040003FA RID: 1018
		None,
		// Token: 0x040003FB RID: 1019
		Subscribed,
		// Token: 0x040003FC RID: 1020
		LegacyItem,
		// Token: 0x040003FD RID: 1021
		Installed = 4,
		// Token: 0x040003FE RID: 1022
		NeedsUpdate = 8,
		// Token: 0x040003FF RID: 1023
		Downloading = 16,
		// Token: 0x04000400 RID: 1024
		DownloadPending = 32
	}
}
