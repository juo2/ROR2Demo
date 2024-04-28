using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004D4 RID: 1236
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LinkAccountCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06001DFE RID: 7678 RVA: 0x0001FED0 File Offset: 0x0001E0D0
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x0001FED8 File Offset: 0x0001E0D8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x0001FEF4 File Offset: 0x0001E0F4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x0001FEFC File Offset: 0x0001E0FC
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000DFD RID: 3581
		private Result m_ResultCode;

		// Token: 0x04000DFE RID: 3582
		private IntPtr m_ClientData;

		// Token: 0x04000DFF RID: 3583
		private IntPtr m_LocalUserId;
	}
}
