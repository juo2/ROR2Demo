using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000413 RID: 1043
	public class GetFriendAtIndexOptions
	{
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x0001A924 File Offset: 0x00018B24
		// (set) Token: 0x06001923 RID: 6435 RVA: 0x0001A92C File Offset: 0x00018B2C
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x0001A935 File Offset: 0x00018B35
		// (set) Token: 0x06001925 RID: 6437 RVA: 0x0001A93D File Offset: 0x00018B3D
		public int Index { get; set; }
	}
}
