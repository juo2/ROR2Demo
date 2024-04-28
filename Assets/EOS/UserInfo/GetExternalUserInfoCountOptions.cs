using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000029 RID: 41
	public class GetExternalUserInfoCountOptions
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00003E9F File Offset: 0x0000209F
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00003EA7 File Offset: 0x000020A7
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00003EB0 File Offset: 0x000020B0
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00003EB8 File Offset: 0x000020B8
		public EpicAccountId TargetUserId { get; set; }
	}
}
