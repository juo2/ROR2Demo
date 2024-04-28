using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000449 RID: 1097
	public class CopyOfferByIdOptions
	{
		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x0001C379 File Offset: 0x0001A579
		// (set) Token: 0x06001AC4 RID: 6852 RVA: 0x0001C381 File Offset: 0x0001A581
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x0001C38A File Offset: 0x0001A58A
		// (set) Token: 0x06001AC6 RID: 6854 RVA: 0x0001C392 File Offset: 0x0001A592
		public string OfferId { get; set; }
	}
}
