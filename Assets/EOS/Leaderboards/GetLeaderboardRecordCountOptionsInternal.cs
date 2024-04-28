using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003C0 RID: 960
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetLeaderboardRecordCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x06001745 RID: 5957 RVA: 0x000188E1 File Offset: 0x00016AE1
		public void Set(GetLeaderboardRecordCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x000188ED File Offset: 0x00016AED
		public void Set(object other)
		{
			this.Set(other as GetLeaderboardRecordCountOptions);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000AE6 RID: 2790
		private int m_ApiVersion;
	}
}
