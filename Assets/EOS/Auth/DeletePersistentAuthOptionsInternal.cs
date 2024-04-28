using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200051B RID: 1307
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeletePersistentAuthOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170009A1 RID: 2465
		// (set) Token: 0x06001F95 RID: 8085 RVA: 0x000216B9 File Offset: 0x0001F8B9
		public string RefreshToken
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_RefreshToken, value);
			}
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000216C8 File Offset: 0x0001F8C8
		public void Set(DeletePersistentAuthOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.RefreshToken = other.RefreshToken;
			}
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x000216E0 File Offset: 0x0001F8E0
		public void Set(object other)
		{
			this.Set(other as DeletePersistentAuthOptions);
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x000216EE File Offset: 0x0001F8EE
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_RefreshToken);
		}

		// Token: 0x04000E9C RID: 3740
		private int m_ApiVersion;

		// Token: 0x04000E9D RID: 3741
		private IntPtr m_RefreshToken;
	}
}
