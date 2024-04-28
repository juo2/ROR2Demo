using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000581 RID: 1409
	public class LogEventOptions
	{
		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x00023A4D File Offset: 0x00021C4D
		// (set) Token: 0x060021CD RID: 8653 RVA: 0x00023A55 File Offset: 0x00021C55
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x00023A5E File Offset: 0x00021C5E
		// (set) Token: 0x060021CF RID: 8655 RVA: 0x00023A66 File Offset: 0x00021C66
		public uint EventId { get; set; }

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x00023A6F File Offset: 0x00021C6F
		// (set) Token: 0x060021D1 RID: 8657 RVA: 0x00023A77 File Offset: 0x00021C77
		public LogEventParamPair[] Params { get; set; }
	}
}
