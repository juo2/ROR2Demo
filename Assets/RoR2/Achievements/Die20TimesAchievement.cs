using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000E8F RID: 3727
	[RegisterAchievement("Die20Times", "Items.DeathProjectile", null, null)]
	public class Die20TimesAchievement : BaseStatMilestoneAchievement
	{
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600553B RID: 21819 RVA: 0x0015DF2D File Offset: 0x0015C12D
		protected override StatDef statDef
		{
			get
			{
				return StatDef.totalDeaths;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600553C RID: 21820 RVA: 0x0015D962 File Offset: 0x0015BB62
		protected override ulong statRequirement
		{
			get
			{
				return 20UL;
			}
		}
	}
}
