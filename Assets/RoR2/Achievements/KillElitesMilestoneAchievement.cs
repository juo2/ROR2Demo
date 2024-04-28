using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000EAB RID: 3755
	[RegisterAchievement("KillElitesMilestone", "Items.ExecuteLowHealthElite", null, null)]
	public class KillElitesMilestoneAchievement : BaseStatMilestoneAchievement
	{
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x060055AB RID: 21931 RVA: 0x0015E8EC File Offset: 0x0015CAEC
		protected override StatDef statDef
		{
			get
			{
				return StatDef.totalEliteKills;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x060055AC RID: 21932 RVA: 0x0015E8F3 File Offset: 0x0015CAF3
		protected override ulong statRequirement
		{
			get
			{
				return 500UL;
			}
		}
	}
}
