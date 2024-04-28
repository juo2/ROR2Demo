using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003B0 RID: 944
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardDefinitionByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170006C1 RID: 1729
		// (set) Token: 0x060016F9 RID: 5881 RVA: 0x00018485 File Offset: 0x00016685
		public uint LeaderboardIndex
		{
			set
			{
				this.m_LeaderboardIndex = value;
			}
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0001848E File Offset: 0x0001668E
		public void Set(CopyLeaderboardDefinitionByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LeaderboardIndex = other.LeaderboardIndex;
			}
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x000184A6 File Offset: 0x000166A6
		public void Set(object other)
		{
			this.Set(other as CopyLeaderboardDefinitionByIndexOptions);
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000AC5 RID: 2757
		private int m_ApiVersion;

		// Token: 0x04000AC6 RID: 2758
		private uint m_LeaderboardIndex;
	}
}
