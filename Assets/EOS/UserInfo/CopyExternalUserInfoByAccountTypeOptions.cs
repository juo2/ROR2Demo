using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000021 RID: 33
	public class CopyExternalUserInfoByAccountTypeOptions
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00003B4F File Offset: 0x00001D4F
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00003B57 File Offset: 0x00001D57
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00003B60 File Offset: 0x00001D60
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x00003B68 File Offset: 0x00001D68
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00003B71 File Offset: 0x00001D71
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x00003B79 File Offset: 0x00001D79
		public ExternalAccountType AccountType { get; set; }
	}
}
