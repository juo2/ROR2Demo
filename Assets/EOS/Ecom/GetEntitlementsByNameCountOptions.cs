using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000459 RID: 1113
	public class GetEntitlementsByNameCountOptions
	{
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x0001D0E7 File Offset: 0x0001B2E7
		// (set) Token: 0x06001B46 RID: 6982 RVA: 0x0001D0EF File Offset: 0x0001B2EF
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x0001D0F8 File Offset: 0x0001B2F8
		// (set) Token: 0x06001B48 RID: 6984 RVA: 0x0001D100 File Offset: 0x0001B300
		public string EntitlementName { get; set; }
	}
}
