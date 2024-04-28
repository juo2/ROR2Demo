using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004AF RID: 1199
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AuthExpirationCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x0001EA7C File Offset: 0x0001CC7C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x0001EA98 File Offset: 0x0001CC98
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x0001EAA0 File Offset: 0x0001CCA0
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000D89 RID: 3465
		private IntPtr m_ClientData;

		// Token: 0x04000D8A RID: 3466
		private IntPtr m_LocalUserId;
	}
}
