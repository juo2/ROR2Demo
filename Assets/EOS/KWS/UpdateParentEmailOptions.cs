using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000409 RID: 1033
	public class UpdateParentEmailOptions
	{
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x0001A3B4 File Offset: 0x000185B4
		// (set) Token: 0x060018EA RID: 6378 RVA: 0x0001A3BC File Offset: 0x000185BC
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x0001A3C5 File Offset: 0x000185C5
		// (set) Token: 0x060018EC RID: 6380 RVA: 0x0001A3CD File Offset: 0x000185CD
		public string ParentEmail { get; set; }
	}
}
