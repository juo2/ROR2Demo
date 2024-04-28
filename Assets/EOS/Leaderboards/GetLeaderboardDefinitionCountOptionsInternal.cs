using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003BE RID: 958
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetLeaderboardDefinitionCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06001741 RID: 5953 RVA: 0x000188C7 File Offset: 0x00016AC7
		public void Set(GetLeaderboardDefinitionCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x000188D3 File Offset: 0x00016AD3
		public void Set(object other)
		{
			this.Set(other as GetLeaderboardDefinitionCountOptions);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000AE5 RID: 2789
		private int m_ApiVersion;
	}
}
