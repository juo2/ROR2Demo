using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000451 RID: 1105
	public class CopyTransactionByIdOptions
	{
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x0001C5E3 File Offset: 0x0001A7E3
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x0001C5EB File Offset: 0x0001A7EB
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
		// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x0001C5FC File Offset: 0x0001A7FC
		public string TransactionId { get; set; }
	}
}
