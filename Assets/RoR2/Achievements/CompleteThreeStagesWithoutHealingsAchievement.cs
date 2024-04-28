using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000E89 RID: 3721
	[RegisterAchievement("CompleteThreeStagesWithoutHealing", "Items.IncreaseHealing", null, typeof(CompleteThreeStagesWithoutHealingsAchievement.CompleteThreeStagesWithoutHealingServerAchievement))]
	public class CompleteThreeStagesWithoutHealingsAchievement : BaseAchievement
	{
		// Token: 0x06005525 RID: 21797 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x06005526 RID: 21798 RVA: 0x0015DB42 File Offset: 0x0015BD42
		public override void OnUninstall()
		{
			base.SetServerTracked(false);
			base.OnUninstall();
		}

		// Token: 0x04005054 RID: 20564
		private const int requirement = 2;

		// Token: 0x02000E8A RID: 3722
		private class CompleteThreeStagesWithoutHealingServerAchievement : BaseServerAchievement
		{
			// Token: 0x06005528 RID: 21800 RVA: 0x0015DD94 File Offset: 0x0015BF94
			public override void OnInstall()
			{
				base.OnInstall();
				SceneExitController.onBeginExit += this.OnSceneBeginExit;
			}

			// Token: 0x06005529 RID: 21801 RVA: 0x0015DDAD File Offset: 0x0015BFAD
			public override void OnUninstall()
			{
				SceneExitController.onBeginExit -= this.OnSceneBeginExit;
				base.OnInstall();
			}

			// Token: 0x0600552A RID: 21802 RVA: 0x0015DDC6 File Offset: 0x0015BFC6
			private void OnSceneBeginExit(SceneExitController exitController)
			{
				this.Check();
			}

			// Token: 0x0600552B RID: 21803 RVA: 0x0015DDD0 File Offset: 0x0015BFD0
			private void Check()
			{
				if (Run.instance && Run.instance.GetType() == typeof(Run) && base.networkUser != null)
				{
					StatSheet currentStats = base.networkUser.masterPlayerStatsComponent.currentStats;
					CharacterBody currentBody = base.GetCurrentBody();
					if (currentStats.GetStatValueULong(StatDef.highestStagesCompleted) >= 2UL && currentStats.GetStatValueULong(StatDef.totalHealthHealed) <= 0f && currentBody && currentBody.healthComponent && currentBody.healthComponent.alive)
					{
						base.Grant();
					}
				}
			}
		}
	}
}
