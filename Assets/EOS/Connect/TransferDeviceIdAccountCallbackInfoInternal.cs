using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004FE RID: 1278
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct TransferDeviceIdAccountCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x00020690 File Offset: 0x0001E890
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x00020698 File Offset: 0x0001E898
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x000206B4 File Offset: 0x0001E8B4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x000206BC File Offset: 0x0001E8BC
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000E39 RID: 3641
		private Result m_ResultCode;

		// Token: 0x04000E3A RID: 3642
		private IntPtr m_ClientData;

		// Token: 0x04000E3B RID: 3643
		private IntPtr m_LocalUserId;
	}
}
