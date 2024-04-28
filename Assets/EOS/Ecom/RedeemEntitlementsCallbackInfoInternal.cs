using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200048B RID: 1163
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RedeemEntitlementsCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x0001DF18 File Offset: 0x0001C118
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x0001DF20 File Offset: 0x0001C120
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x0001DF3C File Offset: 0x0001C13C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x0001DF44 File Offset: 0x0001C144
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000D3D RID: 3389
		private Result m_ResultCode;

		// Token: 0x04000D3E RID: 3390
		private IntPtr m_ClientData;

		// Token: 0x04000D3F RID: 3391
		private IntPtr m_LocalUserId;
	}
}
