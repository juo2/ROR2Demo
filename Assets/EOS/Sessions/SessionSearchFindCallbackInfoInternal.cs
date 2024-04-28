using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200012F RID: 303
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchFindCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x000094DB File Offset: 0x000076DB
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x000094E4 File Offset: 0x000076E4
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00009500 File Offset: 0x00007700
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04000413 RID: 1043
		private Result m_ResultCode;

		// Token: 0x04000414 RID: 1044
		private IntPtr m_ClientData;
	}
}
