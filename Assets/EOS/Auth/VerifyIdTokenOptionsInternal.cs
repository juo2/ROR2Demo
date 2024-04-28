using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000549 RID: 1353
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VerifyIdTokenOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A21 RID: 2593
		// (set) Token: 0x060020E0 RID: 8416 RVA: 0x00022A4D File Offset: 0x00020C4D
		public IdToken IdToken
		{
			set
			{
				Helper.TryMarshalSet<IdTokenInternal, IdToken>(ref this.m_IdToken, value);
			}
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x00022A5C File Offset: 0x00020C5C
		public void Set(VerifyIdTokenOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.IdToken = other.IdToken;
			}
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x00022A74 File Offset: 0x00020C74
		public void Set(object other)
		{
			this.Set(other as VerifyIdTokenOptions);
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x00022A82 File Offset: 0x00020C82
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_IdToken);
		}

		// Token: 0x04000F2A RID: 3882
		private int m_ApiVersion;

		// Token: 0x04000F2B RID: 3883
		private IntPtr m_IdToken;
	}
}
