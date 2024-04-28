using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000280 RID: 640
	public class AddNotifyPeerConnectionRequestOptions
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0001183B File Offset: 0x0000FA3B
		// (set) Token: 0x06001063 RID: 4195 RVA: 0x00011843 File Offset: 0x0000FA43
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0001184C File Offset: 0x0000FA4C
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x00011854 File Offset: 0x0000FA54
		public SocketId SocketId { get; set; }
	}
}
