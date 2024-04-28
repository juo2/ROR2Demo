using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000A0 RID: 160
	public class QueryStatsOptions
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00006900 File Offset: 0x00004B00
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00006908 File Offset: 0x00004B08
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00006911 File Offset: 0x00004B11
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x00006919 File Offset: 0x00004B19
		public DateTimeOffset? StartTime { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00006922 File Offset: 0x00004B22
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0000692A File Offset: 0x00004B2A
		public DateTimeOffset? EndTime { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00006933 File Offset: 0x00004B33
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x0000693B File Offset: 0x00004B3B
		public string[] StatNames { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00006944 File Offset: 0x00004B44
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x0000694C File Offset: 0x00004B4C
		public ProductUserId TargetUserId { get; set; }
	}
}
