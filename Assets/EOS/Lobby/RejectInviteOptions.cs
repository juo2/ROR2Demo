using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A3 RID: 931
	public class RejectInviteOptions
	{
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x00018064 File Offset: 0x00016264
		// (set) Token: 0x060016B3 RID: 5811 RVA: 0x0001806C File Offset: 0x0001626C
		public string InviteId { get; set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x00018075 File Offset: 0x00016275
		// (set) Token: 0x060016B5 RID: 5813 RVA: 0x0001807D File Offset: 0x0001627D
		public ProductUserId LocalUserId { get; set; }
	}
}
