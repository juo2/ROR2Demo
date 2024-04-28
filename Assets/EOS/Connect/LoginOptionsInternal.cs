using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004DA RID: 1242
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000935 RID: 2357
		// (set) Token: 0x06001E22 RID: 7714 RVA: 0x000200FE File Offset: 0x0001E2FE
		public Credentials Credentials
		{
			set
			{
				Helper.TryMarshalSet<CredentialsInternal, Credentials>(ref this.m_Credentials, value);
			}
		}

		// Token: 0x17000936 RID: 2358
		// (set) Token: 0x06001E23 RID: 7715 RVA: 0x0002010D File Offset: 0x0001E30D
		public UserLoginInfo UserLoginInfo
		{
			set
			{
				Helper.TryMarshalSet<UserLoginInfoInternal, UserLoginInfo>(ref this.m_UserLoginInfo, value);
			}
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0002011C File Offset: 0x0001E31C
		public void Set(LoginOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.Credentials = other.Credentials;
				this.UserLoginInfo = other.UserLoginInfo;
			}
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x00020140 File Offset: 0x0001E340
		public void Set(object other)
		{
			this.Set(other as LoginOptions);
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x0002014E File Offset: 0x0001E34E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Credentials);
			Helper.TryMarshalDispose(ref this.m_UserLoginInfo);
		}

		// Token: 0x04000E0F RID: 3599
		private int m_ApiVersion;

		// Token: 0x04000E10 RID: 3600
		private IntPtr m_Credentials;

		// Token: 0x04000E11 RID: 3601
		private IntPtr m_UserLoginInfo;
	}
}
