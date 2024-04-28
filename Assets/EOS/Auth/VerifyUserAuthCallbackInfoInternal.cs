using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200054B RID: 1355
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VerifyUserAuthCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x00022B13 File Offset: 0x00020D13
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x00022B1C File Offset: 0x00020D1C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060020EE RID: 8430 RVA: 0x00022B38 File Offset: 0x00020D38
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04000F2E RID: 3886
		private Result m_ResultCode;

		// Token: 0x04000F2F RID: 3887
		private IntPtr m_ClientData;
	}
}
