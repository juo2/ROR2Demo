using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200040D RID: 1037
	public class AcceptInviteOptions
	{
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x0001A578 File Offset: 0x00018778
		// (set) Token: 0x06001905 RID: 6405 RVA: 0x0001A580 File Offset: 0x00018780
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x0001A589 File Offset: 0x00018789
		// (set) Token: 0x06001907 RID: 6407 RVA: 0x0001A591 File Offset: 0x00018791
		public EpicAccountId TargetUserId { get; set; }
	}
}
