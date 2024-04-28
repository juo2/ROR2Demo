using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004CB RID: 1227
	public class GetExternalAccountMappingsOptions
	{
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x0001FB53 File Offset: 0x0001DD53
		// (set) Token: 0x06001DC6 RID: 7622 RVA: 0x0001FB5B File Offset: 0x0001DD5B
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x0001FB64 File Offset: 0x0001DD64
		// (set) Token: 0x06001DC8 RID: 7624 RVA: 0x0001FB6C File Offset: 0x0001DD6C
		public ExternalAccountType AccountIdType { get; set; }

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x0001FB75 File Offset: 0x0001DD75
		// (set) Token: 0x06001DCA RID: 7626 RVA: 0x0001FB7D File Offset: 0x0001DD7D
		public string TargetExternalUserId { get; set; }
	}
}
