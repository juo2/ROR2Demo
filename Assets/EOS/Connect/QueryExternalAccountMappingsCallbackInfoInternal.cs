using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004F6 RID: 1270
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryExternalAccountMappingsCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x0002033C File Offset: 0x0001E53C
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x00020344 File Offset: 0x0001E544
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x00020360 File Offset: 0x0001E560
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x00020368 File Offset: 0x0001E568
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000E1D RID: 3613
		private Result m_ResultCode;

		// Token: 0x04000E1E RID: 3614
		private IntPtr m_ClientData;

		// Token: 0x04000E1F RID: 3615
		private IntPtr m_LocalUserId;
	}
}
