using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000612 RID: 1554
	public class OnAchievementsUnlockedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x00028927 File Offset: 0x00026B27
		// (set) Token: 0x0600260B RID: 9739 RVA: 0x0002892F File Offset: 0x00026B2F
		public object ClientData { get; private set; }

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x00028938 File Offset: 0x00026B38
		// (set) Token: 0x0600260D RID: 9741 RVA: 0x00028940 File Offset: 0x00026B40
		public ProductUserId UserId { get; private set; }

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x00028949 File Offset: 0x00026B49
		// (set) Token: 0x0600260F RID: 9743 RVA: 0x00028951 File Offset: 0x00026B51
		public string[] AchievementIds { get; private set; }

		// Token: 0x06002610 RID: 9744 RVA: 0x0002895C File Offset: 0x00026B5C
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x00028974 File Offset: 0x00026B74
		internal void Set(OnAchievementsUnlockedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.UserId = other.Value.UserId;
				this.AchievementIds = other.Value.AchievementIds;
			}
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000289C9 File Offset: 0x00026BC9
		public void Set(object other)
		{
			this.Set(other as OnAchievementsUnlockedCallbackInfoInternal?);
		}
	}
}
