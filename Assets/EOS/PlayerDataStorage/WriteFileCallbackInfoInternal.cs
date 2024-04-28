using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000272 RID: 626
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct WriteFileCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00011329 File Offset: 0x0000F529
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x00011334 File Offset: 0x0000F534
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x00011350 File Offset: 0x0000F550
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x00011358 File Offset: 0x0000F558
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00011374 File Offset: 0x0000F574
		public string Filename
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Filename, out result);
				return result;
			}
		}

		// Token: 0x04000775 RID: 1909
		private Result m_ResultCode;

		// Token: 0x04000776 RID: 1910
		private IntPtr m_ClientData;

		// Token: 0x04000777 RID: 1911
		private IntPtr m_LocalUserId;

		// Token: 0x04000778 RID: 1912
		private IntPtr m_Filename;
	}
}
