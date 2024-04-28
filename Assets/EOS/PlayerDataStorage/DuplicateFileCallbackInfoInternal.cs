using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000243 RID: 579
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DuplicateFileCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x000100C4 File Offset: 0x0000E2C4
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x000100CC File Offset: 0x0000E2CC
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x000100E8 File Offset: 0x0000E2E8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x000100F0 File Offset: 0x0000E2F0
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000706 RID: 1798
		private Result m_ResultCode;

		// Token: 0x04000707 RID: 1799
		private IntPtr m_ClientData;

		// Token: 0x04000708 RID: 1800
		private IntPtr m_LocalUserId;
	}
}
