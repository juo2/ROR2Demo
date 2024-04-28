using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000399 RID: 921
	public class PromoteMemberOptions
	{
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x00017BE8 File Offset: 0x00015DE8
		// (set) Token: 0x0600166F RID: 5743 RVA: 0x00017BF0 File Offset: 0x00015DF0
		public string LobbyId { get; set; }

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x00017BF9 File Offset: 0x00015DF9
		// (set) Token: 0x06001671 RID: 5745 RVA: 0x00017C01 File Offset: 0x00015E01
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x00017C0A File Offset: 0x00015E0A
		// (set) Token: 0x06001673 RID: 5747 RVA: 0x00017C12 File Offset: 0x00015E12
		public ProductUserId TargetUserId { get; set; }
	}
}
