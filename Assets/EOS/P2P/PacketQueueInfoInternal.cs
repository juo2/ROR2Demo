using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002AB RID: 683
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PacketQueueInfoInternal : ISettable, IDisposable
	{
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x00012997 File Offset: 0x00010B97
		// (set) Token: 0x0600115E RID: 4446 RVA: 0x0001299F File Offset: 0x00010B9F
		public ulong IncomingPacketQueueMaxSizeBytes
		{
			get
			{
				return this.m_IncomingPacketQueueMaxSizeBytes;
			}
			set
			{
				this.m_IncomingPacketQueueMaxSizeBytes = value;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x000129A8 File Offset: 0x00010BA8
		// (set) Token: 0x06001160 RID: 4448 RVA: 0x000129B0 File Offset: 0x00010BB0
		public ulong IncomingPacketQueueCurrentSizeBytes
		{
			get
			{
				return this.m_IncomingPacketQueueCurrentSizeBytes;
			}
			set
			{
				this.m_IncomingPacketQueueCurrentSizeBytes = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x000129B9 File Offset: 0x00010BB9
		// (set) Token: 0x06001162 RID: 4450 RVA: 0x000129C1 File Offset: 0x00010BC1
		public ulong IncomingPacketQueueCurrentPacketCount
		{
			get
			{
				return this.m_IncomingPacketQueueCurrentPacketCount;
			}
			set
			{
				this.m_IncomingPacketQueueCurrentPacketCount = value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x000129CA File Offset: 0x00010BCA
		// (set) Token: 0x06001164 RID: 4452 RVA: 0x000129D2 File Offset: 0x00010BD2
		public ulong OutgoingPacketQueueMaxSizeBytes
		{
			get
			{
				return this.m_OutgoingPacketQueueMaxSizeBytes;
			}
			set
			{
				this.m_OutgoingPacketQueueMaxSizeBytes = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x000129DB File Offset: 0x00010BDB
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x000129E3 File Offset: 0x00010BE3
		public ulong OutgoingPacketQueueCurrentSizeBytes
		{
			get
			{
				return this.m_OutgoingPacketQueueCurrentSizeBytes;
			}
			set
			{
				this.m_OutgoingPacketQueueCurrentSizeBytes = value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x000129EC File Offset: 0x00010BEC
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x000129F4 File Offset: 0x00010BF4
		public ulong OutgoingPacketQueueCurrentPacketCount
		{
			get
			{
				return this.m_OutgoingPacketQueueCurrentPacketCount;
			}
			set
			{
				this.m_OutgoingPacketQueueCurrentPacketCount = value;
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00012A00 File Offset: 0x00010C00
		public void Set(PacketQueueInfo other)
		{
			if (other != null)
			{
				this.IncomingPacketQueueMaxSizeBytes = other.IncomingPacketQueueMaxSizeBytes;
				this.IncomingPacketQueueCurrentSizeBytes = other.IncomingPacketQueueCurrentSizeBytes;
				this.IncomingPacketQueueCurrentPacketCount = other.IncomingPacketQueueCurrentPacketCount;
				this.OutgoingPacketQueueMaxSizeBytes = other.OutgoingPacketQueueMaxSizeBytes;
				this.OutgoingPacketQueueCurrentSizeBytes = other.OutgoingPacketQueueCurrentSizeBytes;
				this.OutgoingPacketQueueCurrentPacketCount = other.OutgoingPacketQueueCurrentPacketCount;
			}
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00012A58 File Offset: 0x00010C58
		public void Set(object other)
		{
			this.Set(other as PacketQueueInfo);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000825 RID: 2085
		private ulong m_IncomingPacketQueueMaxSizeBytes;

		// Token: 0x04000826 RID: 2086
		private ulong m_IncomingPacketQueueCurrentSizeBytes;

		// Token: 0x04000827 RID: 2087
		private ulong m_IncomingPacketQueueCurrentPacketCount;

		// Token: 0x04000828 RID: 2088
		private ulong m_OutgoingPacketQueueMaxSizeBytes;

		// Token: 0x04000829 RID: 2089
		private ulong m_OutgoingPacketQueueCurrentSizeBytes;

		// Token: 0x0400082A RID: 2090
		private ulong m_OutgoingPacketQueueCurrentPacketCount;
	}
}
