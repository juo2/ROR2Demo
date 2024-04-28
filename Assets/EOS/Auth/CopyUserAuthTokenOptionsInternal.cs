using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000515 RID: 1301
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUserAuthTokenOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06001F6A RID: 8042 RVA: 0x000213E0 File Offset: 0x0001F5E0
		public void Set(CopyUserAuthTokenOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x000213EC File Offset: 0x0001F5EC
		public void Set(object other)
		{
			this.Set(other as CopyUserAuthTokenOptions);
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000E8B RID: 3723
		private int m_ApiVersion;
	}
}
