using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000443 RID: 1091
	public class CopyItemByIdOptions
	{
		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x0001C189 File Offset: 0x0001A389
		// (set) Token: 0x06001AA0 RID: 6816 RVA: 0x0001C191 File Offset: 0x0001A391
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001AA1 RID: 6817 RVA: 0x0001C19A File Offset: 0x0001A39A
		// (set) Token: 0x06001AA2 RID: 6818 RVA: 0x0001C1A2 File Offset: 0x0001A3A2
		public string ItemId { get; set; }
	}
}
