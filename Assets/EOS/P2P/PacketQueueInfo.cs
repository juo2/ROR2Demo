using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002AA RID: 682
	public class PacketQueueInfo : ISettable
	{
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x00012888 File Offset: 0x00010A88
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x00012890 File Offset: 0x00010A90
		public ulong IncomingPacketQueueMaxSizeBytes { get; set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x00012899 File Offset: 0x00010A99
		// (set) Token: 0x06001151 RID: 4433 RVA: 0x000128A1 File Offset: 0x00010AA1
		public ulong IncomingPacketQueueCurrentSizeBytes { get; set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x000128AA File Offset: 0x00010AAA
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x000128B2 File Offset: 0x00010AB2
		public ulong IncomingPacketQueueCurrentPacketCount { get; set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x000128BB File Offset: 0x00010ABB
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x000128C3 File Offset: 0x00010AC3
		public ulong OutgoingPacketQueueMaxSizeBytes { get; set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x000128CC File Offset: 0x00010ACC
		// (set) Token: 0x06001157 RID: 4439 RVA: 0x000128D4 File Offset: 0x00010AD4
		public ulong OutgoingPacketQueueCurrentSizeBytes { get; set; }

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x000128DD File Offset: 0x00010ADD
		// (set) Token: 0x06001159 RID: 4441 RVA: 0x000128E5 File Offset: 0x00010AE5
		public ulong OutgoingPacketQueueCurrentPacketCount { get; set; }

		// Token: 0x0600115A RID: 4442 RVA: 0x000128F0 File Offset: 0x00010AF0
		internal void Set(PacketQueueInfoInternal? other)
		{
			if (other != null)
			{
				this.IncomingPacketQueueMaxSizeBytes = other.Value.IncomingPacketQueueMaxSizeBytes;
				this.IncomingPacketQueueCurrentSizeBytes = other.Value.IncomingPacketQueueCurrentSizeBytes;
				this.IncomingPacketQueueCurrentPacketCount = other.Value.IncomingPacketQueueCurrentPacketCount;
				this.OutgoingPacketQueueMaxSizeBytes = other.Value.OutgoingPacketQueueMaxSizeBytes;
				this.OutgoingPacketQueueCurrentSizeBytes = other.Value.OutgoingPacketQueueCurrentSizeBytes;
				this.OutgoingPacketQueueCurrentPacketCount = other.Value.OutgoingPacketQueueCurrentPacketCount;
			}
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00012984 File Offset: 0x00010B84
		public void Set(object other)
		{
			this.Set(other as PacketQueueInfoInternal?);
		}
	}
}
