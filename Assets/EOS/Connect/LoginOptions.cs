using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004D9 RID: 1241
	public class LoginOptions
	{
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06001E1D RID: 7709 RVA: 0x000200DC File Offset: 0x0001E2DC
		// (set) Token: 0x06001E1E RID: 7710 RVA: 0x000200E4 File Offset: 0x0001E2E4
		public Credentials Credentials { get; set; }

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x000200ED File Offset: 0x0001E2ED
		// (set) Token: 0x06001E20 RID: 7712 RVA: 0x000200F5 File Offset: 0x0001E2F5
		public UserLoginInfo UserLoginInfo { get; set; }
	}
}
