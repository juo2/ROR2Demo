using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200052B RID: 1323
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogoutCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x00021E90 File Offset: 0x00020090
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x00021E98 File Offset: 0x00020098
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x00021EB4 File Offset: 0x000200B4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x0600200A RID: 8202 RVA: 0x00021EBC File Offset: 0x000200BC
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x04000EDE RID: 3806
		private Result m_ResultCode;

		// Token: 0x04000EDF RID: 3807
		private IntPtr m_ClientData;

		// Token: 0x04000EE0 RID: 3808
		private IntPtr m_LocalUserId;
	}
}
