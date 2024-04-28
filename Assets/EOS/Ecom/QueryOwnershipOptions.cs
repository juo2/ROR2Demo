using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000484 RID: 1156
	public class QueryOwnershipOptions
	{
		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x0001DBA2 File Offset: 0x0001BDA2
		// (set) Token: 0x06001C25 RID: 7205 RVA: 0x0001DBAA File Offset: 0x0001BDAA
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x0001DBB3 File Offset: 0x0001BDB3
		// (set) Token: 0x06001C27 RID: 7207 RVA: 0x0001DBBB File Offset: 0x0001BDBB
		public string[] CatalogItemIds { get; set; }

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x0001DBC4 File Offset: 0x0001BDC4
		// (set) Token: 0x06001C29 RID: 7209 RVA: 0x0001DBCC File Offset: 0x0001BDCC
		public string CatalogNamespace { get; set; }
	}
}
