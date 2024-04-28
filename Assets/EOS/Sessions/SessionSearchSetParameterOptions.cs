using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200013A RID: 314
	public class SessionSearchSetParameterOptions
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00009630 File Offset: 0x00007830
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x00009638 File Offset: 0x00007838
		public AttributeData Parameter { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00009641 File Offset: 0x00007841
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x00009649 File Offset: 0x00007849
		public ComparisonOp ComparisonOp { get; set; }
	}
}
