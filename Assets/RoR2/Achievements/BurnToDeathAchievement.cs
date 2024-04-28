using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000E7A RID: 3706
	[RegisterAchievement("BurnToDeath", "Items.Cleanse", null, null)]
	public class BurnToDeathAchievement : BaseStatMilestoneAchievement
	{
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060054E1 RID: 21729 RVA: 0x0015D602 File Offset: 0x0015B802
		protected override StatDef statDef
		{
			get
			{
				return StatDef.totalDeathsWhileBurning;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060054E2 RID: 21730 RVA: 0x0015D609 File Offset: 0x0015B809
		protected override ulong statRequirement
		{
			get
			{
				return 3UL;
			}
		}
	}
}
