using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200001F RID: 31
	public class CopyExternalUserInfoByAccountIdOptions
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00003A8B File Offset: 0x00001C8B
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x00003A93 File Offset: 0x00001C93
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00003A9C File Offset: 0x00001C9C
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x00003AA4 File Offset: 0x00001CA4
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00003AAD File Offset: 0x00001CAD
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x00003AB5 File Offset: 0x00001CB5
		public string AccountId { get; set; }
	}
}
