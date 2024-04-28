using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000480 RID: 1152
	public class QueryOffersOptions
	{
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x0001D9D8 File Offset: 0x0001BBD8
		// (set) Token: 0x06001C0A RID: 7178 RVA: 0x0001D9E0 File Offset: 0x0001BBE0
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x0001D9E9 File Offset: 0x0001BBE9
		// (set) Token: 0x06001C0C RID: 7180 RVA: 0x0001D9F1 File Offset: 0x0001BBF1
		public string OverrideCatalogNamespace { get; set; }
	}
}
