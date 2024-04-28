using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003DA RID: 986
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryLeaderboardUserScoresOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000706 RID: 1798
		// (set) Token: 0x060017DF RID: 6111 RVA: 0x00019321 File Offset: 0x00017521
		public ProductUserId[] UserIds
		{
			set
			{
				Helper.TryMarshalSet<ProductUserId>(ref this.m_UserIds, value, out this.m_UserIdsCount);
			}
		}

		// Token: 0x17000707 RID: 1799
		// (set) Token: 0x060017E0 RID: 6112 RVA: 0x00019336 File Offset: 0x00017536
		public UserScoresQueryStatInfo[] StatInfo
		{
			set
			{
				Helper.TryMarshalSet<UserScoresQueryStatInfoInternal, UserScoresQueryStatInfo>(ref this.m_StatInfo, value, out this.m_StatInfoCount);
			}
		}

		// Token: 0x17000708 RID: 1800
		// (set) Token: 0x060017E1 RID: 6113 RVA: 0x0001934B File Offset: 0x0001754B
		public DateTimeOffset? StartTime
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_StartTime, value);
			}
		}

		// Token: 0x17000709 RID: 1801
		// (set) Token: 0x060017E2 RID: 6114 RVA: 0x0001935A File Offset: 0x0001755A
		public DateTimeOffset? EndTime
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_EndTime, value);
			}
		}

		// Token: 0x1700070A RID: 1802
		// (set) Token: 0x060017E3 RID: 6115 RVA: 0x00019369 File Offset: 0x00017569
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00019378 File Offset: 0x00017578
		public void Set(QueryLeaderboardUserScoresOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.UserIds = other.UserIds;
				this.StatInfo = other.StatInfo;
				this.StartTime = other.StartTime;
				this.EndTime = other.EndTime;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x000193CB File Offset: 0x000175CB
		public void Set(object other)
		{
			this.Set(other as QueryLeaderboardUserScoresOptions);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x000193D9 File Offset: 0x000175D9
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserIds);
			Helper.TryMarshalDispose(ref this.m_StatInfo);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000B2B RID: 2859
		private int m_ApiVersion;

		// Token: 0x04000B2C RID: 2860
		private IntPtr m_UserIds;

		// Token: 0x04000B2D RID: 2861
		private uint m_UserIdsCount;

		// Token: 0x04000B2E RID: 2862
		private IntPtr m_StatInfo;

		// Token: 0x04000B2F RID: 2863
		private uint m_StatInfoCount;

		// Token: 0x04000B30 RID: 2864
		private long m_StartTime;

		// Token: 0x04000B31 RID: 2865
		private long m_EndTime;

		// Token: 0x04000B32 RID: 2866
		private IntPtr m_LocalUserId;
	}
}
