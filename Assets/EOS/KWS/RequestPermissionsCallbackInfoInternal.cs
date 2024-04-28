using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000404 RID: 1028
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RequestPermissionsCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x0001A1E8 File Offset: 0x000183E8
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0001A1F0 File Offset: 0x000183F0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x0001A20C File Offset: 0x0001840C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0001A214 File Offset: 0x00018414
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000B8B RID: 2955
		private Result m_ResultCode;

		// Token: 0x04000B8C RID: 2956
		private IntPtr m_ClientData;

		// Token: 0x04000B8D RID: 2957
		private IntPtr m_LocalUserId;
	}
}
