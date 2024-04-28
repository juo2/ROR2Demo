using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200052D RID: 1325
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogoutOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170009DB RID: 2523
		// (set) Token: 0x0600200E RID: 8206 RVA: 0x00021EE9 File Offset: 0x000200E9
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00021EF8 File Offset: 0x000200F8
		public void Set(LogoutOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x00021F10 File Offset: 0x00020110
		public void Set(object other)
		{
			this.Set(other as LogoutOptions);
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x00021F1E File Offset: 0x0002011E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000EE2 RID: 3810
		private int m_ApiVersion;

		// Token: 0x04000EE3 RID: 3811
		private IntPtr m_LocalUserId;
	}
}
