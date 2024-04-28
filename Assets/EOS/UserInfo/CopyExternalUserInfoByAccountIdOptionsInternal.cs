using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000020 RID: 32
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyExternalUserInfoByAccountIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000012 RID: 18
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x00003ABE File Offset: 0x00001CBE
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x00003ACD File Offset: 0x00001CCD
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x17000014 RID: 20
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x00003ADC File Offset: 0x00001CDC
		public string AccountId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AccountId, value);
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00003AEB File Offset: 0x00001CEB
		public void Set(CopyExternalUserInfoByAccountIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
				this.AccountId = other.AccountId;
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00003B1B File Offset: 0x00001D1B
		public void Set(object other)
		{
			this.Set(other as CopyExternalUserInfoByAccountIdOptions);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00003B29 File Offset: 0x00001D29
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
			Helper.TryMarshalDispose(ref this.m_AccountId);
		}

		// Token: 0x04000122 RID: 290
		private int m_ApiVersion;

		// Token: 0x04000123 RID: 291
		private IntPtr m_LocalUserId;

		// Token: 0x04000124 RID: 292
		private IntPtr m_TargetUserId;

		// Token: 0x04000125 RID: 293
		private IntPtr m_AccountId;
	}
}
