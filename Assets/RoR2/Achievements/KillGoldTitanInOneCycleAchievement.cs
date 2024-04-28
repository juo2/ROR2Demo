using System;
using EntityStates.Missions.Goldshores;

namespace RoR2.Achievements
{
	// Token: 0x02000EAC RID: 3756
	[RegisterAchievement("KillGoldTitanInOneCycle", "Items.Gateway", null, typeof(KillGoldTitanInOneCycleAchievement.KillGoldTitanInOnePhaseServerAchievement))]
	public class KillGoldTitanInOneCycleAchievement : BaseAchievement
	{
		// Token: 0x060055AE RID: 21934 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x060055AF RID: 21935 RVA: 0x0015DB42 File Offset: 0x0015BD42
		public override void OnUninstall()
		{
			base.SetServerTracked(false);
			base.OnUninstall();
		}

		// Token: 0x02000EAD RID: 3757
		public class KillGoldTitanInOnePhaseServerAchievement : BaseServerAchievement
		{
			// Token: 0x060055B1 RID: 21937 RVA: 0x0015E8FB File Offset: 0x0015CAFB
			public override void OnInstall()
			{
				base.OnInstall();
				this.goldTitanBodyIndex = BodyCatalog.FindBodyIndex("TitanGoldBody");
				GoldshoresBossfight.onOneCycleGoldTitanKill += this.OnOneCycleGoldTitanKill;
			}

			// Token: 0x060055B2 RID: 21938 RVA: 0x0015E924 File Offset: 0x0015CB24
			private void OnOneCycleGoldTitanKill()
			{
				if (this.serverAchievementTracker.networkUser.GetCurrentBody())
				{
					base.Grant();
				}
			}

			// Token: 0x060055B3 RID: 21939 RVA: 0x0015E943 File Offset: 0x0015CB43
			public override void OnUninstall()
			{
				GoldshoresBossfight.onOneCycleGoldTitanKill -= this.OnOneCycleGoldTitanKill;
				base.OnUninstall();
			}

			// Token: 0x04005061 RID: 20577
			private BodyIndex goldTitanBodyIndex = BodyIndex.None;
		}
	}
}
