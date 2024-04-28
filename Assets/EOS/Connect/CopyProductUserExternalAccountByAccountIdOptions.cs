using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004B3 RID: 1203
	public class CopyProductUserExternalAccountByAccountIdOptions
	{
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x0001F348 File Offset: 0x0001D548
		// (set) Token: 0x06001D43 RID: 7491 RVA: 0x0001F350 File Offset: 0x0001D550
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0001F359 File Offset: 0x0001D559
		// (set) Token: 0x06001D45 RID: 7493 RVA: 0x0001F361 File Offset: 0x0001D561
		public string AccountId { get; set; }
	}
}
