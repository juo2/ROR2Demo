using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004CC RID: 1228
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetExternalAccountMappingsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000910 RID: 2320
		// (set) Token: 0x06001DCC RID: 7628 RVA: 0x0001FB86 File Offset: 0x0001DD86
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000911 RID: 2321
		// (set) Token: 0x06001DCD RID: 7629 RVA: 0x0001FB95 File Offset: 0x0001DD95
		public ExternalAccountType AccountIdType
		{
			set
			{
				this.m_AccountIdType = value;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (set) Token: 0x06001DCE RID: 7630 RVA: 0x0001FB9E File Offset: 0x0001DD9E
		public string TargetExternalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetExternalUserId, value);
			}
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x0001FBAD File Offset: 0x0001DDAD
		public void Set(GetExternalAccountMappingsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.AccountIdType = other.AccountIdType;
				this.TargetExternalUserId = other.TargetExternalUserId;
			}
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0001FBDD File Offset: 0x0001DDDD
		public void Set(object other)
		{
			this.Set(other as GetExternalAccountMappingsOptions);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0001FBEB File Offset: 0x0001DDEB
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetExternalUserId);
		}

		// Token: 0x04000DE7 RID: 3559
		private int m_ApiVersion;

		// Token: 0x04000DE8 RID: 3560
		private IntPtr m_LocalUserId;

		// Token: 0x04000DE9 RID: 3561
		private ExternalAccountType m_AccountIdType;

		// Token: 0x04000DEA RID: 3562
		private IntPtr m_TargetExternalUserId;
	}
}
