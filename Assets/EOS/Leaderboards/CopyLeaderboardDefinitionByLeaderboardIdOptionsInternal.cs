using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003B2 RID: 946
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardDefinitionByLeaderboardIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170006C3 RID: 1731
		// (set) Token: 0x06001700 RID: 5888 RVA: 0x000184C5 File Offset: 0x000166C5
		public string LeaderboardId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LeaderboardId, value);
			}
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x000184D4 File Offset: 0x000166D4
		public void Set(CopyLeaderboardDefinitionByLeaderboardIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LeaderboardId = other.LeaderboardId;
			}
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x000184EC File Offset: 0x000166EC
		public void Set(object other)
		{
			this.Set(other as CopyLeaderboardDefinitionByLeaderboardIdOptions);
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x000184FA File Offset: 0x000166FA
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LeaderboardId);
		}

		// Token: 0x04000AC8 RID: 2760
		private int m_ApiVersion;

		// Token: 0x04000AC9 RID: 2761
		private IntPtr m_LeaderboardId;
	}
}
