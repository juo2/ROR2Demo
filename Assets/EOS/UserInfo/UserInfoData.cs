using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200003D RID: 61
	public class UserInfoData : ISettable
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00004548 File Offset: 0x00002748
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00004550 File Offset: 0x00002750
		public EpicAccountId UserId { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00004559 File Offset: 0x00002759
		// (set) Token: 0x06000375 RID: 885 RVA: 0x00004561 File Offset: 0x00002761
		public string Country { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000456A File Offset: 0x0000276A
		// (set) Token: 0x06000377 RID: 887 RVA: 0x00004572 File Offset: 0x00002772
		public string DisplayName { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000457B File Offset: 0x0000277B
		// (set) Token: 0x06000379 RID: 889 RVA: 0x00004583 File Offset: 0x00002783
		public string PreferredLanguage { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000458C File Offset: 0x0000278C
		// (set) Token: 0x0600037B RID: 891 RVA: 0x00004594 File Offset: 0x00002794
		public string Nickname { get; set; }

		// Token: 0x0600037C RID: 892 RVA: 0x000045A0 File Offset: 0x000027A0
		internal void Set(UserInfoDataInternal? other)
		{
			if (other != null)
			{
				this.UserId = other.Value.UserId;
				this.Country = other.Value.Country;
				this.DisplayName = other.Value.DisplayName;
				this.PreferredLanguage = other.Value.PreferredLanguage;
				this.Nickname = other.Value.Nickname;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000461F File Offset: 0x0000281F
		public void Set(object other)
		{
			this.Set(other as UserInfoDataInternal?);
		}
	}
}
