using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000063 RID: 99
	public class CopyFileMetadataAtIndexOptions
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x000052A8 File Offset: 0x000034A8
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x000052B0 File Offset: 0x000034B0
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x000052B9 File Offset: 0x000034B9
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x000052C1 File Offset: 0x000034C1
		public uint Index { get; set; }
	}
}
