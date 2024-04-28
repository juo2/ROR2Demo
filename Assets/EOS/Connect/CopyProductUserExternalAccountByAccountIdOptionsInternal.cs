using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004B4 RID: 1204
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyProductUserExternalAccountByAccountIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170008DE RID: 2270
		// (set) Token: 0x06001D47 RID: 7495 RVA: 0x0001F36A File Offset: 0x0001D56A
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x170008DF RID: 2271
		// (set) Token: 0x06001D48 RID: 7496 RVA: 0x0001F379 File Offset: 0x0001D579
		public string AccountId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AccountId, value);
			}
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x0001F388 File Offset: 0x0001D588
		public void Set(CopyProductUserExternalAccountByAccountIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
				this.AccountId = other.AccountId;
			}
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x0001F3AC File Offset: 0x0001D5AC
		public void Set(object other)
		{
			this.Set(other as CopyProductUserExternalAccountByAccountIdOptions);
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x0001F3BA File Offset: 0x0001D5BA
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
			Helper.TryMarshalDispose(ref this.m_AccountId);
		}

		// Token: 0x04000DAF RID: 3503
		private int m_ApiVersion;

		// Token: 0x04000DB0 RID: 3504
		private IntPtr m_TargetUserId;

		// Token: 0x04000DB1 RID: 3505
		private IntPtr m_AccountId;
	}
}
