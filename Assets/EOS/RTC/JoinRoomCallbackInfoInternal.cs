using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001D4 RID: 468
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinRoomCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0000D899 File Offset: 0x0000BA99
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0000D8C0 File Offset: 0x0000BAC0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0000D8E4 File Offset: 0x0000BAE4
		public string RoomName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RoomName, out result);
				return result;
			}
		}

		// Token: 0x040005E8 RID: 1512
		private Result m_ResultCode;

		// Token: 0x040005E9 RID: 1513
		private IntPtr m_ClientData;

		// Token: 0x040005EA RID: 1514
		private IntPtr m_LocalUserId;

		// Token: 0x040005EB RID: 1515
		private IntPtr m_RoomName;
	}
}
