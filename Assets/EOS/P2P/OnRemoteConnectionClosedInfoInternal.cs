using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A8 RID: 680
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnRemoteConnectionClosedInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x00012210 File Offset: 0x00010410
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x0001222C File Offset: 0x0001042C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x00012234 File Offset: 0x00010434
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x00012250 File Offset: 0x00010450
		public ProductUserId RemoteUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_RemoteUserId, out result);
				return result;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0001226C File Offset: 0x0001046C
		public SocketId SocketId
		{
			get
			{
				SocketId result;
				Helper.TryMarshalGet<SocketIdInternal, SocketId>(this.m_SocketId, out result);
				return result;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x00012288 File Offset: 0x00010488
		public ConnectionClosedReason Reason
		{
			get
			{
				return this.m_Reason;
			}
		}

		// Token: 0x04000803 RID: 2051
		private IntPtr m_ClientData;

		// Token: 0x04000804 RID: 2052
		private IntPtr m_LocalUserId;

		// Token: 0x04000805 RID: 2053
		private IntPtr m_RemoteUserId;

		// Token: 0x04000806 RID: 2054
		private IntPtr m_SocketId;

		// Token: 0x04000807 RID: 2055
		private ConnectionClosedReason m_Reason;
	}
}
