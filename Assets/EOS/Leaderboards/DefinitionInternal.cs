using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003BC RID: 956
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DefinitionInternal : ISettable, IDisposable
	{
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x0001878C File Offset: 0x0001698C
		// (set) Token: 0x06001734 RID: 5940 RVA: 0x000187A8 File Offset: 0x000169A8
		public string LeaderboardId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LeaderboardId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LeaderboardId, value);
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x000187B8 File Offset: 0x000169B8
		// (set) Token: 0x06001736 RID: 5942 RVA: 0x000187D4 File Offset: 0x000169D4
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

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x000187E3 File Offset: 0x000169E3
		// (set) Token: 0x06001738 RID: 5944 RVA: 0x000187EB File Offset: 0x000169EB
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

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x000187F4 File Offset: 0x000169F4
		// (set) Token: 0x0600173A RID: 5946 RVA: 0x00018810 File Offset: 0x00016A10
		public DateTimeOffset? StartTime
		{
			get
			{
				DateTimeOffset? result;
				Helper.TryMarshalGet(this.m_StartTime, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_StartTime, value);
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x00018820 File Offset: 0x00016A20
		// (set) Token: 0x0600173C RID: 5948 RVA: 0x0001883C File Offset: 0x00016A3C
		public DateTimeOffset? EndTime
		{
			get
			{
				DateTimeOffset? result;
				Helper.TryMarshalGet(this.m_EndTime, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_EndTime, value);
			}
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0001884C File Offset: 0x00016A4C
		public void Set(Definition other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LeaderboardId = other.LeaderboardId;
				this.StatName = other.StatName;
				this.Aggregation = other.Aggregation;
				this.StartTime = other.StartTime;
				this.EndTime = other.EndTime;
			}
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0001889F File Offset: 0x00016A9F
		public void Set(object other)
		{
			this.Set(other as Definition);
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x000188AD File Offset: 0x00016AAD
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LeaderboardId);
			Helper.TryMarshalDispose(ref this.m_StatName);
		}

		// Token: 0x04000ADF RID: 2783
		private int m_ApiVersion;

		// Token: 0x04000AE0 RID: 2784
		private IntPtr m_LeaderboardId;

		// Token: 0x04000AE1 RID: 2785
		private IntPtr m_StatName;

		// Token: 0x04000AE2 RID: 2786
		private LeaderboardAggregation m_Aggregation;

		// Token: 0x04000AE3 RID: 2787
		private long m_StartTime;

		// Token: 0x04000AE4 RID: 2788
		private long m_EndTime;
	}
}
