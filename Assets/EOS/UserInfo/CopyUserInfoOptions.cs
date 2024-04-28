using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000025 RID: 37
	public class CopyUserInfoOptions
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00003CB3 File Offset: 0x00001EB3
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x00003CBB File Offset: 0x00001EBB
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00003CC4 File Offset: 0x00001EC4
		// (set) Token: 0x060002DA RID: 730 RVA: 0x00003CCC File Offset: 0x00001ECC
		public EpicAccountId TargetUserId { get; set; }
	}
}
