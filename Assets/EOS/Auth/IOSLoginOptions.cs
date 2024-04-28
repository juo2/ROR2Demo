using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000552 RID: 1362
	public class IOSLoginOptions
	{
		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x00022E3F File Offset: 0x0002103F
		// (set) Token: 0x0600211B RID: 8475 RVA: 0x00022E47 File Offset: 0x00021047
		public IOSCredentials Credentials { get; set; }

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x00022E50 File Offset: 0x00021050
		// (set) Token: 0x0600211D RID: 8477 RVA: 0x00022E58 File Offset: 0x00021058
		public AuthScopeFlags ScopeFlags { get; set; }
	}
}
