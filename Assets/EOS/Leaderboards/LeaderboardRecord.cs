using System;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003C4 RID: 964
	public class LeaderboardRecord : ISettable
	{
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x0001894F File Offset: 0x00016B4F
		// (set) Token: 0x06001750 RID: 5968 RVA: 0x00018957 File Offset: 0x00016B57
		public ProductUserId UserId { get; set; }

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001751 RID: 5969 RVA: 0x00018960 File Offset: 0x00016B60
		// (set) Token: 0x06001752 RID: 5970 RVA: 0x00018968 File Offset: 0x00016B68
		public uint Rank { get; set; }

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001753 RID: 5971 RVA: 0x00018971 File Offset: 0x00016B71
		// (set) Token: 0x06001754 RID: 5972 RVA: 0x00018979 File Offset: 0x00016B79
		public int Score { get; set; }

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x00018982 File Offset: 0x00016B82
		// (set) Token: 0x06001756 RID: 5974 RVA: 0x0001898A File Offset: 0x00016B8A
		public string UserDisplayName { get; set; }

		// Token: 0x06001757 RID: 5975 RVA: 0x00018994 File Offset: 0x00016B94
		internal void Set(LeaderboardRecordInternal? other)
		{
			if (other != null)
			{
				this.UserId = other.Value.UserId;
				this.Rank = other.Value.Rank;
				this.Score = other.Value.Score;
				this.UserDisplayName = other.Value.UserDisplayName;
			}
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x000189FE File Offset: 0x00016BFE
		public void Set(object other)
		{
			this.Set(other as LeaderboardRecordInternal?);
		}
	}
}
