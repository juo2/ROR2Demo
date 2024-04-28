using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000408 RID: 1032
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateParentEmailCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x0001A36C File Offset: 0x0001856C
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x0001A374 File Offset: 0x00018574
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x0001A390 File Offset: 0x00018590
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x0001A398 File Offset: 0x00018598
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000B97 RID: 2967
		private Result m_ResultCode;

		// Token: 0x04000B98 RID: 2968
		private IntPtr m_ClientData;

		// Token: 0x04000B99 RID: 2969
		private IntPtr m_LocalUserId;
	}
}
