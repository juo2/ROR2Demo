using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200010B RID: 267
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsCopyInfoOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060007BC RID: 1980 RVA: 0x0000864C File Offset: 0x0000684C
		public void Set(SessionDetailsCopyInfoOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00008658 File Offset: 0x00006858
		public void Set(object other)
		{
			this.Set(other as SessionDetailsCopyInfoOptions);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040003B0 RID: 944
		private int m_ApiVersion;
	}
}
