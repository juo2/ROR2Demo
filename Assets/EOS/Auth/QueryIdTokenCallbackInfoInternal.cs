using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000541 RID: 1345
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryIdTokenCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x000221C1 File Offset: 0x000203C1
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x000221CC File Offset: 0x000203CC
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x000221E8 File Offset: 0x000203E8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x000221F0 File Offset: 0x000203F0
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x0002220C File Offset: 0x0002040C
		public EpicAccountId TargetAccountId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetAccountId, out result);
				return result;
			}
		}

		// Token: 0x04000EF1 RID: 3825
		private Result m_ResultCode;

		// Token: 0x04000EF2 RID: 3826
		private IntPtr m_ClientData;

		// Token: 0x04000EF3 RID: 3827
		private IntPtr m_LocalUserId;

		// Token: 0x04000EF4 RID: 3828
		private IntPtr m_TargetAccountId;
	}
}
