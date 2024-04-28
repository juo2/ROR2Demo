using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000282 RID: 642
	public class ClearPacketQueueOptions
	{
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x000118C7 File Offset: 0x0000FAC7
		// (set) Token: 0x0600106D RID: 4205 RVA: 0x000118CF File Offset: 0x0000FACF
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x000118D8 File Offset: 0x0000FAD8
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x000118E0 File Offset: 0x0000FAE0
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x000118E9 File Offset: 0x0000FAE9
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x000118F1 File Offset: 0x0000FAF1
		public SocketId SocketId { get; set; }
	}
}
