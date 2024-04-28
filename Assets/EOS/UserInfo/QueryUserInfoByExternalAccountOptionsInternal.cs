using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000038 RID: 56
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoByExternalAccountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700004E RID: 78
		// (set) Token: 0x06000351 RID: 849 RVA: 0x00004307 File Offset: 0x00002507
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00004316 File Offset: 0x00002516
		public string ExternalAccountId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ExternalAccountId, value);
			}
		}

		// Token: 0x17000050 RID: 80
		// (set) Token: 0x06000353 RID: 851 RVA: 0x00004325 File Offset: 0x00002525
		public ExternalAccountType AccountType
		{
			set
			{
				this.m_AccountType = value;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000432E File Offset: 0x0000252E
		public void Set(QueryUserInfoByExternalAccountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.ExternalAccountId = other.ExternalAccountId;
				this.AccountType = other.AccountType;
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000435E File Offset: 0x0000255E
		public void Set(object other)
		{
			this.Set(other as QueryUserInfoByExternalAccountOptions);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000436C File Offset: 0x0000256C
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_ExternalAccountId);
		}

		// Token: 0x04000163 RID: 355
		private int m_ApiVersion;

		// Token: 0x04000164 RID: 356
		private IntPtr m_LocalUserId;

		// Token: 0x04000165 RID: 357
		private IntPtr m_ExternalAccountId;

		// Token: 0x04000166 RID: 358
		private ExternalAccountType m_AccountType;
	}
}
