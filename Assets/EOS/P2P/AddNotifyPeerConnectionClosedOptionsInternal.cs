using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200027D RID: 637
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyPeerConnectionClosedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700049A RID: 1178
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x00011745 File Offset: 0x0000F945
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700049B RID: 1179
		// (set) Token: 0x06001054 RID: 4180 RVA: 0x00011754 File Offset: 0x0000F954
		public SocketId SocketId
		{
			set
			{
				Helper.TryMarshalSet<SocketIdInternal, SocketId>(ref this.m_SocketId, value);
			}
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00011763 File Offset: 0x0000F963
		public void Set(AddNotifyPeerConnectionClosedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.SocketId = other.SocketId;
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00011787 File Offset: 0x0000F987
		public void Set(object other)
		{
			this.Set(other as AddNotifyPeerConnectionClosedOptions);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00011795 File Offset: 0x0000F995
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_SocketId);
		}

		// Token: 0x0400079D RID: 1949
		private int m_ApiVersion;

		// Token: 0x0400079E RID: 1950
		private IntPtr m_LocalUserId;

		// Token: 0x0400079F RID: 1951
		private IntPtr m_SocketId;
	}
}
