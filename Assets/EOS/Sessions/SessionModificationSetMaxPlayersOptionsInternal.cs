using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000128 RID: 296
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetMaxPlayersOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001EB RID: 491
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x000091C3 File Offset: 0x000073C3
		public uint MaxPlayers
		{
			set
			{
				this.m_MaxPlayers = value;
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000091CC File Offset: 0x000073CC
		public void Set(SessionModificationSetMaxPlayersOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.MaxPlayers = other.MaxPlayers;
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000091E4 File Offset: 0x000073E4
		public void Set(object other)
		{
			this.Set(other as SessionModificationSetMaxPlayersOptions);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000401 RID: 1025
		private int m_ApiVersion;

		// Token: 0x04000402 RID: 1026
		private uint m_MaxPlayers;
	}
}
