using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000049 RID: 73
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HideFriendsCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00004BFC File Offset: 0x00002DFC
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00004C18 File Offset: 0x00002E18
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00004C20 File Offset: 0x00002E20
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000197 RID: 407
		private Result m_ResultCode;

		// Token: 0x04000198 RID: 408
		private IntPtr m_ClientData;

		// Token: 0x04000199 RID: 409
		private IntPtr m_LocalUserId;
	}
}
