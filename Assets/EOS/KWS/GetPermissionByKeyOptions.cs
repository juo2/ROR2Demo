using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003E5 RID: 997
	public class GetPermissionByKeyOptions
	{
		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x000197C4 File Offset: 0x000179C4
		// (set) Token: 0x06001825 RID: 6181 RVA: 0x000197CC File Offset: 0x000179CC
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x000197D5 File Offset: 0x000179D5
		// (set) Token: 0x06001827 RID: 6183 RVA: 0x000197DD File Offset: 0x000179DD
		public string Key { get; set; }
	}
}
