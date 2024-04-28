using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003B4 RID: 948
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardRecordByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170006C5 RID: 1733
		// (set) Token: 0x06001707 RID: 5895 RVA: 0x00018519 File Offset: 0x00016719
		public uint LeaderboardRecordIndex
		{
			set
			{
				this.m_LeaderboardRecordIndex = value;
			}
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00018522 File Offset: 0x00016722
		public void Set(CopyLeaderboardRecordByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LeaderboardRecordIndex = other.LeaderboardRecordIndex;
			}
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0001853A File Offset: 0x0001673A
		public void Set(object other)
		{
			this.Set(other as CopyLeaderboardRecordByIndexOptions);
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000ACB RID: 2763
		private int m_ApiVersion;

		// Token: 0x04000ACC RID: 2764
		private uint m_LeaderboardRecordIndex;
	}
}
