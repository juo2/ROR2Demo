using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200039C RID: 924
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryInvitesCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x00017D54 File Offset: 0x00015F54
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x00017D5C File Offset: 0x00015F5C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x00017D78 File Offset: 0x00015F78
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x00017D80 File Offset: 0x00015F80
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000A8E RID: 2702
		private Result m_ResultCode;

		// Token: 0x04000A8F RID: 2703
		private IntPtr m_ClientData;

		// Token: 0x04000A90 RID: 2704
		private IntPtr m_LocalUserId;
	}
}
