using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003D6 RID: 982
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryLeaderboardDefinitionsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170006FA RID: 1786
		// (set) Token: 0x060017C4 RID: 6084 RVA: 0x000191C7 File Offset: 0x000173C7
		public DateTimeOffset? StartTime
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_StartTime, value);
			}
		}

		// Token: 0x170006FB RID: 1787
		// (set) Token: 0x060017C5 RID: 6085 RVA: 0x000191D6 File Offset: 0x000173D6
		public DateTimeOffset? EndTime
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_EndTime, value);
			}
		}

		// Token: 0x170006FC RID: 1788
		// (set) Token: 0x060017C6 RID: 6086 RVA: 0x000191E5 File Offset: 0x000173E5
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x000191F4 File Offset: 0x000173F4
		public void Set(QueryLeaderboardDefinitionsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.StartTime = other.StartTime;
				this.EndTime = other.EndTime;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00019224 File Offset: 0x00017424
		public void Set(object other)
		{
			this.Set(other as QueryLeaderboardDefinitionsOptions);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00019232 File Offset: 0x00017432
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000B1D RID: 2845
		private int m_ApiVersion;

		// Token: 0x04000B1E RID: 2846
		private long m_StartTime;

		// Token: 0x04000B1F RID: 2847
		private long m_EndTime;

		// Token: 0x04000B20 RID: 2848
		private IntPtr m_LocalUserId;
	}
}
