using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000526 RID: 1318
	public class LoginOptions
	{
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x00021C40 File Offset: 0x0001FE40
		// (set) Token: 0x06001FE3 RID: 8163 RVA: 0x00021C48 File Offset: 0x0001FE48
		public Credentials Credentials { get; set; }

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06001FE4 RID: 8164 RVA: 0x00021C51 File Offset: 0x0001FE51
		// (set) Token: 0x06001FE5 RID: 8165 RVA: 0x00021C59 File Offset: 0x0001FE59
		public AuthScopeFlags ScopeFlags { get; set; }
	}
}
