using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000286 RID: 646
	public class CloseConnectionsOptions
	{
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x00011A4F File Offset: 0x0000FC4F
		// (set) Token: 0x06001087 RID: 4231 RVA: 0x00011A57 File Offset: 0x0000FC57
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x00011A60 File Offset: 0x0000FC60
		// (set) Token: 0x06001089 RID: 4233 RVA: 0x00011A68 File Offset: 0x0000FC68
		public SocketId SocketId { get; set; }
	}
}
