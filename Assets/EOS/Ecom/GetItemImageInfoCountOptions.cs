using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200045D RID: 1117
	public class GetItemImageInfoCountOptions
	{
		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x0001D1C7 File Offset: 0x0001B3C7
		// (set) Token: 0x06001B57 RID: 6999 RVA: 0x0001D1CF File Offset: 0x0001B3CF
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x0001D1D8 File Offset: 0x0001B3D8
		// (set) Token: 0x06001B59 RID: 7001 RVA: 0x0001D1E0 File Offset: 0x0001B3E0
		public string ItemId { get; set; }
	}
}
