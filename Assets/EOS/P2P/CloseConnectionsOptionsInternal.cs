using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000287 RID: 647
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CloseConnectionsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170004B2 RID: 1202
		// (set) Token: 0x0600108B RID: 4235 RVA: 0x00011A71 File Offset: 0x0000FC71
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (set) Token: 0x0600108C RID: 4236 RVA: 0x00011A80 File Offset: 0x0000FC80
		public SocketId SocketId
		{
			set
			{
				Helper.TryMarshalSet<SocketIdInternal, SocketId>(ref this.m_SocketId, value);
			}
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00011A8F File Offset: 0x0000FC8F
		public void Set(CloseConnectionsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.SocketId = other.SocketId;
			}
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		public void Set(object other)
		{
			this.Set(other as CloseConnectionsOptions);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00011AC1 File Offset: 0x0000FCC1
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_SocketId);
		}

		// Token: 0x040007BA RID: 1978
		private int m_ApiVersion;

		// Token: 0x040007BB RID: 1979
		private IntPtr m_LocalUserId;

		// Token: 0x040007BC RID: 1980
		private IntPtr m_SocketId;
	}
}
