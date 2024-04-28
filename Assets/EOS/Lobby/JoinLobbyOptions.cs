using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200031E RID: 798
	public class JoinLobbyOptions
	{
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00015240 File Offset: 0x00013440
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x00015248 File Offset: 0x00013448
		public LobbyDetails LobbyDetailsHandle { get; set; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x00015251 File Offset: 0x00013451
		// (set) Token: 0x060013E5 RID: 5093 RVA: 0x00015259 File Offset: 0x00013459
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00015262 File Offset: 0x00013462
		// (set) Token: 0x060013E7 RID: 5095 RVA: 0x0001526A File Offset: 0x0001346A
		public bool PresenceEnabled { get; set; }

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x00015273 File Offset: 0x00013473
		// (set) Token: 0x060013E9 RID: 5097 RVA: 0x0001527B File Offset: 0x0001347B
		public LocalRTCOptions LocalRTCOptions { get; set; }
	}
}
