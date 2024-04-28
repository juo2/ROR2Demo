using System;

namespace RoR2.Achievements
{
	// Token: 0x02000EB9 RID: 3769
	[RegisterAchievement("MultiCombatShrine", "Items.EnergizedOnEquipmentUse", null, typeof(MultiCombatShrineAchievement.MultiCombatShrineServerAchievement))]
	public class MultiCombatShrineAchievement : BaseAchievement
	{
		// Token: 0x060055E4 RID: 21988 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x060055E5 RID: 21989 RVA: 0x0015DB42 File Offset: 0x0015BD42
		public override void OnUninstall()
		{
			base.SetServerTracked(false);
			base.OnUninstall();
		}

		// Token: 0x02000EBA RID: 3770
		private class MultiCombatShrineServerAchievement : BaseServerAchievement
		{
			// Token: 0x060055E7 RID: 21991 RVA: 0x0015EE29 File Offset: 0x0015D029
			public override void OnInstall()
			{
				base.OnInstall();
				ShrineCombatBehavior.onDefeatedServerGlobal += this.OnShrineDefeated;
				Stage.onServerStageBegin += this.OnServerStageBegin;
			}

			// Token: 0x060055E8 RID: 21992 RVA: 0x0015EE53 File Offset: 0x0015D053
			public override void OnUninstall()
			{
				Stage.onServerStageBegin -= this.OnServerStageBegin;
				ShrineCombatBehavior.onDefeatedServerGlobal -= this.OnShrineDefeated;
				base.OnUninstall();
			}

			// Token: 0x060055E9 RID: 21993 RVA: 0x0015EE7D File Offset: 0x0015D07D
			private void OnServerStageBegin(Stage stage)
			{
				this.counter = 0;
			}

			// Token: 0x060055EA RID: 21994 RVA: 0x0015EE86 File Offset: 0x0015D086
			private void OnShrineDefeated(ShrineCombatBehavior instance)
			{
				this.counter++;
				this.Check();
			}

			// Token: 0x060055EB RID: 21995 RVA: 0x0015EE9C File Offset: 0x0015D09C
			private void Check()
			{
				if (this.counter >= MultiCombatShrineAchievement.MultiCombatShrineServerAchievement.requirement)
				{
					base.Grant();
				}
			}

			// Token: 0x0400506B RID: 20587
			private int counter;

			// Token: 0x0400506C RID: 20588
			private static readonly int requirement = 3;
		}
	}
}
