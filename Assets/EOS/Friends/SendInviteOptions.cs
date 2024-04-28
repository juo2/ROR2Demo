using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200042F RID: 1071
	public class SendInviteOptions
	{
		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x0001B02C File Offset: 0x0001922C
		// (set) Token: 0x060019BB RID: 6587 RVA: 0x0001B034 File Offset: 0x00019234
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x0001B03D File Offset: 0x0001923D
		// (set) Token: 0x060019BD RID: 6589 RVA: 0x0001B045 File Offset: 0x00019245
		public EpicAccountId TargetUserId { get; set; }
	}
}
