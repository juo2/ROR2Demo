using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000529 RID: 1321
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginStatusChangedCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06001FF8 RID: 8184 RVA: 0x00021D98 File Offset: 0x0001FF98
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x00021DB4 File Offset: 0x0001FFB4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06001FFA RID: 8186 RVA: 0x00021DBC File Offset: 0x0001FFBC
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x00021DD8 File Offset: 0x0001FFD8
		public LoginStatus PrevStatus
		{
			get
			{
				return this.m_PrevStatus;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06001FFC RID: 8188 RVA: 0x00021DE0 File Offset: 0x0001FFE0
		public LoginStatus CurrentStatus
		{
			get
			{
				return this.m_CurrentStatus;
			}
		}

		// Token: 0x04000ED7 RID: 3799
		private IntPtr m_ClientData;

		// Token: 0x04000ED8 RID: 3800
		private IntPtr m_LocalUserId;

		// Token: 0x04000ED9 RID: 3801
		private LoginStatus m_PrevStatus;

		// Token: 0x04000EDA RID: 3802
		private LoginStatus m_CurrentStatus;
	}
}
