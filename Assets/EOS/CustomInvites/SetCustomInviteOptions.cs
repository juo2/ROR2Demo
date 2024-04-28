using System;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004A8 RID: 1192
	public class SetCustomInviteOptions
	{
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x0001E92C File Offset: 0x0001CB2C
		// (set) Token: 0x06001CF8 RID: 7416 RVA: 0x0001E934 File Offset: 0x0001CB34
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x0001E93D File Offset: 0x0001CB3D
		// (set) Token: 0x06001CFA RID: 7418 RVA: 0x0001E945 File Offset: 0x0001CB45
		public string Payload { get; set; }
	}
}
