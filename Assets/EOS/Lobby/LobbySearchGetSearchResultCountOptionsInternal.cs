using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000364 RID: 868
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchGetSearchResultCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06001583 RID: 5507 RVA: 0x00017690 File Offset: 0x00015890
		public void Set(LobbySearchGetSearchResultCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0001769C File Offset: 0x0001589C
		public void Set(object other)
		{
			this.Set(other as LobbySearchGetSearchResultCountOptions);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000A5D RID: 2653
		private int m_ApiVersion;
	}
}
