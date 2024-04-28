using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000263 RID: 611
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00010B98 File Offset: 0x0000ED98
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x00010BA0 File Offset: 0x0000EDA0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00010BBC File Offset: 0x0000EDBC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00010BC4 File Offset: 0x0000EDC4
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000738 RID: 1848
		private Result m_ResultCode;

		// Token: 0x04000739 RID: 1849
		private IntPtr m_ClientData;

		// Token: 0x0400073A RID: 1850
		private IntPtr m_LocalUserId;
	}
}
