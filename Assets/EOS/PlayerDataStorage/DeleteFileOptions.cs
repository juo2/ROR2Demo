using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000240 RID: 576
	public class DeleteFileOptions
	{
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x0000FF90 File Offset: 0x0000E190
		// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x0000FF98 File Offset: 0x0000E198
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0000FFA1 File Offset: 0x0000E1A1
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x0000FFA9 File Offset: 0x0000E1A9
		public string Filename { get; set; }
	}
}
