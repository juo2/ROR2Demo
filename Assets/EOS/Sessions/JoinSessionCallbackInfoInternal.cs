using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D7 RID: 215
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinSessionCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00007D5B File Offset: 0x00005F5B
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00007D64 File Offset: 0x00005F64
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00007D80 File Offset: 0x00005F80
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x0400035C RID: 860
		private Result m_ResultCode;

		// Token: 0x0400035D RID: 861
		private IntPtr m_ClientData;
	}
}
