using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004C9 RID: 1225
	public class ExternalAccountInfo : ISettable
	{
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x0001F922 File Offset: 0x0001DB22
		// (set) Token: 0x06001DAC RID: 7596 RVA: 0x0001F92A File Offset: 0x0001DB2A
		public ProductUserId ProductUserId { get; set; }

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x0001F933 File Offset: 0x0001DB33
		// (set) Token: 0x06001DAE RID: 7598 RVA: 0x0001F93B File Offset: 0x0001DB3B
		public string DisplayName { get; set; }

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x0001F944 File Offset: 0x0001DB44
		// (set) Token: 0x06001DB0 RID: 7600 RVA: 0x0001F94C File Offset: 0x0001DB4C
		public string AccountId { get; set; }

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x0001F955 File Offset: 0x0001DB55
		// (set) Token: 0x06001DB2 RID: 7602 RVA: 0x0001F95D File Offset: 0x0001DB5D
		public ExternalAccountType AccountIdType { get; set; }

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x0001F966 File Offset: 0x0001DB66
		// (set) Token: 0x06001DB4 RID: 7604 RVA: 0x0001F96E File Offset: 0x0001DB6E
		public DateTimeOffset? LastLoginTime { get; set; }

		// Token: 0x06001DB5 RID: 7605 RVA: 0x0001F978 File Offset: 0x0001DB78
		internal void Set(ExternalAccountInfoInternal? other)
		{
			if (other != null)
			{
				this.ProductUserId = other.Value.ProductUserId;
				this.DisplayName = other.Value.DisplayName;
				this.AccountId = other.Value.AccountId;
				this.AccountIdType = other.Value.AccountIdType;
				this.LastLoginTime = other.Value.LastLoginTime;
			}
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x0001F9F7 File Offset: 0x0001DBF7
		public void Set(object other)
		{
			this.Set(other as ExternalAccountInfoInternal?);
		}
	}
}
