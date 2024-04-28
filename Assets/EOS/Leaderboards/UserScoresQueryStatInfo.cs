using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003DB RID: 987
	public class UserScoresQueryStatInfo : ISettable
	{
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x000193FF File Offset: 0x000175FF
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x00019407 File Offset: 0x00017607
		public string StatName { get; set; }

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x00019410 File Offset: 0x00017610
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x00019418 File Offset: 0x00017618
		public LeaderboardAggregation Aggregation { get; set; }

		// Token: 0x060017EB RID: 6123 RVA: 0x00019424 File Offset: 0x00017624
		internal void Set(UserScoresQueryStatInfoInternal? other)
		{
			if (other != null)
			{
				this.StatName = other.Value.StatName;
				this.Aggregation = other.Value.Aggregation;
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00019464 File Offset: 0x00017664
		public void Set(object other)
		{
			this.Set(other as UserScoresQueryStatInfoInternal?);
		}
	}
}
