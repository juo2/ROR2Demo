using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000E80 RID: 3712
	[RegisterAchievement("CleanupDuty", "Items.Recycle", null, null)]
	public class CleanupDuty : BaseStatMilestoneAchievement
	{
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060054FD RID: 21757 RVA: 0x0015D95B File Offset: 0x0015BB5B
		protected override StatDef statDef
		{
			get
			{
				return StatDef.totalMaulingRockKills;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060054FE RID: 21758 RVA: 0x0015D962 File Offset: 0x0015BB62
		protected override ulong statRequirement
		{
			get
			{
				return 20UL;
			}
		}
	}
}
