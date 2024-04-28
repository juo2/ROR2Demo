using System;
using RoR2.Stats;

namespace RoR2.Achievements.Toolbot
{
	// Token: 0x02000ED0 RID: 3792
	[RegisterAchievement("ToolbotBeatArenaLater", "Skills.Toolbot.SpecialAlt", "RepeatFirstTeleporter", typeof(ToolbotBeatArenaLaterAchievement.ToolbotBeatArenaLaterServerAchievement))]
	public class ToolbotBeatArenaLaterAchievement : BaseAchievement
	{
		// Token: 0x06005660 RID: 22112 RVA: 0x0015FD82 File Offset: 0x0015DF82
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("ToolbotBody");
		}

		// Token: 0x06005661 RID: 22113 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x06005662 RID: 22114 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x0400508A RID: 20618
		private const int requirement = 6;

		// Token: 0x02000ED1 RID: 3793
		private class ToolbotBeatArenaLaterServerAchievement : BaseServerAchievement
		{
			// Token: 0x06005664 RID: 22116 RVA: 0x0015FD8E File Offset: 0x0015DF8E
			public override void OnInstall()
			{
				base.OnInstall();
				ArenaMissionController.onBeatArena += this.OnBeatArena;
			}

			// Token: 0x06005665 RID: 22117 RVA: 0x0015FDA7 File Offset: 0x0015DFA7
			public override void OnUninstall()
			{
				ArenaMissionController.onBeatArena -= this.OnBeatArena;
				base.OnInstall();
			}

			// Token: 0x06005666 RID: 22118 RVA: 0x0015FDC0 File Offset: 0x0015DFC0
			private void OnBeatArena()
			{
				PlayerStatsComponent playerStatsComponent = this.playerStatsComponentGetter.Get(base.networkUser.masterObject);
				if (playerStatsComponent && playerStatsComponent.currentStats.GetStatValueULong(StatDef.highestStagesCompleted) >= 6UL)
				{
					base.Grant();
				}
			}

			// Token: 0x0400508B RID: 20619
			private MemoizedGetComponent<PlayerStatsComponent> playerStatsComponentGetter;
		}
	}
}
