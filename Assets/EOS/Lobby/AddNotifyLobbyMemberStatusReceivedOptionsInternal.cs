using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020002F5 RID: 757
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyLobbyMemberStatusReceivedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060012ED RID: 4845 RVA: 0x00014281 File Offset: 0x00012481
		public void Set(AddNotifyLobbyMemberStatusReceivedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0001428D File Offset: 0x0001248D
		public void Set(object other)
		{
			this.Set(other as AddNotifyLobbyMemberStatusReceivedOptions);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400090A RID: 2314
		private int m_ApiVersion;
	}
}
