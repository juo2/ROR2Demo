using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000502 RID: 1282
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnlinkAccountCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x00020844 File Offset: 0x0001EA44
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x0002084C File Offset: 0x0001EA4C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x00020868 File Offset: 0x0001EA68
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x00020870 File Offset: 0x0001EA70
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000E46 RID: 3654
		private Result m_ResultCode;

		// Token: 0x04000E47 RID: 3655
		private IntPtr m_ClientData;

		// Token: 0x04000E48 RID: 3656
		private IntPtr m_LocalUserId;
	}
}
