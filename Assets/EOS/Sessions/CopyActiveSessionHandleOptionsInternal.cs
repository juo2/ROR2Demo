using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000B9 RID: 185
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyActiveSessionHandleOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700013D RID: 317
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x00007531 File Offset: 0x00005731
		public string SessionName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionName, value);
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00007540 File Offset: 0x00005740
		public void Set(CopyActiveSessionHandleOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.SessionName;
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00007558 File Offset: 0x00005758
		public void Set(object other)
		{
			this.Set(other as CopyActiveSessionHandleOptions);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00007566 File Offset: 0x00005766
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionName);
		}

		// Token: 0x04000319 RID: 793
		private int m_ApiVersion;

		// Token: 0x0400031A RID: 794
		private IntPtr m_SessionName;
	}
}
