using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005CF RID: 1487
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReceiveMessageFromPeerOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B28 RID: 2856
		// (set) Token: 0x060023DD RID: 9181 RVA: 0x00025D90 File Offset: 0x00023F90
		public IntPtr PeerHandle
		{
			set
			{
				this.m_PeerHandle = value;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (set) Token: 0x060023DE RID: 9182 RVA: 0x00025D99 File Offset: 0x00023F99
		public byte[] Data
		{
			set
			{
				Helper.TryMarshalSet<byte>(ref this.m_Data, value, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x00025DAE File Offset: 0x00023FAE
		public void Set(ReceiveMessageFromPeerOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.PeerHandle = other.PeerHandle;
				this.Data = other.Data;
			}
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x00025DD2 File Offset: 0x00023FD2
		public void Set(object other)
		{
			this.Set(other as ReceiveMessageFromPeerOptions);
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x00025DE0 File Offset: 0x00023FE0
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_PeerHandle);
			Helper.TryMarshalDispose(ref this.m_Data);
		}

		// Token: 0x040010EF RID: 4335
		private int m_ApiVersion;

		// Token: 0x040010F0 RID: 4336
		private IntPtr m_PeerHandle;

		// Token: 0x040010F1 RID: 4337
		private uint m_DataLengthBytes;

		// Token: 0x040010F2 RID: 4338
		private IntPtr m_Data;
	}
}
