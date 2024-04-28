using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200028D RID: 653
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetNextReceivedPacketSizeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170004B6 RID: 1206
		// (set) Token: 0x06001099 RID: 4249 RVA: 0x00011B17 File Offset: 0x0000FD17
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (set) Token: 0x0600109A RID: 4250 RVA: 0x00011B26 File Offset: 0x0000FD26
		public byte? RequestedChannel
		{
			set
			{
				Helper.TryMarshalSet<byte>(ref this.m_RequestedChannel, value);
			}
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00011B35 File Offset: 0x0000FD35
		public void Set(GetNextReceivedPacketSizeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.LocalUserId;
				this.RequestedChannel = other.RequestedChannel;
			}
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00011B59 File Offset: 0x0000FD59
		public void Set(object other)
		{
			this.Set(other as GetNextReceivedPacketSizeOptions);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00011B67 File Offset: 0x0000FD67
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RequestedChannel);
		}

		// Token: 0x040007CF RID: 1999
		private int m_ApiVersion;

		// Token: 0x040007D0 RID: 2000
		private IntPtr m_LocalUserId;

		// Token: 0x040007D1 RID: 2001
		private IntPtr m_RequestedChannel;
	}
}
