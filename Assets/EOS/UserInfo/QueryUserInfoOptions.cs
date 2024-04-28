using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200003B RID: 59
	public class QueryUserInfoOptions
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000368 RID: 872 RVA: 0x000044BC File Offset: 0x000026BC
		// (set) Token: 0x06000369 RID: 873 RVA: 0x000044C4 File Offset: 0x000026C4
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600036A RID: 874 RVA: 0x000044CD File Offset: 0x000026CD
		// (set) Token: 0x0600036B RID: 875 RVA: 0x000044D5 File Offset: 0x000026D5
		public EpicAccountId TargetUserId { get; set; }
	}
}
