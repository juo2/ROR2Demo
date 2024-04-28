using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200044F RID: 1103
	public class CopyOfferItemByIndexOptions
	{
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x0001C531 File Offset: 0x0001A731
		// (set) Token: 0x06001AE5 RID: 6885 RVA: 0x0001C539 File Offset: 0x0001A739
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x0001C542 File Offset: 0x0001A742
		// (set) Token: 0x06001AE7 RID: 6887 RVA: 0x0001C54A File Offset: 0x0001A74A
		public string OfferId { get; set; }

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x0001C553 File Offset: 0x0001A753
		// (set) Token: 0x06001AE9 RID: 6889 RVA: 0x0001C55B File Offset: 0x0001A75B
		public uint ItemIndex { get; set; }
	}
}
