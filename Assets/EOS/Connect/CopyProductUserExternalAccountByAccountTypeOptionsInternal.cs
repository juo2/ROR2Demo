using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004B6 RID: 1206
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyProductUserExternalAccountByAccountTypeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170008E2 RID: 2274
		// (set) Token: 0x06001D51 RID: 7505 RVA: 0x0001F3F6 File Offset: 0x0001D5F6
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x0001F405 File Offset: 0x0001D605
		public ExternalAccountType AccountIdType
		{
			set
			{
				this.m_AccountIdType = value;
			}
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0001F40E File Offset: 0x0001D60E
		public void Set(CopyProductUserExternalAccountByAccountTypeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
				this.AccountIdType = other.AccountIdType;
			}
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0001F432 File Offset: 0x0001D632
		public void Set(object other)
		{
			this.Set(other as CopyProductUserExternalAccountByAccountTypeOptions);
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0001F440 File Offset: 0x0001D640
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000DB4 RID: 3508
		private int m_ApiVersion;

		// Token: 0x04000DB5 RID: 3509
		private IntPtr m_TargetUserId;

		// Token: 0x04000DB6 RID: 3510
		private ExternalAccountType m_AccountIdType;
	}
}
