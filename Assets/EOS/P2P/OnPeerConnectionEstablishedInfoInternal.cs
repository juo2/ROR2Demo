using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A0 RID: 672
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnPeerConnectionEstablishedInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x00011FB0 File Offset: 0x000101B0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00011FCC File Offset: 0x000101CC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x00011FD4 File Offset: 0x000101D4
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00011FF0 File Offset: 0x000101F0
		public ProductUserId RemoteUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_RemoteUserId, out result);
				return result;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0001200C File Offset: 0x0001020C
		public SocketId SocketId
		{
			get
			{
				SocketId result;
				Helper.TryMarshalGet<SocketIdInternal, SocketId>(this.m_SocketId, out result);
				return result;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x00012028 File Offset: 0x00010228
		public ConnectionEstablishedType ConnectionType
		{
			get
			{
				return this.m_ConnectionType;
			}
		}

		// Token: 0x040007F3 RID: 2035
		private IntPtr m_ClientData;

		// Token: 0x040007F4 RID: 2036
		private IntPtr m_LocalUserId;

		// Token: 0x040007F5 RID: 2037
		private IntPtr m_RemoteUserId;

		// Token: 0x040007F6 RID: 2038
		private IntPtr m_SocketId;

		// Token: 0x040007F7 RID: 2039
		private ConnectionEstablishedType m_ConnectionType;
	}
}
