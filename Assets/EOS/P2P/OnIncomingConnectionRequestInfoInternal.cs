using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000298 RID: 664
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnIncomingConnectionRequestInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x00011CAC File Offset: 0x0000FEAC
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x00011CC8 File Offset: 0x0000FEC8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x00011CD0 File Offset: 0x0000FED0
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00011CEC File Offset: 0x0000FEEC
		public ProductUserId RemoteUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_RemoteUserId, out result);
				return result;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x00011D08 File Offset: 0x0000FF08
		public SocketId SocketId
		{
			get
			{
				SocketId result;
				Helper.TryMarshalGet<SocketIdInternal, SocketId>(this.m_SocketId, out result);
				return result;
			}
		}

		// Token: 0x040007DE RID: 2014
		private IntPtr m_ClientData;

		// Token: 0x040007DF RID: 2015
		private IntPtr m_LocalUserId;

		// Token: 0x040007E0 RID: 2016
		private IntPtr m_RemoteUserId;

		// Token: 0x040007E1 RID: 2017
		private IntPtr m_SocketId;
	}
}
