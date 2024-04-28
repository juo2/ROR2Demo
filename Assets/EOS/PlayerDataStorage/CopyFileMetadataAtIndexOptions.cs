using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000236 RID: 566
	public class CopyFileMetadataAtIndexOptions
	{
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0000FC54 File Offset: 0x0000DE54
		// (set) Token: 0x06000EA1 RID: 3745 RVA: 0x0000FC5C File Offset: 0x0000DE5C
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x0000FC65 File Offset: 0x0000DE65
		// (set) Token: 0x06000EA3 RID: 3747 RVA: 0x0000FC6D File Offset: 0x0000DE6D
		public uint Index { get; set; }
	}
}
