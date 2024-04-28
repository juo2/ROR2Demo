using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200020F RID: 527
	public class HasPresenceOptions
	{
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0000EBDF File Offset: 0x0000CDDF
		// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x0000EBE7 File Offset: 0x0000CDE7
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
		public EpicAccountId TargetUserId { get; set; }
	}
}
