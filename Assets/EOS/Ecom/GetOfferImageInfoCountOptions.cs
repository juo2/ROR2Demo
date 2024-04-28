using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000463 RID: 1123
	public class GetOfferImageInfoCountOptions
	{
		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x0001D333 File Offset: 0x0001B533
		// (set) Token: 0x06001B72 RID: 7026 RVA: 0x0001D33B File Offset: 0x0001B53B
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x0001D344 File Offset: 0x0001B544
		// (set) Token: 0x06001B74 RID: 7028 RVA: 0x0001D34C File Offset: 0x0001B54C
		public string OfferId { get; set; }
	}
}
