using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004D8 RID: 1240
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x00020075 File Offset: 0x0001E275
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x00020080 File Offset: 0x0001E280
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06001E1A RID: 7706 RVA: 0x0002009C File Offset: 0x0001E29C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x000200A4 File Offset: 0x0001E2A4
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06001E1C RID: 7708 RVA: 0x000200C0 File Offset: 0x0001E2C0
		public ContinuanceToken ContinuanceToken
		{
			get
			{
				ContinuanceToken result;
				Helper.TryMarshalGet<ContinuanceToken>(this.m_ContinuanceToken, out result);
				return result;
			}
		}

		// Token: 0x04000E09 RID: 3593
		private Result m_ResultCode;

		// Token: 0x04000E0A RID: 3594
		private IntPtr m_ClientData;

		// Token: 0x04000E0B RID: 3595
		private IntPtr m_LocalUserId;

		// Token: 0x04000E0C RID: 3596
		private IntPtr m_ContinuanceToken;
	}
}
