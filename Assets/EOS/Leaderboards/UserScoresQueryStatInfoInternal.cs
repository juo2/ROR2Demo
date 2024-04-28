using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003DC RID: 988
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserScoresQueryStatInfoInternal : ISettable, IDisposable
	{
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x00019478 File Offset: 0x00017678
		// (set) Token: 0x060017EF RID: 6127 RVA: 0x00019494 File Offset: 0x00017694
		public string StatName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_StatName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_StatName, value);
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x000194A3 File Offset: 0x000176A3
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x000194AB File Offset: 0x000176AB
		public LeaderboardAggregation Aggregation
		{
			get
			{
				return this.m_Aggregation;
			}
			set
			{
				this.m_Aggregation = value;
			}
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x000194B4 File Offset: 0x000176B4
		public void Set(UserScoresQueryStatInfo other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.StatName = other.StatName;
				this.Aggregation = other.Aggregation;
			}
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x000194D8 File Offset: 0x000176D8
		public void Set(object other)
		{
			this.Set(other as UserScoresQueryStatInfo);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x000194E6 File Offset: 0x000176E6
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_StatName);
		}

		// Token: 0x04000B35 RID: 2869
		private int m_ApiVersion;

		// Token: 0x04000B36 RID: 2870
		private IntPtr m_StatName;

		// Token: 0x04000B37 RID: 2871
		private LeaderboardAggregation m_Aggregation;
	}
}
