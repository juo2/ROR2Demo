using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000EA4 RID: 3748
	[RegisterAchievement("KillBossQuantityInRun", "Items.LunarSkillReplacements", null, null)]
	public class KillBossQuantityInRunAchievement : BaseAchievement
	{
		// Token: 0x06005593 RID: 21907 RVA: 0x0015E710 File Offset: 0x0015C910
		public override void OnInstall()
		{
			base.OnInstall();
			base.localUser.onMasterChanged += this.OnMasterChanged;
			UserProfile userProfile = base.userProfile;
			userProfile.onStatsReceived = (Action)Delegate.Combine(userProfile.onStatsReceived, new Action(this.OnStatsReceived));
		}

		// Token: 0x06005594 RID: 21908 RVA: 0x0015E764 File Offset: 0x0015C964
		public override void OnUninstall()
		{
			UserProfile userProfile = base.userProfile;
			userProfile.onStatsReceived = (Action)Delegate.Remove(userProfile.onStatsReceived, new Action(this.OnStatsReceived));
			base.localUser.onMasterChanged -= this.OnMasterChanged;
			base.OnUninstall();
		}

		// Token: 0x06005595 RID: 21909 RVA: 0x0015E7B5 File Offset: 0x0015C9B5
		private void OnMasterChanged()
		{
			PlayerCharacterMasterController cachedMasterController = base.localUser.cachedMasterController;
			this.playerStatsComponent = ((cachedMasterController != null) ? cachedMasterController.GetComponent<PlayerStatsComponent>() : null);
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x0015E7D4 File Offset: 0x0015C9D4
		private void OnStatsReceived()
		{
			this.Check();
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x0015E7DC File Offset: 0x0015C9DC
		private void Check()
		{
			if (this.playerStatsComponent != null && KillBossQuantityInRunAchievement.requirement <= this.playerStatsComponent.currentStats.GetStatValueULong(StatDef.totalTeleporterBossKillsWitnessed))
			{
				base.Grant();
			}
		}

		// Token: 0x0400505E RID: 20574
		private static readonly ulong requirement = 15UL;

		// Token: 0x0400505F RID: 20575
		private PlayerStatsComponent playerStatsComponent;
	}
}
