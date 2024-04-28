using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A7 RID: 935
	public class SendInviteOptions
	{
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x000181E0 File Offset: 0x000163E0
		// (set) Token: 0x060016CB RID: 5835 RVA: 0x000181E8 File Offset: 0x000163E8
		public string LobbyId { get; set; }

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x000181F1 File Offset: 0x000163F1
		// (set) Token: 0x060016CD RID: 5837 RVA: 0x000181F9 File Offset: 0x000163F9
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x00018202 File Offset: 0x00016402
		// (set) Token: 0x060016CF RID: 5839 RVA: 0x0001820A File Offset: 0x0001640A
		public ProductUserId TargetUserId { get; set; }
	}
}
