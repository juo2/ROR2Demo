using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000E77 RID: 3703
	public abstract class BaseStatMilestoneAchievement : BaseAchievement
	{
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060054D2 RID: 21714
		protected abstract StatDef statDef { get; }

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060054D3 RID: 21715
		protected abstract ulong statRequirement { get; }

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060054D4 RID: 21716 RVA: 0x0015D504 File Offset: 0x0015B704
		private ulong statProgress
		{
			get
			{
				return base.userProfile.statSheet.GetStatValueULong(this.statDef);
			}
		}

		// Token: 0x060054D5 RID: 21717 RVA: 0x0015D51C File Offset: 0x0015B71C
		public override void OnInstall()
		{
			base.OnInstall();
			UserProfile userProfile = base.userProfile;
			userProfile.onStatsReceived = (Action)Delegate.Combine(userProfile.onStatsReceived, new Action(this.Check));
			this.Check();
		}

		// Token: 0x060054D6 RID: 21718 RVA: 0x0015D551 File Offset: 0x0015B751
		public override void OnUninstall()
		{
			UserProfile userProfile = base.userProfile;
			userProfile.onStatsReceived = (Action)Delegate.Remove(userProfile.onStatsReceived, new Action(this.Check));
			base.OnUninstall();
		}

		// Token: 0x060054D7 RID: 21719 RVA: 0x0015D580 File Offset: 0x0015B780
		public override float ProgressForAchievement()
		{
			return this.statProgress / this.statRequirement;
		}

		// Token: 0x060054D8 RID: 21720 RVA: 0x0015D593 File Offset: 0x0015B793
		private void Check()
		{
			if (this.statProgress >= this.statRequirement)
			{
				base.Grant();
			}
		}
	}
}
