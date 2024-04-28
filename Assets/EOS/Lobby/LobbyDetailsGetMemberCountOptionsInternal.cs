using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200033D RID: 829
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsGetMemberCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x0600146F RID: 5231 RVA: 0x00015B59 File Offset: 0x00013D59
		public void Set(LobbyDetailsGetMemberCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x00015B65 File Offset: 0x00013D65
		public void Set(object other)
		{
			this.Set(other as LobbyDetailsGetMemberCountOptions);
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040009C0 RID: 2496
		private int m_ApiVersion;
	}
}
