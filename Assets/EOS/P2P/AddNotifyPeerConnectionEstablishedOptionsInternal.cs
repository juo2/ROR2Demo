using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200027F RID: 639
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyPeerConnectionEstablishedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700049E RID: 1182
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x000117D1 File Offset: 0x0000F9D1
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700049F RID: 1183
		// (set) Token: 0x0600105E RID: 4190 RVA: 0x000117E0 File Offset: 0x0000F9E0
		public SocketId SocketId
		{
			set
			{
				Helper.TryMarshalSet<SocketIdInternal, SocketId>(ref this.m_SocketId, value);
			}
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x000117EF File Offset: 0x0000F9EF
		public void Set(AddNotifyPeerConnectionEstablishedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.SocketId = other.SocketId;
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00011813 File Offset: 0x0000FA13
		public void Set(object other)
		{
			this.Set(other as AddNotifyPeerConnectionEstablishedOptions);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00011821 File Offset: 0x0000FA21
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_SocketId);
		}

		// Token: 0x040007A2 RID: 1954
		private int m_ApiVersion;

		// Token: 0x040007A3 RID: 1955
		private IntPtr m_LocalUserId;

		// Token: 0x040007A4 RID: 1956
		private IntPtr m_SocketId;
	}
}
