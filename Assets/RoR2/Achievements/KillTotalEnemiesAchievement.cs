using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000EAE RID: 3758
	[RegisterAchievement("KillTotalEnemies", "Items.Infusion", null, null)]
	public class KillTotalEnemiesAchievement : BaseStatMilestoneAchievement
	{
		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060055B5 RID: 21941 RVA: 0x0015E96B File Offset: 0x0015CB6B
		protected override StatDef statDef
		{
			get
			{
				return StatDef.totalKills;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060055B6 RID: 21942 RVA: 0x0015E972 File Offset: 0x0015CB72
		protected override ulong statRequirement
		{
			get
			{
				return 3000UL;
			}
		}
	}
}
