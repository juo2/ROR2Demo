using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020002F7 RID: 759
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyMemberUpdateReceivedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060012F1 RID: 4849 RVA: 0x0001429B File Offset: 0x0001249B
		public void Set(AddNotifyLobbyMemberUpdateReceivedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x000142A7 File Offset: 0x000124A7
		public void Set(object other)
		{
			this.Set(other as AddNotifyLobbyMemberUpdateReceivedOptions);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400090B RID: 2315
		private int m_ApiVersion;
	}
}
