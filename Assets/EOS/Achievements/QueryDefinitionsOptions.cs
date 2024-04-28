using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000628 RID: 1576
	public class QueryDefinitionsOptions
	{
		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060026AB RID: 9899 RVA: 0x00029315 File Offset: 0x00027515
		// (set) Token: 0x060026AC RID: 9900 RVA: 0x0002931D File Offset: 0x0002751D
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x060026AD RID: 9901 RVA: 0x00029326 File Offset: 0x00027526
		// (set) Token: 0x060026AE RID: 9902 RVA: 0x0002932E File Offset: 0x0002752E
		public EpicAccountId EpicUserId_DEPRECATED { get; set; }

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060026AF RID: 9903 RVA: 0x00029337 File Offset: 0x00027537
		// (set) Token: 0x060026B0 RID: 9904 RVA: 0x0002933F File Offset: 0x0002753F
		public string[] HiddenAchievementIds_DEPRECATED { get; set; }
	}
}
