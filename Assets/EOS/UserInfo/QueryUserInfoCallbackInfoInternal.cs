using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200003A RID: 58
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00004455 File Offset: 0x00002655
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00004460 File Offset: 0x00002660
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000447C File Offset: 0x0000267C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00004484 File Offset: 0x00002684
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000044A0 File Offset: 0x000026A0
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x0400016B RID: 363
		private Result m_ResultCode;

		// Token: 0x0400016C RID: 364
		private IntPtr m_ClientData;

		// Token: 0x0400016D RID: 365
		private IntPtr m_LocalUserId;

		// Token: 0x0400016E RID: 366
		private IntPtr m_TargetUserId;
	}
}
