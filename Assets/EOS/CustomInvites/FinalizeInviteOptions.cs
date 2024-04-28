using System;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x02000498 RID: 1176
	public class FinalizeInviteOptions
	{
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x0001E344 File Offset: 0x0001C544
		// (set) Token: 0x06001C8D RID: 7309 RVA: 0x0001E34C File Offset: 0x0001C54C
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x0001E355 File Offset: 0x0001C555
		// (set) Token: 0x06001C8F RID: 7311 RVA: 0x0001E35D File Offset: 0x0001C55D
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x0001E366 File Offset: 0x0001C566
		// (set) Token: 0x06001C91 RID: 7313 RVA: 0x0001E36E File Offset: 0x0001C56E
		public string CustomInviteId { get; set; }

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x0001E377 File Offset: 0x0001C577
		// (set) Token: 0x06001C93 RID: 7315 RVA: 0x0001E37F File Offset: 0x0001C57F
		public Result ProcessingResult { get; set; }
	}
}
