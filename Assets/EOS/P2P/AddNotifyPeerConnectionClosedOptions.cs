using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200027C RID: 636
	public class AddNotifyPeerConnectionClosedOptions
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x00011723 File Offset: 0x0000F923
		// (set) Token: 0x0600104F RID: 4175 RVA: 0x0001172B File Offset: 0x0000F92B
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x00011734 File Offset: 0x0000F934
		// (set) Token: 0x06001051 RID: 4177 RVA: 0x0001173C File Offset: 0x0000F93C
		public SocketId SocketId { get; set; }
	}
}
