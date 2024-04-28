using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004FB RID: 1275
	public class QueryProductUserIdMappingsOptions
	{
		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x00020530 File Offset: 0x0001E730
		// (set) Token: 0x06001EC2 RID: 7874 RVA: 0x00020538 File Offset: 0x0001E738
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x00020541 File Offset: 0x0001E741
		// (set) Token: 0x06001EC4 RID: 7876 RVA: 0x00020549 File Offset: 0x0001E749
		public ExternalAccountType AccountIdType_DEPRECATED { get; set; }

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x00020552 File Offset: 0x0001E752
		// (set) Token: 0x06001EC6 RID: 7878 RVA: 0x0002055A File Offset: 0x0001E75A
		public ProductUserId[] ProductUserIds { get; set; }
	}
}
