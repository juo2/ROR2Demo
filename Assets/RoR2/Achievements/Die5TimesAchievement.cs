using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000E90 RID: 3728
	[RegisterAchievement("Die5Times", "Items.Bear", null, null)]
	public class Die5TimesAchievement : BaseStatMilestoneAchievement
	{
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x0600553E RID: 21822 RVA: 0x0015DF2D File Offset: 0x0015C12D
		protected override StatDef statDef
		{
			get
			{
				return StatDef.totalDeaths;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x0600553F RID: 21823 RVA: 0x0015DF34 File Offset: 0x0015C134
		protected override ulong statRequirement
		{
			get
			{
				return 5UL;
			}
		}
	}
}
