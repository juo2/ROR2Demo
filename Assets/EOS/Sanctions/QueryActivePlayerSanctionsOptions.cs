using System;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x02000159 RID: 345
	public class QueryActivePlayerSanctionsOptions
	{
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0000A854 File Offset: 0x00008A54
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x0000A85C File Offset: 0x00008A5C
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0000A865 File Offset: 0x00008A65
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x0000A86D File Offset: 0x00008A6D
		public ProductUserId LocalUserId { get; set; }
	}
}
