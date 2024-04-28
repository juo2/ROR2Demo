using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200062A RID: 1578
	public class QueryPlayerAchievementsOptions
	{
		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x000293E0 File Offset: 0x000275E0
		// (set) Token: 0x060026B9 RID: 9913 RVA: 0x000293E8 File Offset: 0x000275E8
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x000293F1 File Offset: 0x000275F1
		// (set) Token: 0x060026BB RID: 9915 RVA: 0x000293F9 File Offset: 0x000275F9
		public ProductUserId LocalUserId { get; set; }
	}
}
