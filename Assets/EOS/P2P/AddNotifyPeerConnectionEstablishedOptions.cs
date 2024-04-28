using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200027E RID: 638
	public class AddNotifyPeerConnectionEstablishedOptions
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x000117AF File Offset: 0x0000F9AF
		// (set) Token: 0x06001059 RID: 4185 RVA: 0x000117B7 File Offset: 0x0000F9B7
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x000117C0 File Offset: 0x0000F9C0
		// (set) Token: 0x0600105B RID: 4187 RVA: 0x000117C8 File Offset: 0x0000F9C8
		public SocketId SocketId { get; set; }
	}
}
