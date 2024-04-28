using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004C4 RID: 1220
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CredentialsInternal : ISettable, IDisposable
	{
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0001F7DC File Offset: 0x0001D9DC
		// (set) Token: 0x06001D96 RID: 7574 RVA: 0x0001F7F8 File Offset: 0x0001D9F8
		public string Token
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Token, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Token, value);
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0001F807 File Offset: 0x0001DA07
		// (set) Token: 0x06001D98 RID: 7576 RVA: 0x0001F80F File Offset: 0x0001DA0F
		public ExternalCredentialType Type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x0001F818 File Offset: 0x0001DA18
		public void Set(Credentials other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Token = other.Token;
				this.Type = other.Type;
			}
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x0001F83C File Offset: 0x0001DA3C
		public void Set(object other)
		{
			this.Set(other as Credentials);
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x0001F84A File Offset: 0x0001DA4A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Token);
		}

		// Token: 0x04000DD1 RID: 3537
		private int m_ApiVersion;

		// Token: 0x04000DD2 RID: 3538
		private IntPtr m_Token;

		// Token: 0x04000DD3 RID: 3539
		private ExternalCredentialType m_Type;
	}
}
