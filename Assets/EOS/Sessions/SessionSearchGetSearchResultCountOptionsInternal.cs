using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000133 RID: 307
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchGetSearchResultCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06000896 RID: 2198 RVA: 0x0000955C File Offset: 0x0000775C
		public void Set(SessionSearchGetSearchResultCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00009568 File Offset: 0x00007768
		public void Set(object other)
		{
			this.Set(other as SessionSearchGetSearchResultCountOptions);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000418 RID: 1048
		private int m_ApiVersion;
	}
}
