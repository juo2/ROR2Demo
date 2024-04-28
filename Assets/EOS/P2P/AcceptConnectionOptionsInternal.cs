using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000279 RID: 633
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AcceptConnectionOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000495 RID: 1173
		// (set) Token: 0x06001044 RID: 4164 RVA: 0x00011678 File Offset: 0x0000F878
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (set) Token: 0x06001045 RID: 4165 RVA: 0x00011687 File Offset: 0x0000F887
		public ProductUserId RemoteUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RemoteUserId, value);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (set) Token: 0x06001046 RID: 4166 RVA: 0x00011696 File Offset: 0x0000F896
		public SocketId SocketId
		{
			set
			{
				Helper.TryMarshalSet<SocketIdInternal, SocketId>(ref this.m_SocketId, value);
			}
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x000116A5 File Offset: 0x0000F8A5
		public void Set(AcceptConnectionOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RemoteUserId = other.RemoteUserId;
				this.SocketId = other.SocketId;
			}
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000116D5 File Offset: 0x0000F8D5
		public void Set(object other)
		{
			this.Set(other as AcceptConnectionOptions);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x000116E3 File Offset: 0x0000F8E3
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RemoteUserId);
			Helper.TryMarshalDispose(ref this.m_SocketId);
		}

		// Token: 0x04000796 RID: 1942
		private int m_ApiVersion;

		// Token: 0x04000797 RID: 1943
		private IntPtr m_LocalUserId;

		// Token: 0x04000798 RID: 1944
		private IntPtr m_RemoteUserId;

		// Token: 0x04000799 RID: 1945
		private IntPtr m_SocketId;
	}
}
