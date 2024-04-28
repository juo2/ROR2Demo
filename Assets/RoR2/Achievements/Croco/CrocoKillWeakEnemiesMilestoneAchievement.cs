using System;
using RoR2.Stats;

namespace RoR2.Achievements.Croco
{
	// Token: 0x02000EF8 RID: 3832
	[RegisterAchievement("CrocoKillWeakEnemiesMilestone", "Skills.Croco.PassivePoisonLethal", "BeatArena", null)]
	public class CrocoKillWeakEnemiesMilestoneAchievement : BaseStatMilestoneAchievement
	{
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06005740 RID: 22336 RVA: 0x00161240 File Offset: 0x0015F440
		protected override StatDef statDef
		{
			get
			{
				return StatDef.totalCrocoWeakEnemyKills;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06005741 RID: 22337 RVA: 0x00161247 File Offset: 0x0015F447
		protected override ulong statRequirement
		{
			get
			{
				return 50UL;
			}
		}
	}
}
