using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000417 RID: 1047
	public class GetStatusOptions
	{
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x0001A9F2 File Offset: 0x00018BF2
		// (set) Token: 0x06001934 RID: 6452 RVA: 0x0001A9FA File Offset: 0x00018BFA
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x0001AA03 File Offset: 0x00018C03
		// (set) Token: 0x06001936 RID: 6454 RVA: 0x0001AA0B File Offset: 0x00018C0B
		public EpicAccountId TargetUserId { get; set; }
	}
}
