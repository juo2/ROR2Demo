using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200047F RID: 1151
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOffersCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x0001D990 File Offset: 0x0001BB90
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x0001D998 File Offset: 0x0001BB98
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0001D9B4 File Offset: 0x0001BBB4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x0001D9BC File Offset: 0x0001BBBC
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000D11 RID: 3345
		private Result m_ResultCode;

		// Token: 0x04000D12 RID: 3346
		private IntPtr m_ClientData;

		// Token: 0x04000D13 RID: 3347
		private IntPtr m_LocalUserId;
	}
}
