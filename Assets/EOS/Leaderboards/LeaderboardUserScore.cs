using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003C6 RID: 966
	public class LeaderboardUserScore : ISettable
	{
		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x00018AF3 File Offset: 0x00016CF3
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x00018AFB File Offset: 0x00016CFB
		public ProductUserId UserId { get; set; }

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x00018B04 File Offset: 0x00016D04
		// (set) Token: 0x06001768 RID: 5992 RVA: 0x00018B0C File Offset: 0x00016D0C
		public int Score { get; set; }

		// Token: 0x06001769 RID: 5993 RVA: 0x00018B18 File Offset: 0x00016D18
		internal void Set(LeaderboardUserScoreInternal? other)
		{
			if (other != null)
			{
				this.UserId = other.Value.UserId;
				this.Score = other.Value.Score;
			}
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00018B58 File Offset: 0x00016D58
		public void Set(object other)
		{
			this.Set(other as LeaderboardUserScoreInternal?);
		}
	}
}
