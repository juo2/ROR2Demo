using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000488 RID: 1160
	public class QueryOwnershipTokenOptions
	{
		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x0001DDA4 File Offset: 0x0001BFA4
		// (set) Token: 0x06001C43 RID: 7235 RVA: 0x0001DDAC File Offset: 0x0001BFAC
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x0001DDB5 File Offset: 0x0001BFB5
		// (set) Token: 0x06001C45 RID: 7237 RVA: 0x0001DDBD File Offset: 0x0001BFBD
		public string[] CatalogItemIds { get; set; }

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x0001DDC6 File Offset: 0x0001BFC6
		// (set) Token: 0x06001C47 RID: 7239 RVA: 0x0001DDCE File Offset: 0x0001BFCE
		public string CatalogNamespace { get; set; }
	}
}
