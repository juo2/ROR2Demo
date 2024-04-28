using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020002F1 RID: 753
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyInviteAcceptedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060012E5 RID: 4837 RVA: 0x0001424D File Offset: 0x0001244D
		public void Set(AddNotifyLobbyInviteAcceptedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00014259 File Offset: 0x00012459
		public void Set(object other)
		{
			this.Set(other as AddNotifyLobbyInviteAcceptedOptions);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000908 RID: 2312
		private int m_ApiVersion;
	}
}
