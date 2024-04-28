using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200043B RID: 1083
	public class CheckoutOptions
	{
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x0001BF07 File Offset: 0x0001A107
		// (set) Token: 0x06001A72 RID: 6770 RVA: 0x0001BF0F File Offset: 0x0001A10F
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x0001BF18 File Offset: 0x0001A118
		// (set) Token: 0x06001A74 RID: 6772 RVA: 0x0001BF20 File Offset: 0x0001A120
		public string OverrideCatalogNamespace { get; set; }

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x0001BF29 File Offset: 0x0001A129
		// (set) Token: 0x06001A76 RID: 6774 RVA: 0x0001BF31 File Offset: 0x0001A131
		public CheckoutEntry[] Entries { get; set; }
	}
}
