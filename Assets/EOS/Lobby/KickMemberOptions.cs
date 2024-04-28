using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000322 RID: 802
	public class KickMemberOptions
	{
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x00015420 File Offset: 0x00013620
		// (set) Token: 0x06001401 RID: 5121 RVA: 0x00015428 File Offset: 0x00013628
		public string LobbyId { get; set; }

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x00015431 File Offset: 0x00013631
		// (set) Token: 0x06001403 RID: 5123 RVA: 0x00015439 File Offset: 0x00013639
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x00015442 File Offset: 0x00013642
		// (set) Token: 0x06001405 RID: 5125 RVA: 0x0001544A File Offset: 0x0001364A
		public ProductUserId TargetUserId { get; set; }
	}
}
