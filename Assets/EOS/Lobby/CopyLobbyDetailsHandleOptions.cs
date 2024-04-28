using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000306 RID: 774
	public class CopyLobbyDetailsHandleOptions
	{
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x00014930 File Offset: 0x00012B30
		// (set) Token: 0x06001349 RID: 4937 RVA: 0x00014938 File Offset: 0x00012B38
		public string LobbyId { get; set; }

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x00014941 File Offset: 0x00012B41
		// (set) Token: 0x0600134B RID: 4939 RVA: 0x00014949 File Offset: 0x00012B49
		public ProductUserId LocalUserId { get; set; }
	}
}
