using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000E82 RID: 3714
	[RegisterAchievement("Complete30StagesCareer", "Characters.Engineer", null, null)]
	public class Complete30StagesCareerAchievement : BaseStatMilestoneAchievement
	{
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06005506 RID: 21766 RVA: 0x0015DA83 File Offset: 0x0015BC83
		protected override StatDef statDef
		{
			get
			{
				return StatDef.totalStagesCompleted;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06005507 RID: 21767 RVA: 0x0015DA8A File Offset: 0x0015BC8A
		protected override ulong statRequirement
		{
			get
			{
				return 30UL;
			}
		}
	}
}
