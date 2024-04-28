using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004C6 RID: 1222
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteDeviceIdCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x0001F8DB File Offset: 0x0001DADB
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x0001F8E4 File Offset: 0x0001DAE4
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x0001F900 File Offset: 0x0001DB00
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04000DD6 RID: 3542
		private Result m_ResultCode;

		// Token: 0x04000DD7 RID: 3543
		private IntPtr m_ClientData;
	}
}
