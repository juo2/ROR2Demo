using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200007E RID: 126
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00005900 File Offset: 0x00003B00
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00005908 File Offset: 0x00003B08
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00005924 File Offset: 0x00003B24
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000592C File Offset: 0x00003B2C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000265 RID: 613
		private Result m_ResultCode;

		// Token: 0x04000266 RID: 614
		private IntPtr m_ClientData;

		// Token: 0x04000267 RID: 615
		private IntPtr m_LocalUserId;
	}
}
