using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200032F RID: 815
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsCopyInfoOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06001441 RID: 5185 RVA: 0x00015971 File Offset: 0x00013B71
		public void Set(LobbyDetailsCopyInfoOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0001597D File Offset: 0x00013B7D
		public void Set(object other)
		{
			this.Set(other as LobbyDetailsCopyInfoOptions);
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040009AD RID: 2477
		private int m_ApiVersion;
	}
}
