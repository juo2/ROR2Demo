using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x02000098 RID: 152
	public class IngestStatOptions
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00006700 File Offset: 0x00004900
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00006708 File Offset: 0x00004908
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00006711 File Offset: 0x00004911
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x00006719 File Offset: 0x00004919
		public IngestData[] Stats { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00006722 File Offset: 0x00004922
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0000672A File Offset: 0x0000492A
		public ProductUserId TargetUserId { get; set; }
	}
}
