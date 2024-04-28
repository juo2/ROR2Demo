using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000104 RID: 260
	public class SendInviteOptions
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x00008380 File Offset: 0x00006580
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x00008388 File Offset: 0x00006588
		public string SessionName { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00008391 File Offset: 0x00006591
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x00008399 File Offset: 0x00006599
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x000083A2 File Offset: 0x000065A2
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x000083AA File Offset: 0x000065AA
		public ProductUserId TargetUserId { get; set; }
	}
}
