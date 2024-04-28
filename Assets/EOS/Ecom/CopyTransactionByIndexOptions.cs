using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000453 RID: 1107
	public class CopyTransactionByIndexOptions
	{
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x0001C66F File Offset: 0x0001A86F
		// (set) Token: 0x06001AFC RID: 6908 RVA: 0x0001C677 File Offset: 0x0001A877
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x0001C680 File Offset: 0x0001A880
		// (set) Token: 0x06001AFE RID: 6910 RVA: 0x0001C688 File Offset: 0x0001A888
		public uint TransactionIndex { get; set; }
	}
}
