using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000630 RID: 1584
	public class UnlockedAchievement : ISettable
	{
		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060026DA RID: 9946 RVA: 0x000295F3 File Offset: 0x000277F3
		// (set) Token: 0x060026DB RID: 9947 RVA: 0x000295FB File Offset: 0x000277FB
		public string AchievementId { get; set; }

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x00029604 File Offset: 0x00027804
		// (set) Token: 0x060026DD RID: 9949 RVA: 0x0002960C File Offset: 0x0002780C
		public DateTimeOffset? UnlockTime { get; set; }

		// Token: 0x060026DE RID: 9950 RVA: 0x00029618 File Offset: 0x00027818
		internal void Set(UnlockedAchievementInternal? other)
		{
			if (other != null)
			{
				this.AchievementId = other.Value.AchievementId;
				this.UnlockTime = other.Value.UnlockTime;
			}
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x00029658 File Offset: 0x00027858
		public void Set(object other)
		{
			this.Set(other as UnlockedAchievementInternal?);
		}
	}
}
