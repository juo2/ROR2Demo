using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001D9 RID: 473
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaveRoomCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0000DB8D File Offset: 0x0000BD8D
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0000DB98 File Offset: 0x0000BD98
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0000DBBC File Offset: 0x0000BDBC
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		public string RoomName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RoomName, out result);
				return result;
			}
		}

		// Token: 0x04000604 RID: 1540
		private Result m_ResultCode;

		// Token: 0x04000605 RID: 1541
		private IntPtr m_ClientData;

		// Token: 0x04000606 RID: 1542
		private IntPtr m_LocalUserId;

		// Token: 0x04000607 RID: 1543
		private IntPtr m_RoomName;
	}
}
