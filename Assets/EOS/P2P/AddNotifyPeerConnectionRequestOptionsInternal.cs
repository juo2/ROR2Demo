using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000281 RID: 641
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyPeerConnectionRequestOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170004A2 RID: 1186
		// (set) Token: 0x06001067 RID: 4199 RVA: 0x0001185D File Offset: 0x0000FA5D
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x0001186C File Offset: 0x0000FA6C
		public SocketId SocketId
		{
			set
			{
				Helper.TryMarshalSet<SocketIdInternal, SocketId>(ref this.m_SocketId, value);
			}
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0001187B File Offset: 0x0000FA7B
		public void Set(AddNotifyPeerConnectionRequestOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.SocketId = other.SocketId;
			}
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0001189F File Offset: 0x0000FA9F
		public void Set(object other)
		{
			this.Set(other as AddNotifyPeerConnectionRequestOptions);
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x000118AD File Offset: 0x0000FAAD
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_SocketId);
		}

		// Token: 0x040007A7 RID: 1959
		private int m_ApiVersion;

		// Token: 0x040007A8 RID: 1960
		private IntPtr m_LocalUserId;

		// Token: 0x040007A9 RID: 1961
		private IntPtr m_SocketId;
	}
}
