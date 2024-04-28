using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000036 RID: 54
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoByExternalAccountCallbackInfoInternal : ICallbackInfoInternal
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000424B File Offset: 0x0000244B
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00004254 File Offset: 0x00002454
		public object ClientData
		{
			get
			{
				object result;
				Helper.TryMarshalGet(this.m_ClientData, out result);
				return result;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00004270 File Offset: 0x00002470
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00004278 File Offset: 0x00002478
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00004294 File Offset: 0x00002494
		public string ExternalAccountId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ExternalAccountId, out result);
				return result;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000042B0 File Offset: 0x000024B0
		public ExternalAccountType AccountType
		{
			get
			{
				return this.m_AccountType;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000349 RID: 841 RVA: 0x000042B8 File Offset: 0x000024B8
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out result);
				return result;
			}
		}

		// Token: 0x0400015A RID: 346
		private Result m_ResultCode;

		// Token: 0x0400015B RID: 347
		private IntPtr m_ClientData;

		// Token: 0x0400015C RID: 348
		private IntPtr m_LocalUserId;

		// Token: 0x0400015D RID: 349
		private IntPtr m_ExternalAccountId;

		// Token: 0x0400015E RID: 350
		private ExternalAccountType m_AccountType;

		// Token: 0x0400015F RID: 351
		private IntPtr m_TargetUserId;
	}
}
