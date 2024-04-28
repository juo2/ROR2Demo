using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200035E RID: 862
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchCopySearchResultByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000657 RID: 1623
		// (set) Token: 0x0600156C RID: 5484 RVA: 0x0001755D File Offset: 0x0001575D
		public uint LobbyIndex
		{
			set
			{
				this.m_LobbyIndex = value;
			}
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00017566 File Offset: 0x00015766
		public void Set(LobbySearchCopySearchResultByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LobbyIndex = other.LobbyIndex;
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0001757E File Offset: 0x0001577E
		public void Set(object other)
		{
			this.Set(other as LobbySearchCopySearchResultByIndexOptions);
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000A54 RID: 2644
		private int m_ApiVersion;

		// Token: 0x04000A55 RID: 2645
		private uint m_LobbyIndex;
	}
}
