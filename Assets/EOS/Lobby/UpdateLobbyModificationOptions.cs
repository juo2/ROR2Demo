using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003AB RID: 939
	public class UpdateLobbyModificationOptions
	{
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00018394 File Offset: 0x00016594
		// (set) Token: 0x060016E6 RID: 5862 RVA: 0x0001839C File Offset: 0x0001659C
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x000183A5 File Offset: 0x000165A5
		// (set) Token: 0x060016E8 RID: 5864 RVA: 0x000183AD File Offset: 0x000165AD
		public string LobbyId { get; set; }
	}
}
