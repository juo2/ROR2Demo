using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020002F9 RID: 761
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyUpdateReceivedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060012F5 RID: 4853 RVA: 0x000142B5 File Offset: 0x000124B5
		public void Set(AddNotifyLobbyUpdateReceivedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000142C1 File Offset: 0x000124C1
		public void Set(object other)
		{
			this.Set(other as AddNotifyLobbyUpdateReceivedOptions);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400090C RID: 2316
		private int m_ApiVersion;
	}
}
