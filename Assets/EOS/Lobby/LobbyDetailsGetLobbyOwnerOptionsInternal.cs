using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000337 RID: 823
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsGetLobbyOwnerOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x0600145D RID: 5213 RVA: 0x00015AAB File Offset: 0x00013CAB
		public void Set(LobbyDetailsGetLobbyOwnerOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00015AB7 File Offset: 0x00013CB7
		public void Set(object other)
		{
			this.Set(other as LobbyDetailsGetLobbyOwnerOptions);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040009B9 RID: 2489
		private int m_ApiVersion;
	}
}
