using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000316 RID: 790
	public class GetRTCRoomNameOptions
	{
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x00014F3A File Offset: 0x0001313A
		// (set) Token: 0x060013B3 RID: 5043 RVA: 0x00014F42 File Offset: 0x00013142
		public string LobbyId { get; set; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00014F4B File Offset: 0x0001314B
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x00014F53 File Offset: 0x00013153
		public ProductUserId LocalUserId { get; set; }
	}
}
