using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000023 RID: 35
	public class CopyExternalUserInfoByIndexOptions
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00003C01 File Offset: 0x00001E01
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00003C09 File Offset: 0x00001E09
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00003C12 File Offset: 0x00001E12
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00003C1A File Offset: 0x00001E1A
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00003C23 File Offset: 0x00001E23
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00003C2B File Offset: 0x00001E2B
		public uint Index { get; set; }
	}
}
