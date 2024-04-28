using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200029B RID: 667
	public class OnIncomingPacketQueueFullInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x00011D24 File Offset: 0x0000FF24
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x00011D2C File Offset: 0x0000FF2C
		public object ClientData { get; private set; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x00011D35 File Offset: 0x0000FF35
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x00011D3D File Offset: 0x0000FF3D
		public ulong PacketQueueMaxSizeBytes { get; private set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x00011D46 File Offset: 0x0000FF46
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x00011D4E File Offset: 0x0000FF4E
		public ulong PacketQueueCurrentSizeBytes { get; private set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00011D57 File Offset: 0x0000FF57
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x00011D5F File Offset: 0x0000FF5F
		public ProductUserId OverflowPacketLocalUserId { get; private set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x00011D68 File Offset: 0x0000FF68
		// (set) Token: 0x060010D4 RID: 4308 RVA: 0x00011D70 File Offset: 0x0000FF70
		public byte OverflowPacketChannel { get; private set; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x00011D79 File Offset: 0x0000FF79
		// (set) Token: 0x060010D6 RID: 4310 RVA: 0x00011D81 File Offset: 0x0000FF81
		public uint OverflowPacketSizeBytes { get; private set; }

		// Token: 0x060010D7 RID: 4311 RVA: 0x00011D8C File Offset: 0x0000FF8C
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00011DA4 File Offset: 0x0000FFA4
		internal void Set(OnIncomingPacketQueueFullInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.PacketQueueMaxSizeBytes = other.Value.PacketQueueMaxSizeBytes;
				this.PacketQueueCurrentSizeBytes = other.Value.PacketQueueCurrentSizeBytes;
				this.OverflowPacketLocalUserId = other.Value.OverflowPacketLocalUserId;
				this.OverflowPacketChannel = other.Value.OverflowPacketChannel;
				this.OverflowPacketSizeBytes = other.Value.OverflowPacketSizeBytes;
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00011E38 File Offset: 0x00010038
		public void Set(object other)
		{
			this.Set(other as OnIncomingPacketQueueFullInfoInternal?);
		}
	}
}
