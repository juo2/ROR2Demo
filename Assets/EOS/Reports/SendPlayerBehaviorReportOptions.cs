using System;

namespace Epic.OnlineServices.Reports
{
	// Token: 0x02000162 RID: 354
	public class SendPlayerBehaviorReportOptions
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0000AAF0 File Offset: 0x00008CF0
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		public ProductUserId ReporterUserId { get; set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0000AB01 File Offset: 0x00008D01
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x0000AB09 File Offset: 0x00008D09
		public ProductUserId ReportedUserId { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0000AB12 File Offset: 0x00008D12
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x0000AB1A File Offset: 0x00008D1A
		public PlayerReportsCategory Category { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0000AB23 File Offset: 0x00008D23
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x0000AB2B File Offset: 0x00008D2B
		public string Message { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0000AB34 File Offset: 0x00008D34
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x0000AB3C File Offset: 0x00008D3C
		public string Context { get; set; }
	}
}
