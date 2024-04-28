using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000527 RID: 1319
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170009C8 RID: 2504
		// (set) Token: 0x06001FE7 RID: 8167 RVA: 0x00021C62 File Offset: 0x0001FE62
		public Credentials Credentials
		{
			set
			{
				Helper.TryMarshalSet<CredentialsInternal, Credentials>(ref this.m_Credentials, value);
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x00021C71 File Offset: 0x0001FE71
		public AuthScopeFlags ScopeFlags
		{
			set
			{
				this.m_ScopeFlags = value;
			}
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x00021C7A File Offset: 0x0001FE7A
		public void Set(LoginOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.Credentials = other.Credentials;
				this.ScopeFlags = other.ScopeFlags;
			}
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x00021C9E File Offset: 0x0001FE9E
		public void Set(object other)
		{
			this.Set(other as LoginOptions);
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x00021CAC File Offset: 0x0001FEAC
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Credentials);
		}

		// Token: 0x04000ED0 RID: 3792
		private int m_ApiVersion;

		// Token: 0x04000ED1 RID: 3793
		private IntPtr m_Credentials;

		// Token: 0x04000ED2 RID: 3794
		private AuthScopeFlags m_ScopeFlags;
	}
}
