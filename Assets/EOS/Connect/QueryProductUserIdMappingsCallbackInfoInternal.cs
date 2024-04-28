using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004FA RID: 1274
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryProductUserIdMappingsCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06001EBD RID: 7869 RVA: 0x000204E8 File Offset: 0x0001E6E8
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x000204F0 File Offset: 0x0001E6F0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x0002050C File Offset: 0x0001E70C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x00020514 File Offset: 0x0001E714
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000E2B RID: 3627
		private Result m_ResultCode;

		// Token: 0x04000E2C RID: 3628
		private IntPtr m_ClientData;

		// Token: 0x04000E2D RID: 3629
		private IntPtr m_LocalUserId;
	}
}
