using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020002EF RID: 751
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyJoinLobbyAcceptedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060012E1 RID: 4833 RVA: 0x00014233 File Offset: 0x00012433
		public void Set(AddNotifyJoinLobbyAcceptedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0001423F File Offset: 0x0001243F
		public void Set(object other)
		{
			this.Set(other as AddNotifyJoinLobbyAcceptedOptions);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000907 RID: 2311
		private int m_ApiVersion;
	}
}
