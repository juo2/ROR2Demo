using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004BC RID: 1212
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateDeviceIdCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x0001F59F File Offset: 0x0001D79F
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x0001F5A8 File Offset: 0x0001D7A8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x0001F5C4 File Offset: 0x0001D7C4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04000DC1 RID: 3521
		private Result m_ResultCode;

		// Token: 0x04000DC2 RID: 3522
		private IntPtr m_ClientData;
	}
}
