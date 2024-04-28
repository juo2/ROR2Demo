using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200036C RID: 876
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchSetMaxResultsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000666 RID: 1638
		// (set) Token: 0x060015A2 RID: 5538 RVA: 0x00017789 File Offset: 0x00015989
		public uint MaxResults
		{
			set
			{
				this.m_MaxResults = value;
			}
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00017792 File Offset: 0x00015992
		public void Set(LobbySearchSetMaxResultsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.MaxResults = other.MaxResults;
			}
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x000177AA File Offset: 0x000159AA
		public void Set(object other)
		{
			this.Set(other as LobbySearchSetMaxResultsOptions);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000A67 RID: 2663
		private int m_ApiVersion;

		// Token: 0x04000A68 RID: 2664
		private uint m_MaxResults;
	}
}
