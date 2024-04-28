using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000441 RID: 1089
	public class CopyEntitlementByNameAndIndexOptions
	{
		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001A92 RID: 6802 RVA: 0x0001C0D7 File Offset: 0x0001A2D7
		// (set) Token: 0x06001A93 RID: 6803 RVA: 0x0001C0DF File Offset: 0x0001A2DF
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001A94 RID: 6804 RVA: 0x0001C0E8 File Offset: 0x0001A2E8
		// (set) Token: 0x06001A95 RID: 6805 RVA: 0x0001C0F0 File Offset: 0x0001A2F0
		public string EntitlementName { get; set; }

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x0001C0F9 File Offset: 0x0001A2F9
		// (set) Token: 0x06001A97 RID: 6807 RVA: 0x0001C101 File Offset: 0x0001A301
		public uint Index { get; set; }
	}
}
