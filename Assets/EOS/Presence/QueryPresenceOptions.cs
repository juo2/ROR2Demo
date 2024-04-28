using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200022D RID: 557
	public class QueryPresenceOptions
	{
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x0000FA4C File Offset: 0x0000DC4C
		// (set) Token: 0x06000E77 RID: 3703 RVA: 0x0000FA54 File Offset: 0x0000DC54
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x0000FA5D File Offset: 0x0000DC5D
		// (set) Token: 0x06000E79 RID: 3705 RVA: 0x0000FA65 File Offset: 0x0000DC65
		public EpicAccountId TargetUserId { get; set; }
	}
}
