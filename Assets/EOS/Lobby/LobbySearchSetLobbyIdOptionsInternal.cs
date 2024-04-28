using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200036A RID: 874
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchSetLobbyIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000664 RID: 1636
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x00017735 File Offset: 0x00015935
		public string LobbyId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00017744 File Offset: 0x00015944
		public void Set(LobbySearchSetLobbyIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.LobbyId;
			}
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0001775C File Offset: 0x0001595C
		public void Set(object other)
		{
			this.Set(other as LobbySearchSetLobbyIdOptions);
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0001776A File Offset: 0x0001596A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LobbyId);
		}

		// Token: 0x04000A64 RID: 2660
		private int m_ApiVersion;

		// Token: 0x04000A65 RID: 2661
		private IntPtr m_LobbyId;
	}
}
