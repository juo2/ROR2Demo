using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000EC7 RID: 3783
	[RegisterAchievement("UseThreePortals", "Items.Tonic", null, null)]
	public class UseThreePortalsAchievement : BaseAchievement
	{
		// Token: 0x06005624 RID: 22052 RVA: 0x0015F570 File Offset: 0x0015D770
		public override void OnInstall()
		{
			base.OnInstall();
			this.statsToCheck = new StatDef[]
			{
				PerStageStatDef.totalTimesVisited.FindStatDef("bazaar"),
				PerStageStatDef.totalTimesVisited.FindStatDef("mysteryspace"),
				PerStageStatDef.totalTimesVisited.FindStatDef("goldshores"),
				PerStageStatDef.totalTimesVisited.FindStatDef("arena"),
				PerStageStatDef.totalTimesVisited.FindStatDef("artifactworld")
			};
			UserProfile userProfile = base.userProfile;
			userProfile.onStatsReceived = (Action)Delegate.Combine(userProfile.onStatsReceived, new Action(this.Check));
			this.Check();
		}

		// Token: 0x06005625 RID: 22053 RVA: 0x0015F616 File Offset: 0x0015D816
		public override void OnUninstall()
		{
			UserProfile userProfile = base.userProfile;
			userProfile.onStatsReceived = (Action)Delegate.Remove(userProfile.onStatsReceived, new Action(this.Check));
			base.OnUninstall();
		}

		// Token: 0x06005626 RID: 22054 RVA: 0x0015F645 File Offset: 0x0015D845
		public override float ProgressForAchievement()
		{
			return (float)this.GetUniquePortalsUsedCount() / 3f;
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x0015F654 File Offset: 0x0015D854
		private int GetUniquePortalsUsedCount()
		{
			StatSheet statSheet = base.userProfile.statSheet;
			int num = 0;
			foreach (StatDef statDef in this.statsToCheck)
			{
				if (statSheet.GetStatValueULong(statDef) > 0UL)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06005628 RID: 22056 RVA: 0x0015F69A File Offset: 0x0015D89A
		private void Check()
		{
			if (this.GetUniquePortalsUsedCount() >= 3)
			{
				base.Grant();
			}
		}

		// Token: 0x0400507A RID: 20602
		private StatDef[] statsToCheck;

		// Token: 0x0400507B RID: 20603
		private const int requirement = 3;
	}
}
