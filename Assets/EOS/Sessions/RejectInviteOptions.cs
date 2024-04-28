using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000100 RID: 256
	public class RejectInviteOptions
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00008244 File Offset: 0x00006444
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x0000824C File Offset: 0x0000644C
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00008255 File Offset: 0x00006455
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x0000825D File Offset: 0x0000645D
		public string InviteId { get; set; }
	}
}
