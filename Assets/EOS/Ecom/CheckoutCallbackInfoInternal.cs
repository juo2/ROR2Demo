using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000438 RID: 1080
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CheckoutCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x0001BDED File Offset: 0x00019FED
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x0001BDF8 File Offset: 0x00019FF8
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x0001BE14 File Offset: 0x0001A014
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x0001BE1C File Offset: 0x0001A01C
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001A66 RID: 6758 RVA: 0x0001BE38 File Offset: 0x0001A038
		public string TransactionId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_TransactionId, out result);
				return result;
			}
		}

		// Token: 0x04000C3D RID: 3133
		private Result m_ResultCode;

		// Token: 0x04000C3E RID: 3134
		private IntPtr m_ClientData;

		// Token: 0x04000C3F RID: 3135
		private IntPtr m_LocalUserId;

		// Token: 0x04000C40 RID: 3136
		private IntPtr m_TransactionId;
	}
}
