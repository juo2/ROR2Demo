using System;

namespace SteamNative
{
	// Token: 0x0200004D RID: 77
	internal enum ItemUpdateStatus
	{
		// Token: 0x040003F3 RID: 1011
		Invalid,
		// Token: 0x040003F4 RID: 1012
		PreparingConfig,
		// Token: 0x040003F5 RID: 1013
		PreparingContent,
		// Token: 0x040003F6 RID: 1014
		UploadingContent,
		// Token: 0x040003F7 RID: 1015
		UploadingPreviewFile,
		// Token: 0x040003F8 RID: 1016
		CommittingChanges
	}
}
