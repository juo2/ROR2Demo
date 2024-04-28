using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003BB RID: 955
	public class Definition : ISettable
	{
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x000186A2 File Offset: 0x000168A2
		// (set) Token: 0x06001727 RID: 5927 RVA: 0x000186AA File Offset: 0x000168AA
		public string LeaderboardId { get; set; }

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x000186B3 File Offset: 0x000168B3
		// (set) Token: 0x06001729 RID: 5929 RVA: 0x000186BB File Offset: 0x000168BB
		public string StatName { get; set; }

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600172A RID: 5930 RVA: 0x000186C4 File Offset: 0x000168C4
		// (set) Token: 0x0600172B RID: 5931 RVA: 0x000186CC File Offset: 0x000168CC
		public LeaderboardAggregation Aggregation { get; set; }

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x000186D5 File Offset: 0x000168D5
		// (set) Token: 0x0600172D RID: 5933 RVA: 0x000186DD File Offset: 0x000168DD
		public DateTimeOffset? StartTime { get; set; }

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x000186E6 File Offset: 0x000168E6
		// (set) Token: 0x0600172F RID: 5935 RVA: 0x000186EE File Offset: 0x000168EE
		public DateTimeOffset? EndTime { get; set; }

		// Token: 0x06001730 RID: 5936 RVA: 0x000186F8 File Offset: 0x000168F8
		internal void Set(DefinitionInternal? other)
		{
			if (other != null)
			{
				this.LeaderboardId = other.Value.LeaderboardId;
				this.StatName = other.Value.StatName;
				this.Aggregation = other.Value.Aggregation;
				this.StartTime = other.Value.StartTime;
				this.EndTime = other.Value.EndTime;
			}
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x00018777 File Offset: 0x00016977
		public void Set(object other)
		{
			this.Set(other as DefinitionInternal?);
		}
	}
}
