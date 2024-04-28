using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000310 RID: 784
	public class DestroyLobbyOptions
	{
		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x00014DE0 File Offset: 0x00012FE0
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x00014DE8 File Offset: 0x00012FE8
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x00014DF1 File Offset: 0x00012FF1
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x00014DF9 File Offset: 0x00012FF9
		public string LobbyId { get; set; }
	}
}
