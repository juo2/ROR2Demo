using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020002F3 RID: 755
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyInviteReceivedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060012E9 RID: 4841 RVA: 0x00014267 File Offset: 0x00012467
		public void Set(AddNotifyLobbyInviteReceivedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00014273 File Offset: 0x00012473
		public void Set(object other)
		{
			this.Set(other as AddNotifyLobbyInviteReceivedOptions);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000909 RID: 2313
		private int m_ApiVersion;
	}
}
