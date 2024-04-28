using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000136 RID: 310
	public class SessionSearchRemoveParameterOptions
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00009576 File Offset: 0x00007776
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x0000957E File Offset: 0x0000777E
		public string Key { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00009587 File Offset: 0x00007787
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x0000958F File Offset: 0x0000778F
		public ComparisonOp ComparisonOp { get; set; }
	}
}
