using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002B3 RID: 691
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendPacketOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000504 RID: 1284
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x00012BA9 File Offset: 0x00010DA9
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000505 RID: 1285
		// (set) Token: 0x0600118D RID: 4493 RVA: 0x00012BB8 File Offset: 0x00010DB8
		public ProductUserId RemoteUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RemoteUserId, value);
			}
		}

		// Token: 0x17000506 RID: 1286
		// (set) Token: 0x0600118E RID: 4494 RVA: 0x00012BC7 File Offset: 0x00010DC7
		public SocketId SocketId
		{
			set
			{
				Helper.TryMarshalSet<SocketIdInternal, SocketId>(ref this.m_SocketId, value);
			}
		}

		// Token: 0x17000507 RID: 1287
		// (set) Token: 0x0600118F RID: 4495 RVA: 0x00012BD6 File Offset: 0x00010DD6
		public byte Channel
		{
			set
			{
				this.m_Channel = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (set) Token: 0x06001190 RID: 4496 RVA: 0x00012BDF File Offset: 0x00010DDF
		public byte[] Data
		{
			set
			{
				Helper.TryMarshalSet<byte>(ref this.m_Data, value, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x17000509 RID: 1289
		// (set) Token: 0x06001191 RID: 4497 RVA: 0x00012BF4 File Offset: 0x00010DF4
		public bool AllowDelayedDelivery
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AllowDelayedDelivery, value);
			}
		}

		// Token: 0x1700050A RID: 1290
		// (set) Token: 0x06001192 RID: 4498 RVA: 0x00012C03 File Offset: 0x00010E03
		public PacketReliability Reliability
		{
			set
			{
				this.m_Reliability = value;
			}
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00012C0C File Offset: 0x00010E0C
		public void Set(SendPacketOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.LocalUserId;
				this.RemoteUserId = other.RemoteUserId;
				this.SocketId = other.SocketId;
				this.Channel = other.Channel;
				this.Data = other.Data;
				this.AllowDelayedDelivery = other.AllowDelayedDelivery;
				this.Reliability = other.Reliability;
			}
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00012C77 File Offset: 0x00010E77
		public void Set(object other)
		{
			this.Set(other as SendPacketOptions);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00012C85 File Offset: 0x00010E85
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_RemoteUserId);
			Helper.TryMarshalDispose(ref this.m_SocketId);
			Helper.TryMarshalDispose(ref this.m_Data);
		}

		// Token: 0x04000842 RID: 2114
		private int m_ApiVersion;

		// Token: 0x04000843 RID: 2115
		private IntPtr m_LocalUserId;

		// Token: 0x04000844 RID: 2116
		private IntPtr m_RemoteUserId;

		// Token: 0x04000845 RID: 2117
		private IntPtr m_SocketId;

		// Token: 0x04000846 RID: 2118
		private byte m_Channel;

		// Token: 0x04000847 RID: 2119
		private uint m_DataLengthBytes;

		// Token: 0x04000848 RID: 2120
		private IntPtr m_Data;

		// Token: 0x04000849 RID: 2121
		private int m_AllowDelayedDelivery;

		// Token: 0x0400084A RID: 2122
		private PacketReliability m_Reliability;
	}
}
