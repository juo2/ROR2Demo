using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000033 RID: 51
	public class QueryUserInfoByDisplayNameOptions
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000329 RID: 809 RVA: 0x000040A4 File Offset: 0x000022A4
		// (set) Token: 0x0600032A RID: 810 RVA: 0x000040AC File Offset: 0x000022AC
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600032B RID: 811 RVA: 0x000040B5 File Offset: 0x000022B5
		// (set) Token: 0x0600032C RID: 812 RVA: 0x000040BD File Offset: 0x000022BD
		public string DisplayName { get; set; }
	}
}
