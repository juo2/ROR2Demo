using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D8 RID: 216
	public class JoinSessionOptions
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00007D88 File Offset: 0x00005F88
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x00007D90 File Offset: 0x00005F90
		public string SessionName { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00007D99 File Offset: 0x00005F99
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x00007DA1 File Offset: 0x00005FA1
		public SessionDetails SessionHandle { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x00007DAA File Offset: 0x00005FAA
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x00007DB2 File Offset: 0x00005FB2
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00007DBB File Offset: 0x00005FBB
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x00007DC3 File Offset: 0x00005FC3
		public bool PresenceEnabled { get; set; }
	}
}
