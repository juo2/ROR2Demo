using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000027 RID: 39
	public class ExternalUserInfo : ISettable
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00003D3F File Offset: 0x00001F3F
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x00003D47 File Offset: 0x00001F47
		public ExternalAccountType AccountType { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00003D50 File Offset: 0x00001F50
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x00003D58 File Offset: 0x00001F58
		public string AccountId { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00003D61 File Offset: 0x00001F61
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x00003D69 File Offset: 0x00001F69
		public string DisplayName { get; set; }

		// Token: 0x060002E7 RID: 743 RVA: 0x00003D74 File Offset: 0x00001F74
		internal void Set(ExternalUserInfoInternal? other)
		{
			if (other != null)
			{
				this.AccountType = other.Value.AccountType;
				this.AccountId = other.Value.AccountId;
				this.DisplayName = other.Value.DisplayName;
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00003DC9 File Offset: 0x00001FC9
		public void Set(object other)
		{
			this.Set(other as ExternalUserInfoInternal?);
		}
	}
}
