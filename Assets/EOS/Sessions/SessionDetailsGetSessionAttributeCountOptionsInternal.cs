using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000111 RID: 273
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsGetSessionAttributeCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060007CE RID: 1998 RVA: 0x000086FA File Offset: 0x000068FA
		public void Set(SessionDetailsGetSessionAttributeCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00008706 File Offset: 0x00006906
		public void Set(object other)
		{
			this.Set(other as SessionDetailsGetSessionAttributeCountOptions);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040003B7 RID: 951
		private int m_ApiVersion;
	}
}
