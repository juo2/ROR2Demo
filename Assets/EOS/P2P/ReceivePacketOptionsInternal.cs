using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002B0 RID: 688
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReceivePacketOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170004FA RID: 1274
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x00012AB3 File Offset: 0x00010CB3
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (set) Token: 0x06001178 RID: 4472 RVA: 0x00012AC2 File Offset: 0x00010CC2
		public uint MaxDataSizeBytes
		{
			set
			{
				this.m_MaxDataSizeBytes = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (set) Token: 0x06001179 RID: 4473 RVA: 0x00012ACB File Offset: 0x00010CCB
		public byte? RequestedChannel
		{
			set
			{
				Helper.TryMarshalSet<byte>(ref this.m_RequestedChannel, value);
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00012ADA File Offset: 0x00010CDA
		public void Set(ReceivePacketOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.LocalUserId;
				this.MaxDataSizeBytes = other.MaxDataSizeBytes;
				this.RequestedChannel = other.RequestedChannel;
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00012B0A File Offset: 0x00010D0A
		public void Set(object other)
		{
			this.Set(other as ReceivePacketOptions);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00012B18 File Offset: 0x00010D18
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RequestedChannel);
		}

		// Token: 0x04000833 RID: 2099
		private int m_ApiVersion;

		// Token: 0x04000834 RID: 2100
		private IntPtr m_LocalUserId;

		// Token: 0x04000835 RID: 2101
		private uint m_MaxDataSizeBytes;

		// Token: 0x04000836 RID: 2102
		private IntPtr m_RequestedChannel;
	}
}
