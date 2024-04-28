using System;
using RoR2.Stats;

namespace RoR2.Achievements.Croco
{
	// Token: 0x02000EF9 RID: 3833
	[RegisterAchievement("CrocoTotalInfectionsMilestone", "Skills.Croco.ChainableLeap", "BeatArena", null)]
	public class CrocoTotalInfectionsMilestoneAchievement : BaseStatMilestoneAchievement
	{
		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06005743 RID: 22339 RVA: 0x0016124C File Offset: 0x0015F44C
		protected override StatDef statDef
		{
			get
			{
				return StatDef.totalCrocoInfectionsInflicted;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06005744 RID: 22340 RVA: 0x00161253 File Offset: 0x0015F453
		protected override ulong statRequirement
		{
			get
			{
				return 1000UL;
			}
		}
	}
}
