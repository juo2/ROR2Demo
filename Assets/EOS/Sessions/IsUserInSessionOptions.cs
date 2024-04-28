using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D2 RID: 210
	public class IsUserInSessionOptions
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x00007B4E File Offset: 0x00005D4E
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x00007B56 File Offset: 0x00005D56
		public string SessionName { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00007B5F File Offset: 0x00005D5F
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x00007B67 File Offset: 0x00005D67
		public ProductUserId TargetUserId { get; set; }
	}
}
