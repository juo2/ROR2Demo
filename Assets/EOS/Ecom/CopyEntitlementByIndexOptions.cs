using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200043F RID: 1087
	public class CopyEntitlementByIndexOptions
	{
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x0001C05D File Offset: 0x0001A25D
		// (set) Token: 0x06001A89 RID: 6793 RVA: 0x0001C065 File Offset: 0x0001A265
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x0001C06E File Offset: 0x0001A26E
		// (set) Token: 0x06001A8B RID: 6795 RVA: 0x0001C076 File Offset: 0x0001A276
		public uint EntitlementIndex { get; set; }
	}
}
