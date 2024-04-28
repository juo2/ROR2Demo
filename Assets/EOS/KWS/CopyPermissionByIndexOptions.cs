using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003DF RID: 991
	public class CopyPermissionByIndexOptions
	{
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x0001950E File Offset: 0x0001770E
		// (set) Token: 0x060017FA RID: 6138 RVA: 0x00019516 File Offset: 0x00017716
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0001951F File Offset: 0x0001771F
		// (set) Token: 0x060017FC RID: 6140 RVA: 0x00019527 File Offset: 0x00017727
		public uint Index { get; set; }
	}
}
