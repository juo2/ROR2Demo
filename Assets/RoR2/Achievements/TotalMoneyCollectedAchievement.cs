using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000EC6 RID: 3782
	[RegisterAchievement("TotalMoneyCollected", "Items.GoldGat", null, null)]
	public class TotalMoneyCollectedAchievement : BaseStatMilestoneAchievement
	{
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06005621 RID: 22049 RVA: 0x0015F560 File Offset: 0x0015D760
		protected override StatDef statDef
		{
			get
			{
				return StatDef.goldCollected;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06005622 RID: 22050 RVA: 0x0015F567 File Offset: 0x0015D767
		protected override ulong statRequirement
		{
			get
			{
				return 30480UL;
			}
		}
	}
}
