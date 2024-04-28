using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Reports
{
	// Token: 0x02000161 RID: 353
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendPlayerBehaviorReportCompleteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0000AAC3 File Offset: 0x00008CC3
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0000AACC File Offset: 0x00008CCC
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x040004A0 RID: 1184
		private Result m_ResultCode;

		// Token: 0x040004A1 RID: 1185
		private IntPtr m_ClientData;
	}
}
