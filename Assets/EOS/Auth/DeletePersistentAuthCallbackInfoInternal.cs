using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000519 RID: 1305
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeletePersistentAuthCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06001F8F RID: 8079 RVA: 0x0002167B File Offset: 0x0001F87B
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x00021684 File Offset: 0x0001F884
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06001F91 RID: 8081 RVA: 0x000216A0 File Offset: 0x0001F8A0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04000E99 RID: 3737
		private Result m_ResultCode;

		// Token: 0x04000E9A RID: 3738
		private IntPtr m_ClientData;
	}
}
