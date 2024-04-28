using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200017F RID: 383
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioOutputStateCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x0000B904 File Offset: 0x00009B04
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0000B90C File Offset: 0x00009B0C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0000B928 File Offset: 0x00009B28
		public string RoomName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RoomName, out result);
				return result;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0000B944 File Offset: 0x00009B44
		public RTCAudioOutputStatus Status
		{
			get
			{
				return this.m_Status;
			}
		}

		// Token: 0x040004FF RID: 1279
		private IntPtr m_ClientData;

		// Token: 0x04000500 RID: 1280
		private IntPtr m_LocalUserId;

		// Token: 0x04000501 RID: 1281
		private IntPtr m_RoomName;

		// Token: 0x04000502 RID: 1282
		private RTCAudioOutputStatus m_Status;
	}
}
