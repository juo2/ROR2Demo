using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200030D RID: 781
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateLobbySearchOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170005B4 RID: 1460
		// (set) Token: 0x06001385 RID: 4997 RVA: 0x00014CC0 File Offset: 0x00012EC0
		public uint MaxResults
		{
			set
			{
				this.m_MaxResults = value;
			}
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00014CC9 File Offset: 0x00012EC9
		public void Set(CreateLobbySearchOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.MaxResults = other.MaxResults;
			}
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00014CE1 File Offset: 0x00012EE1
		public void Set(object other)
		{
			this.Set(other as CreateLobbySearchOptions);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400094D RID: 2381
		private int m_ApiVersion;

		// Token: 0x0400094E RID: 2382
		private uint m_MaxResults;
	}
}
