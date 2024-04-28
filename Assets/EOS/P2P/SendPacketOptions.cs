using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002B2 RID: 690
	public class SendPacketOptions
	{
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00012B32 File Offset: 0x00010D32
		// (set) Token: 0x0600117E RID: 4478 RVA: 0x00012B3A File Offset: 0x00010D3A
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x00012B43 File Offset: 0x00010D43
		// (set) Token: 0x06001180 RID: 4480 RVA: 0x00012B4B File Offset: 0x00010D4B
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x00012B54 File Offset: 0x00010D54
		// (set) Token: 0x06001182 RID: 4482 RVA: 0x00012B5C File Offset: 0x00010D5C
		public SocketId SocketId { get; set; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00012B65 File Offset: 0x00010D65
		// (set) Token: 0x06001184 RID: 4484 RVA: 0x00012B6D File Offset: 0x00010D6D
		public byte Channel { get; set; }

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x00012B76 File Offset: 0x00010D76
		// (set) Token: 0x06001186 RID: 4486 RVA: 0x00012B7E File Offset: 0x00010D7E
		public byte[] Data { get; set; }

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x00012B87 File Offset: 0x00010D87
		// (set) Token: 0x06001188 RID: 4488 RVA: 0x00012B8F File Offset: 0x00010D8F
		public bool AllowDelayedDelivery { get; set; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x00012B98 File Offset: 0x00010D98
		// (set) Token: 0x0600118A RID: 4490 RVA: 0x00012BA0 File Offset: 0x00010DA0
		public PacketReliability Reliability { get; set; }
	}
}
