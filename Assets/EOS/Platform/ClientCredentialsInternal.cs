using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005DF RID: 1503
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ClientCredentialsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x00026A08 File Offset: 0x00024C08
		// (set) Token: 0x06002471 RID: 9329 RVA: 0x00026A24 File Offset: 0x00024C24
		public string ClientId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ClientId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ClientId, value);
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x00026A34 File Offset: 0x00024C34
		// (set) Token: 0x06002473 RID: 9331 RVA: 0x00026A50 File Offset: 0x00024C50
		public string ClientSecret
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ClientSecret, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ClientSecret, value);
			}
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x00026A5F File Offset: 0x00024C5F
		public void Set(ClientCredentials other)
		{
			if (other != null)
			{
				this.ClientId = other.ClientId;
				this.ClientSecret = other.ClientSecret;
			}
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x00026A7C File Offset: 0x00024C7C
		public void Set(object other)
		{
			this.Set(other as ClientCredentials);
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x00026A8A File Offset: 0x00024C8A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ClientId);
			Helper.TryMarshalDispose(ref this.m_ClientSecret);
		}

		// Token: 0x0400112F RID: 4399
		private IntPtr m_ClientId;

		// Token: 0x04001130 RID: 4400
		private IntPtr m_ClientSecret;
	}
}
