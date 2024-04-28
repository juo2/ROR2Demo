using System;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004A6 RID: 1190
	public class SendCustomInviteOptions
	{
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x0001E89A File Offset: 0x0001CA9A
		// (set) Token: 0x06001CEE RID: 7406 RVA: 0x0001E8A2 File Offset: 0x0001CAA2
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x0001E8AB File Offset: 0x0001CAAB
		// (set) Token: 0x06001CF0 RID: 7408 RVA: 0x0001E8B3 File Offset: 0x0001CAB3
		public ProductUserId[] TargetUserIds { get; set; }
	}
}
