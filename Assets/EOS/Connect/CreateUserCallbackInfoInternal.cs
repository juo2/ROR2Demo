using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004C0 RID: 1216
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateUserCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x0001F6C8 File Offset: 0x0001D8C8
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x0001F6D0 File Offset: 0x0001D8D0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x0001F6EC File Offset: 0x0001D8EC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x0001F6F4 File Offset: 0x0001D8F4
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000DC9 RID: 3529
		private Result m_ResultCode;

		// Token: 0x04000DCA RID: 3530
		private IntPtr m_ClientData;

		// Token: 0x04000DCB RID: 3531
		private IntPtr m_LocalUserId;
	}
}
