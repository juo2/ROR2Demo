using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001B0 RID: 432
	public class CopyUserTokenByIndexOptions
	{
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0000C9AE File Offset: 0x0000ABAE
		// (set) Token: 0x06000B84 RID: 2948 RVA: 0x0000C9B6 File Offset: 0x0000ABB6
		public uint UserTokenIndex { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0000C9BF File Offset: 0x0000ABBF
		// (set) Token: 0x06000B86 RID: 2950 RVA: 0x0000C9C7 File Offset: 0x0000ABC7
		public uint QueryId { get; set; }
	}
}
