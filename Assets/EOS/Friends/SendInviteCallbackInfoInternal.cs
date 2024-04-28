using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200042E RID: 1070
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x0001AFC5 File Offset: 0x000191C5
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x0001AFD0 File Offset: 0x000191D0
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x0001AFEC File Offset: 0x000191EC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x0001AFF4 File Offset: 0x000191F4
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x0001B010 File Offset: 0x00019210
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x04000BEB RID: 3051
		private Result m_ResultCode;

		// Token: 0x04000BEC RID: 3052
		private IntPtr m_ClientData;

		// Token: 0x04000BED RID: 3053
		private IntPtr m_LocalUserId;

		// Token: 0x04000BEE RID: 3054
		private IntPtr m_TargetUserId;
	}
}
