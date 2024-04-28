using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200047B RID: 1147
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryEntitlementsCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x0001D7E0 File Offset: 0x0001B9E0
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x0001D7E8 File Offset: 0x0001B9E8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x0001D804 File Offset: 0x0001BA04
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x0001D80C File Offset: 0x0001BA0C
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000D03 RID: 3331
		private Result m_ResultCode;

		// Token: 0x04000D04 RID: 3332
		private IntPtr m_ClientData;

		// Token: 0x04000D05 RID: 3333
		private IntPtr m_LocalUserId;
	}
}
