using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000624 RID: 1572
	public class PlayerAchievement : ISettable
	{
		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x00028E58 File Offset: 0x00027058
		// (set) Token: 0x06002674 RID: 9844 RVA: 0x00028E60 File Offset: 0x00027060
		public string AchievementId { get; set; }

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06002675 RID: 9845 RVA: 0x00028E69 File Offset: 0x00027069
		// (set) Token: 0x06002676 RID: 9846 RVA: 0x00028E71 File Offset: 0x00027071
		public double Progress { get; set; }

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x00028E7A File Offset: 0x0002707A
		// (set) Token: 0x06002678 RID: 9848 RVA: 0x00028E82 File Offset: 0x00027082
		public DateTimeOffset? UnlockTime { get; set; }

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x00028E8B File Offset: 0x0002708B
		// (set) Token: 0x0600267A RID: 9850 RVA: 0x00028E93 File Offset: 0x00027093
		public PlayerStatInfo[] StatInfo { get; set; }

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x00028E9C File Offset: 0x0002709C
		// (set) Token: 0x0600267C RID: 9852 RVA: 0x00028EA4 File Offset: 0x000270A4
		public string DisplayName { get; set; }

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x0600267D RID: 9853 RVA: 0x00028EAD File Offset: 0x000270AD
		// (set) Token: 0x0600267E RID: 9854 RVA: 0x00028EB5 File Offset: 0x000270B5
		public string Description { get; set; }

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x00028EBE File Offset: 0x000270BE
		// (set) Token: 0x06002680 RID: 9856 RVA: 0x00028EC6 File Offset: 0x000270C6
		public string IconURL { get; set; }

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x00028ECF File Offset: 0x000270CF
		// (set) Token: 0x06002682 RID: 9858 RVA: 0x00028ED7 File Offset: 0x000270D7
		public string FlavorText { get; set; }

		// Token: 0x06002683 RID: 9859 RVA: 0x00028EE0 File Offset: 0x000270E0
		internal void Set(PlayerAchievementInternal? other)
		{
			if (other != null)
			{
				this.AchievementId = other.Value.AchievementId;
				this.Progress = other.Value.Progress;
				this.UnlockTime = other.Value.UnlockTime;
				this.StatInfo = other.Value.StatInfo;
				this.DisplayName = other.Value.DisplayName;
				this.Description = other.Value.Description;
				this.IconURL = other.Value.IconURL;
				this.FlavorText = other.Value.FlavorText;
			}
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x00028FA1 File Offset: 0x000271A1
		public void Set(object other)
		{
			this.Set(other as PlayerAchievementInternal?);
		}
	}
}
