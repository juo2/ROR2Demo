using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200042A RID: 1066
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x0001AE01 File Offset: 0x00019001
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x0001AE0C File Offset: 0x0001900C
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x0001AE28 File Offset: 0x00019028
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x0001AE30 File Offset: 0x00019030
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x0001AE4C File Offset: 0x0001904C
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x04000BDE RID: 3038
		private Result m_ResultCode;

		// Token: 0x04000BDF RID: 3039
		private IntPtr m_ClientData;

		// Token: 0x04000BE0 RID: 3040
		private IntPtr m_LocalUserId;

		// Token: 0x04000BE1 RID: 3041
		private IntPtr m_TargetUserId;
	}
}
