using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000022 RID: 34
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyExternalUserInfoByAccountTypeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000018 RID: 24
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x00003B82 File Offset: 0x00001D82
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00003B91 File Offset: 0x00001D91
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public ExternalAccountType AccountType
		{
			set
			{
				this.m_AccountType = value;
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00003BA9 File Offset: 0x00001DA9
		public void Set(CopyExternalUserInfoByAccountTypeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
				this.AccountType = other.AccountType;
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00003BD9 File Offset: 0x00001DD9
		public void Set(object other)
		{
			this.Set(other as CopyExternalUserInfoByAccountTypeOptions);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00003BE7 File Offset: 0x00001DE7
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000129 RID: 297
		private int m_ApiVersion;

		// Token: 0x0400012A RID: 298
		private IntPtr m_LocalUserId;

		// Token: 0x0400012B RID: 299
		private IntPtr m_TargetUserId;

		// Token: 0x0400012C RID: 300
		private ExternalAccountType m_AccountType;
	}
}
