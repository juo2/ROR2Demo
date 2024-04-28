using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200050A RID: 1290
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VerifyIdTokenOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700098C RID: 2444
		// (set) Token: 0x06001F34 RID: 7988 RVA: 0x00020CD5 File Offset: 0x0001EED5
		public IdToken IdToken
		{
			set
			{
				Helper.TryMarshalSet<IdTokenInternal, IdToken>(ref this.m_IdToken, value);
			}
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x00020CE4 File Offset: 0x0001EEE4
		public void Set(VerifyIdTokenOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.IdToken = other.IdToken;
			}
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00020CFC File Offset: 0x0001EEFC
		public void Set(object other)
		{
			this.Set(other as VerifyIdTokenOptions);
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x00020D0A File Offset: 0x0001EF0A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_IdToken);
		}

		// Token: 0x04000E68 RID: 3688
		private int m_ApiVersion;

		// Token: 0x04000E69 RID: 3689
		private IntPtr m_IdToken;
	}
}
