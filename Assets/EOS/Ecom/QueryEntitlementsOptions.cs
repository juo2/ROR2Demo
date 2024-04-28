using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200047C RID: 1148
	public class QueryEntitlementsOptions
	{
		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x0001D828 File Offset: 0x0001BA28
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x0001D830 File Offset: 0x0001BA30
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0001D839 File Offset: 0x0001BA39
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x0001D841 File Offset: 0x0001BA41
		public string[] EntitlementNames { get; set; }

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x0001D84A File Offset: 0x0001BA4A
		// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x0001D852 File Offset: 0x0001BA52
		public bool IncludeRedeemed { get; set; }
	}
}
