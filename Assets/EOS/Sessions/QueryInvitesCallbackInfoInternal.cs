using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000F7 RID: 247
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryInvitesCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00007F20 File Offset: 0x00006120
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00007F28 File Offset: 0x00006128
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00007F44 File Offset: 0x00006144
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00007F4C File Offset: 0x0000614C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000377 RID: 887
		private Result m_ResultCode;

		// Token: 0x04000378 RID: 888
		private IntPtr m_ClientData;

		// Token: 0x04000379 RID: 889
		private IntPtr m_LocalUserId;
	}
}
