using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000EC5 RID: 3781
	[RegisterAchievement("TotalDronesRepaired", "Items.DroneBackup", null, null)]
	public class TotalDronesRepairedAchievement : BaseStatMilestoneAchievement
	{
		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x0600561E RID: 22046 RVA: 0x0015F559 File Offset: 0x0015D759
		protected override StatDef statDef
		{
			get
			{
				return StatDef.totalDronesPurchased;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x0600561F RID: 22047 RVA: 0x0015DA8A File Offset: 0x0015BC8A
		protected override ulong statRequirement
		{
			get
			{
				return 30UL;
			}
		}
	}
}
