using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001D2 RID: 466
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DisconnectedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0000D761 File Offset: 0x0000B961
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0000D76C File Offset: 0x0000B96C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x0000D788 File Offset: 0x0000B988
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0000D790 File Offset: 0x0000B990
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		public string RoomName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RoomName, out result);
				return result;
			}
		}

		// Token: 0x040005E0 RID: 1504
		private Result m_ResultCode;

		// Token: 0x040005E1 RID: 1505
		private IntPtr m_ClientData;

		// Token: 0x040005E2 RID: 1506
		private IntPtr m_LocalUserId;

		// Token: 0x040005E3 RID: 1507
		private IntPtr m_RoomName;
	}
}
