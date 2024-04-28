using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000616 RID: 1558
	public class OnAchievementsUnlockedCallbackV2Info : ICallbackInfo, ISettable
	{
		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06002620 RID: 9760 RVA: 0x00028A3F File Offset: 0x00026C3F
		// (set) Token: 0x06002621 RID: 9761 RVA: 0x00028A47 File Offset: 0x00026C47
		public object ClientData { get; private set; }

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x00028A50 File Offset: 0x00026C50
		// (set) Token: 0x06002623 RID: 9763 RVA: 0x00028A58 File Offset: 0x00026C58
		public ProductUserId UserId { get; private set; }

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x00028A61 File Offset: 0x00026C61
		// (set) Token: 0x06002625 RID: 9765 RVA: 0x00028A69 File Offset: 0x00026C69
		public string AchievementId { get; private set; }

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x00028A72 File Offset: 0x00026C72
		// (set) Token: 0x06002627 RID: 9767 RVA: 0x00028A7A File Offset: 0x00026C7A
		public DateTimeOffset? UnlockTime { get; private set; }

		// Token: 0x06002628 RID: 9768 RVA: 0x00028A84 File Offset: 0x00026C84
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x00028A9C File Offset: 0x00026C9C
		internal void Set(OnAchievementsUnlockedCallbackV2InfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.UserId = other.Value.UserId;
				this.AchievementId = other.Value.AchievementId;
				this.UnlockTime = other.Value.UnlockTime;
			}
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x00028B06 File Offset: 0x00026D06
		public void Set(object other)
		{
			this.Set(other as OnAchievementsUnlockedCallbackV2InfoInternal?);
		}
	}
}
