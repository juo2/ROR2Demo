using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003D8 RID: 984
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryLeaderboardRanksOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170006FF RID: 1791
		// (set) Token: 0x060017CF RID: 6095 RVA: 0x00019262 File Offset: 0x00017462
		public string LeaderboardId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LeaderboardId, value);
			}
		}

		// Token: 0x17000700 RID: 1792
		// (set) Token: 0x060017D0 RID: 6096 RVA: 0x00019271 File Offset: 0x00017471
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00019280 File Offset: 0x00017480
		public void Set(QueryLeaderboardRanksOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LeaderboardId = other.LeaderboardId;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x000192A4 File Offset: 0x000174A4
		public void Set(object other)
		{
			this.Set(other as QueryLeaderboardRanksOptions);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x000192B2 File Offset: 0x000174B2
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LeaderboardId);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000B23 RID: 2851
		private int m_ApiVersion;

		// Token: 0x04000B24 RID: 2852
		private IntPtr m_LeaderboardId;

		// Token: 0x04000B25 RID: 2853
		private IntPtr m_LocalUserId;
	}
}
