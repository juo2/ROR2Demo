using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200042B RID: 1067
	public class RejectInviteOptions
	{
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x0001AE68 File Offset: 0x00019068
		// (set) Token: 0x060019A0 RID: 6560 RVA: 0x0001AE70 File Offset: 0x00019070
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x0001AE79 File Offset: 0x00019079
		// (set) Token: 0x060019A2 RID: 6562 RVA: 0x0001AE81 File Offset: 0x00019081
		public EpicAccountId TargetUserId { get; set; }
	}
}
