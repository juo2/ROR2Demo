using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200045F RID: 1119
	public class GetItemReleaseCountOptions
	{
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x0001D253 File Offset: 0x0001B453
		// (set) Token: 0x06001B61 RID: 7009 RVA: 0x0001D25B File Offset: 0x0001B45B
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0001D264 File Offset: 0x0001B464
		// (set) Token: 0x06001B63 RID: 7011 RVA: 0x0001D26C File Offset: 0x0001B46C
		public string ItemId { get; set; }
	}
}
