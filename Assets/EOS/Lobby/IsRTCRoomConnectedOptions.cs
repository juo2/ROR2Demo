using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000318 RID: 792
	public class IsRTCRoomConnectedOptions
	{
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x00014FC6 File Offset: 0x000131C6
		// (set) Token: 0x060013BD RID: 5053 RVA: 0x00014FCE File Offset: 0x000131CE
		public string LobbyId { get; set; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x00014FD7 File Offset: 0x000131D7
		// (set) Token: 0x060013BF RID: 5055 RVA: 0x00014FDF File Offset: 0x000131DF
		public ProductUserId LocalUserId { get; set; }
	}
}
