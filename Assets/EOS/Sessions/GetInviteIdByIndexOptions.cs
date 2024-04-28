using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D0 RID: 208
	public class GetInviteIdByIndexOptions
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00007AD4 File Offset: 0x00005CD4
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x00007ADC File Offset: 0x00005CDC
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x00007AE5 File Offset: 0x00005CE5
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x00007AED File Offset: 0x00005CED
		public uint Index { get; set; }
	}
}
