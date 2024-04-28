using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004F7 RID: 1271
	public class QueryExternalAccountMappingsOptions
	{
		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06001EA6 RID: 7846 RVA: 0x00020384 File Offset: 0x0001E584
		// (set) Token: 0x06001EA7 RID: 7847 RVA: 0x0002038C File Offset: 0x0001E58C
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x00020395 File Offset: 0x0001E595
		// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x0002039D File Offset: 0x0001E59D
		public ExternalAccountType AccountIdType { get; set; }

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x000203A6 File Offset: 0x0001E5A6
		// (set) Token: 0x06001EAB RID: 7851 RVA: 0x000203AE File Offset: 0x0001E5AE
		public string[] ExternalAccountIds { get; set; }
	}
}
