using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000285 RID: 645
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CloseConnectionOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170004AD RID: 1197
		// (set) Token: 0x06001080 RID: 4224 RVA: 0x000119BE File Offset: 0x0000FBBE
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (set) Token: 0x06001081 RID: 4225 RVA: 0x000119CD File Offset: 0x0000FBCD
		public ProductUserId RemoteUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RemoteUserId, value);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (set) Token: 0x06001082 RID: 4226 RVA: 0x000119DC File Offset: 0x0000FBDC
		public SocketId SocketId
		{
			set
			{
				Helper.TryMarshalSet<SocketIdInternal, SocketId>(ref this.m_SocketId, value);
			}
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x000119EB File Offset: 0x0000FBEB
		public void Set(CloseConnectionOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RemoteUserId = other.RemoteUserId;
				this.SocketId = other.SocketId;
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00011A1B File Offset: 0x0000FC1B
		public void Set(object other)
		{
			this.Set(other as CloseConnectionOptions);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00011A29 File Offset: 0x0000FC29
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RemoteUserId);
			Helper.TryMarshalDispose(ref this.m_SocketId);
		}

		// Token: 0x040007B4 RID: 1972
		private int m_ApiVersion;

		// Token: 0x040007B5 RID: 1973
		private IntPtr m_LocalUserId;

		// Token: 0x040007B6 RID: 1974
		private IntPtr m_RemoteUserId;

		// Token: 0x040007B7 RID: 1975
		private IntPtr m_SocketId;
	}
}
