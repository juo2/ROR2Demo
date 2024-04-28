using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003B8 RID: 952
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardUserScoreByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170006CA RID: 1738
		// (set) Token: 0x06001717 RID: 5911 RVA: 0x000185BE File Offset: 0x000167BE
		public uint LeaderboardUserScoreIndex
		{
			set
			{
				this.m_LeaderboardUserScoreIndex = value;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (set) Token: 0x06001718 RID: 5912 RVA: 0x000185C7 File Offset: 0x000167C7
		public string StatName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_StatName, value);
			}
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x000185D6 File Offset: 0x000167D6
		public void Set(CopyLeaderboardUserScoreByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LeaderboardUserScoreIndex = other.LeaderboardUserScoreIndex;
				this.StatName = other.StatName;
			}
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x000185FA File Offset: 0x000167FA
		public void Set(object other)
		{
			this.Set(other as CopyLeaderboardUserScoreByIndexOptions);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00018608 File Offset: 0x00016808
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_StatName);
		}

		// Token: 0x04000AD2 RID: 2770
		private int m_ApiVersion;

		// Token: 0x04000AD3 RID: 2771
		private uint m_LeaderboardUserScoreIndex;

		// Token: 0x04000AD4 RID: 2772
		private IntPtr m_StatName;
	}
}
