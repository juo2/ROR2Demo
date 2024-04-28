using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200054D RID: 1357
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VerifyUserAuthOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A28 RID: 2600
		// (set) Token: 0x060020F2 RID: 8434 RVA: 0x00022B51 File Offset: 0x00020D51
		public Token AuthToken
		{
			set
			{
				Helper.TryMarshalSet<TokenInternal, Token>(ref this.m_AuthToken, value);
			}
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x00022B60 File Offset: 0x00020D60
		public void Set(VerifyUserAuthOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.AuthToken = other.AuthToken;
			}
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x00022B78 File Offset: 0x00020D78
		public void Set(object other)
		{
			this.Set(other as VerifyUserAuthOptions);
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x00022B86 File Offset: 0x00020D86
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_AuthToken);
		}

		// Token: 0x04000F31 RID: 3889
		private int m_ApiVersion;

		// Token: 0x04000F32 RID: 3890
		private IntPtr m_AuthToken;
	}
}
