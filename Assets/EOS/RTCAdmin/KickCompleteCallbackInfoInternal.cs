using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001B5 RID: 437
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct KickCompleteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0000CB13 File Offset: 0x0000AD13
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0000CB38 File Offset: 0x0000AD38
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x0400058A RID: 1418
		private Result m_ResultCode;

		// Token: 0x0400058B RID: 1419
		private IntPtr m_ClientData;
	}
}
