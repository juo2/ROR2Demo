using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004CF RID: 1231
	public class GetProductUserIdMappingOptions
	{
		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x0001FC59 File Offset: 0x0001DE59
		// (set) Token: 0x06001DDA RID: 7642 RVA: 0x0001FC61 File Offset: 0x0001DE61
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x0001FC6A File Offset: 0x0001DE6A
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x0001FC72 File Offset: 0x0001DE72
		public ExternalAccountType AccountIdType { get; set; }

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x0001FC7B File Offset: 0x0001DE7B
		// (set) Token: 0x06001DDE RID: 7646 RVA: 0x0001FC83 File Offset: 0x0001DE83
		public ProductUserId TargetProductUserId { get; set; }
	}
}
