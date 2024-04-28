using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000600 RID: 1536
	public class CopyPlayerAchievementByIndexOptions
	{
		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x0600256F RID: 9583 RVA: 0x00027D34 File Offset: 0x00025F34
		// (set) Token: 0x06002570 RID: 9584 RVA: 0x00027D3C File Offset: 0x00025F3C
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06002571 RID: 9585 RVA: 0x00027D45 File Offset: 0x00025F45
		// (set) Token: 0x06002572 RID: 9586 RVA: 0x00027D4D File Offset: 0x00025F4D
		public uint AchievementIndex { get; set; }

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06002573 RID: 9587 RVA: 0x00027D56 File Offset: 0x00025F56
		// (set) Token: 0x06002574 RID: 9588 RVA: 0x00027D5E File Offset: 0x00025F5E
		public ProductUserId LocalUserId { get; set; }
	}
}
