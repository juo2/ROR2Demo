using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200029C RID: 668
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnIncomingPacketQueueFullInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00011E4C File Offset: 0x0001004C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x00011E68 File Offset: 0x00010068
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x00011E70 File Offset: 0x00010070
		public ulong PacketQueueMaxSizeBytes
		{
			get
			{
				return this.m_PacketQueueMaxSizeBytes;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x00011E78 File Offset: 0x00010078
		public ulong PacketQueueCurrentSizeBytes
		{
			get
			{
				return this.m_PacketQueueCurrentSizeBytes;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x00011E80 File Offset: 0x00010080
		public ProductUserId OverflowPacketLocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_OverflowPacketLocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x00011E9C File Offset: 0x0001009C
		public byte OverflowPacketChannel
		{
			get
			{
				return this.m_OverflowPacketChannel;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x00011EA4 File Offset: 0x000100A4
		public uint OverflowPacketSizeBytes
		{
			get
			{
				return this.m_OverflowPacketSizeBytes;
			}
		}

		// Token: 0x040007E8 RID: 2024
		private IntPtr m_ClientData;

		// Token: 0x040007E9 RID: 2025
		private ulong m_PacketQueueMaxSizeBytes;

		// Token: 0x040007EA RID: 2026
		private ulong m_PacketQueueCurrentSizeBytes;

		// Token: 0x040007EB RID: 2027
		private IntPtr m_OverflowPacketLocalUserId;

		// Token: 0x040007EC RID: 2028
		private byte m_OverflowPacketChannel;

		// Token: 0x040007ED RID: 2029
		private uint m_OverflowPacketSizeBytes;
	}
}
