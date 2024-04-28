using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020002FA RID: 762
	public class AddNotifyRTCRoomConnectionChangedOptions
	{
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x000142CF File Offset: 0x000124CF
		// (set) Token: 0x060012F9 RID: 4857 RVA: 0x000142D7 File Offset: 0x000124D7
		public string LobbyId { get; set; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x000142E0 File Offset: 0x000124E0
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x000142E8 File Offset: 0x000124E8
		public ProductUserId LocalUserId { get; set; }
	}
}
