using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000E81 RID: 3713
	[RegisterAchievement("Complete20Stages", "Items.Clover", null, null)]
	public class Complete20StagesAchievement : BaseAchievement
	{
		// Token: 0x06005500 RID: 21760 RVA: 0x0015D967 File Offset: 0x0015BB67
		public override void OnInstall()
		{
			base.OnInstall();
			Run.onRunStartGlobal += this.OnRunStart;
		}

		// Token: 0x06005501 RID: 21761 RVA: 0x0015D980 File Offset: 0x0015BB80
		public override void OnUninstall()
		{
			Run.onRunStartGlobal -= this.OnRunStart;
			this.SetListeningForStats(false);
			base.OnUninstall();
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x0015D9A0 File Offset: 0x0015BBA0
		private void OnRunStart(Run run)
		{
			this.SetListeningForStats(true);
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x0015D9AC File Offset: 0x0015BBAC
		private void SetListeningForStats(bool shouldListen)
		{
			if (this.listeningForStats == shouldListen)
			{
				return;
			}
			this.listeningForStats = shouldListen;
			if (this.listeningForStats)
			{
				this.subscribedProfile = base.localUser.userProfile;
				UserProfile userProfile = this.subscribedProfile;
				userProfile.onStatsReceived = (Action)Delegate.Combine(userProfile.onStatsReceived, new Action(this.OnStatsReceived));
				return;
			}
			UserProfile userProfile2 = this.subscribedProfile;
			userProfile2.onStatsReceived = (Action)Delegate.Remove(userProfile2.onStatsReceived, new Action(this.OnStatsReceived));
			this.subscribedProfile = null;
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x0015DA3C File Offset: 0x0015BC3C
		private void OnStatsReceived()
		{
			PlayerStatsComponent playerStatsComponent = this.playerStatsComponentGetter.Get(base.localUser.cachedMasterObject);
			if (playerStatsComponent && playerStatsComponent.currentStats.GetStatValueULong(StatDef.highestStagesCompleted) >= 20UL)
			{
				base.Grant();
			}
		}

		// Token: 0x0400504D RID: 20557
		private const int requirement = 20;

		// Token: 0x0400504E RID: 20558
		private bool listeningForStats;

		// Token: 0x0400504F RID: 20559
		private UserProfile subscribedProfile;

		// Token: 0x04005050 RID: 20560
		private MemoizedGetComponent<PlayerStatsComponent> playerStatsComponentGetter;
	}
}
