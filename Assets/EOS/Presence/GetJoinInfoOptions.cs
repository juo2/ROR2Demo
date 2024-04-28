using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200020D RID: 525
	public class GetJoinInfoOptions
	{
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0000EB53 File Offset: 0x0000CD53
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x0000EB5B File Offset: 0x0000CD5B
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x0000EB64 File Offset: 0x0000CD64
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		public EpicAccountId TargetUserId { get; set; }
	}
}
