using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000283 RID: 643
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ClearPacketQueueOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170004A7 RID: 1191
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x000118FA File Offset: 0x0000FAFA
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (set) Token: 0x06001074 RID: 4212 RVA: 0x00011909 File Offset: 0x0000FB09
		public ProductUserId RemoteUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RemoteUserId, value);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x00011918 File Offset: 0x0000FB18
		public SocketId SocketId
		{
			set
			{
				Helper.TryMarshalSet<SocketIdInternal, SocketId>(ref this.m_SocketId, value);
			}
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00011927 File Offset: 0x0000FB27
		public void Set(ClearPacketQueueOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.RemoteUserId = other.RemoteUserId;
				this.SocketId = other.SocketId;
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00011957 File Offset: 0x0000FB57
		public void Set(object other)
		{
			this.Set(other as ClearPacketQueueOptions);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00011965 File Offset: 0x0000FB65
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RemoteUserId);
			Helper.TryMarshalDispose(ref this.m_SocketId);
		}

		// Token: 0x040007AD RID: 1965
		private int m_ApiVersion;

		// Token: 0x040007AE RID: 1966
		private IntPtr m_LocalUserId;

		// Token: 0x040007AF RID: 1967
		private IntPtr m_RemoteUserId;

		// Token: 0x040007B0 RID: 1968
		private IntPtr m_SocketId;
	}
}
