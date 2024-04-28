using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000037 RID: 55
	public class QueryUserInfoByExternalAccountOptions
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600034A RID: 842 RVA: 0x000042D4 File Offset: 0x000024D4
		// (set) Token: 0x0600034B RID: 843 RVA: 0x000042DC File Offset: 0x000024DC
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600034C RID: 844 RVA: 0x000042E5 File Offset: 0x000024E5
		// (set) Token: 0x0600034D RID: 845 RVA: 0x000042ED File Offset: 0x000024ED
		public string ExternalAccountId { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600034E RID: 846 RVA: 0x000042F6 File Offset: 0x000024F6
		// (set) Token: 0x0600034F RID: 847 RVA: 0x000042FE File Offset: 0x000024FE
		public ExternalAccountType AccountType { get; set; }
	}
}
