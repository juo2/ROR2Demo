using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200043D RID: 1085
	public class CopyEntitlementByIdOptions
	{
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x0001BFD1 File Offset: 0x0001A1D1
		// (set) Token: 0x06001A7F RID: 6783 RVA: 0x0001BFD9 File Offset: 0x0001A1D9
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x0001BFE2 File Offset: 0x0001A1E2
		// (set) Token: 0x06001A81 RID: 6785 RVA: 0x0001BFEA File Offset: 0x0001A1EA
		public string EntitlementId { get; set; }
	}
}
