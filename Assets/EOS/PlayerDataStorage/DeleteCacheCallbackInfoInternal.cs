using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200023B RID: 571
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteCacheCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x0000FE04 File Offset: 0x0000E004
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x0000FE0C File Offset: 0x0000E00C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x0000FE28 File Offset: 0x0000E028
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0000FE30 File Offset: 0x0000E030
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x040006F2 RID: 1778
		private Result m_ResultCode;

		// Token: 0x040006F3 RID: 1779
		private IntPtr m_ClientData;

		// Token: 0x040006F4 RID: 1780
		private IntPtr m_LocalUserId;
	}
}
