using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000335 RID: 821
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsGetAttributeCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06001459 RID: 5209 RVA: 0x00015A91 File Offset: 0x00013C91
		public void Set(LobbyDetailsGetAttributeCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00015A9D File Offset: 0x00013C9D
		public void Set(object other)
		{
			this.Set(other as LobbyDetailsGetAttributeCountOptions);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040009B8 RID: 2488
		private int m_ApiVersion;
	}
}
