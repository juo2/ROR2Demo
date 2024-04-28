using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000284 RID: 644
	public class CloseConnectionOptions
	{
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x0001198B File Offset: 0x0000FB8B
		// (set) Token: 0x0600107A RID: 4218 RVA: 0x00011993 File Offset: 0x0000FB93
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x0001199C File Offset: 0x0000FB9C
		// (set) Token: 0x0600107C RID: 4220 RVA: 0x000119A4 File Offset: 0x0000FBA4
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x000119AD File Offset: 0x0000FBAD
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x000119B5 File Offset: 0x0000FBB5
		public SocketId SocketId { get; set; }
	}
}
