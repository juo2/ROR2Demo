using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000553 RID: 1363
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IOSLoginOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A37 RID: 2615
		// (set) Token: 0x0600211F RID: 8479 RVA: 0x00022E61 File Offset: 0x00021061
		public IOSCredentials Credentials
		{
			set
			{
				Helper.TryMarshalSet<IOSCredentialsInternal, IOSCredentials>(ref this.m_Credentials, value);
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (set) Token: 0x06002120 RID: 8480 RVA: 0x00022E70 File Offset: 0x00021070
		public AuthScopeFlags ScopeFlags
		{
			set
			{
				this.m_ScopeFlags = value;
			}
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x00022E79 File Offset: 0x00021079
		public void Set(IOSLoginOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.Credentials = other.Credentials;
				this.ScopeFlags = other.ScopeFlags;
			}
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x00022E9D File Offset: 0x0002109D
		public void Set(object other)
		{
			this.Set(other as IOSLoginOptions);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x00022EAB File Offset: 0x000210AB
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Credentials);
		}

		// Token: 0x04000F43 RID: 3907
		private int m_ApiVersion;

		// Token: 0x04000F44 RID: 3908
		private IntPtr m_Credentials;

		// Token: 0x04000F45 RID: 3909
		private AuthScopeFlags m_ScopeFlags;
	}
}
