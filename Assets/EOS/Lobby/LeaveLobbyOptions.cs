using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000326 RID: 806
	public class LeaveLobbyOptions
	{
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x000155D4 File Offset: 0x000137D4
		// (set) Token: 0x0600141C RID: 5148 RVA: 0x000155DC File Offset: 0x000137DC
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x000155E5 File Offset: 0x000137E5
		// (set) Token: 0x0600141E RID: 5150 RVA: 0x000155ED File Offset: 0x000137ED
		public string LobbyId { get; set; }
	}
}
