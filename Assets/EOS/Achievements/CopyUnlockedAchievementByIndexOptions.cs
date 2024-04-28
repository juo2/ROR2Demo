using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000604 RID: 1540
	public class CopyUnlockedAchievementByIndexOptions
	{
		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x00027E72 File Offset: 0x00026072
		// (set) Token: 0x06002587 RID: 9607 RVA: 0x00027E7A File Offset: 0x0002607A
		public ProductUserId UserId { get; set; }

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06002588 RID: 9608 RVA: 0x00027E83 File Offset: 0x00026083
		// (set) Token: 0x06002589 RID: 9609 RVA: 0x00027E8B File Offset: 0x0002608B
		public uint AchievementIndex { get; set; }
	}
}
