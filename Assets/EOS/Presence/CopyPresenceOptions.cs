using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000207 RID: 519
	public class CopyPresenceOptions
	{
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x0000E958 File Offset: 0x0000CB58
		// (set) Token: 0x06000D8E RID: 3470 RVA: 0x0000E960 File Offset: 0x0000CB60
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0000E969 File Offset: 0x0000CB69
		// (set) Token: 0x06000D90 RID: 3472 RVA: 0x0000E971 File Offset: 0x0000CB71
		public EpicAccountId TargetUserId { get; set; }
	}
}
