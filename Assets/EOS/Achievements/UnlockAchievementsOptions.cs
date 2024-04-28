using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200062E RID: 1582
	public class UnlockAchievementsOptions
	{
		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x00029560 File Offset: 0x00027760
		// (set) Token: 0x060026D1 RID: 9937 RVA: 0x00029568 File Offset: 0x00027768
		public ProductUserId UserId { get; set; }

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x060026D2 RID: 9938 RVA: 0x00029571 File Offset: 0x00027771
		// (set) Token: 0x060026D3 RID: 9939 RVA: 0x00029579 File Offset: 0x00027779
		public string[] AchievementIds { get; set; }
	}
}
