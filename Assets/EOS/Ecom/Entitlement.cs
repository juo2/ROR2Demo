using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000457 RID: 1111
	public class Entitlement : ISettable
	{
		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0001CE74 File Offset: 0x0001B074
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x0001CE7C File Offset: 0x0001B07C
		public string EntitlementName { get; set; }

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0001CE85 File Offset: 0x0001B085
		// (set) Token: 0x06001B2A RID: 6954 RVA: 0x0001CE8D File Offset: 0x0001B08D
		public string EntitlementId { get; set; }

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x0001CE96 File Offset: 0x0001B096
		// (set) Token: 0x06001B2C RID: 6956 RVA: 0x0001CE9E File Offset: 0x0001B09E
		public string CatalogItemId { get; set; }

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x0001CEA7 File Offset: 0x0001B0A7
		// (set) Token: 0x06001B2E RID: 6958 RVA: 0x0001CEAF File Offset: 0x0001B0AF
		public int ServerIndex { get; set; }

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x0001CEB8 File Offset: 0x0001B0B8
		// (set) Token: 0x06001B30 RID: 6960 RVA: 0x0001CEC0 File Offset: 0x0001B0C0
		public bool Redeemed { get; set; }

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x0001CEC9 File Offset: 0x0001B0C9
		// (set) Token: 0x06001B32 RID: 6962 RVA: 0x0001CED1 File Offset: 0x0001B0D1
		public long EndTimestamp { get; set; }

		// Token: 0x06001B33 RID: 6963 RVA: 0x0001CEDC File Offset: 0x0001B0DC
		internal void Set(EntitlementInternal? other)
		{
			if (other != null)
			{
				this.EntitlementName = other.Value.EntitlementName;
				this.EntitlementId = other.Value.EntitlementId;
				this.CatalogItemId = other.Value.CatalogItemId;
				this.ServerIndex = other.Value.ServerIndex;
				this.Redeemed = other.Value.Redeemed;
				this.EndTimestamp = other.Value.EndTimestamp;
			}
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0001CF70 File Offset: 0x0001B170
		public void Set(object other)
		{
			this.Set(other as EntitlementInternal?);
		}
	}
}
