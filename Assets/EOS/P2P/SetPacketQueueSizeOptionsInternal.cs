using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002B5 RID: 693
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetPacketQueueSizeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700050D RID: 1293
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x00012CD9 File Offset: 0x00010ED9
		public ulong IncomingPacketQueueMaxSizeBytes
		{
			set
			{
				this.m_IncomingPacketQueueMaxSizeBytes = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x00012CE2 File Offset: 0x00010EE2
		public ulong OutgoingPacketQueueMaxSizeBytes
		{
			set
			{
				this.m_OutgoingPacketQueueMaxSizeBytes = value;
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00012CEB File Offset: 0x00010EEB
		public void Set(SetPacketQueueSizeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.IncomingPacketQueueMaxSizeBytes = other.IncomingPacketQueueMaxSizeBytes;
				this.OutgoingPacketQueueMaxSizeBytes = other.OutgoingPacketQueueMaxSizeBytes;
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00012D0F File Offset: 0x00010F0F
		public void Set(object other)
		{
			this.Set(other as SetPacketQueueSizeOptions);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400084D RID: 2125
		private int m_ApiVersion;

		// Token: 0x0400084E RID: 2126
		private ulong m_IncomingPacketQueueMaxSizeBytes;

		// Token: 0x0400084F RID: 2127
		private ulong m_OutgoingPacketQueueMaxSizeBytes;
	}
}
