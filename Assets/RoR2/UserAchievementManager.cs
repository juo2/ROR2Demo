using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using RoR2.Achievements;
using RoR2.Stats;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004C4 RID: 1220
	public class UserAchievementManager
	{
		// Token: 0x0600161A RID: 5658 RVA: 0x00061C10 File Offset: 0x0005FE10
		private void OnUserProfileStatsUpdated()
		{
			StatSheet statSheet = this.userProfile.statSheet;
			if (statSheet != null)
			{
				this.OnUserProfileStatsUpdated(statSheet);
			}
			PlayerStatsComponent cachedStatsComponent = this.localUser.cachedStatsComponent;
			if (cachedStatsComponent)
			{
				StatSheet currentStats = cachedStatsComponent.currentStats;
				if (currentStats != null)
				{
					this.OnRunStatsUpdated(currentStats);
				}
			}
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00061C58 File Offset: 0x0005FE58
		private void OnUserProfileStatsUpdated([NotNull] StatSheet statSheet)
		{
			Action<StatSheet> action = this.onUserProfileStatsUpdated;
			if (action == null)
			{
				return;
			}
			action(statSheet);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00061C6B File Offset: 0x0005FE6B
		private void OnRunStatsUpdated([NotNull] StatSheet statSheet)
		{
			Action<StatSheet> action = this.onRunStatsUpdated;
			if (action == null)
			{
				return;
			}
			action(statSheet);
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600161D RID: 5661 RVA: 0x00061C80 File Offset: 0x0005FE80
		// (remove) Token: 0x0600161E RID: 5662 RVA: 0x00061CB8 File Offset: 0x0005FEB8
		public event Action<StatSheet> onUserProfileStatsUpdated;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600161F RID: 5663 RVA: 0x00061CF0 File Offset: 0x0005FEF0
		// (remove) Token: 0x06001620 RID: 5664 RVA: 0x00061D28 File Offset: 0x0005FF28
		public event Action<StatSheet> onRunStatsUpdated;

		// Token: 0x06001621 RID: 5665 RVA: 0x00061D5D File Offset: 0x0005FF5D
		public void SetServerAchievementTracked(ServerAchievementIndex serverAchievementIndex, bool shouldTrack)
		{
			if (this.serverAchievementTrackingMask[serverAchievementIndex.intValue] == shouldTrack)
			{
				return;
			}
			this.serverAchievementTrackingMask[serverAchievementIndex.intValue] = shouldTrack;
			this.serverAchievementTrackingMaskDirty = true;
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00061D85 File Offset: 0x0005FF85
		public void TransmitAchievementRequestsToServer()
		{
			if (this.localUser.currentNetworkUser)
			{
				this.localUser.currentNetworkUser.GetComponent<ServerAchievementTracker>().SendAchievementTrackerRequestsMaskToServer(this.serverAchievementTrackingMask);
			}
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x00061DB4 File Offset: 0x0005FFB4
		public void Update()
		{
			if (this.serverAchievementTrackingMaskDirty)
			{
				this.serverAchievementTrackingMaskDirty = false;
				this.TransmitAchievementRequestsToServer();
			}
			int num = this.achievementsList.Count - 1;
			while (num >= 0 && this.dirtyGrantsCount > 0)
			{
				BaseAchievement baseAchievement = this.achievementsList[num];
				if (baseAchievement.shouldGrant)
				{
					this.dirtyGrantsCount--;
					this.achievementsList.RemoveAt(num);
					this.userProfile.AddAchievement(baseAchievement.achievementDef.identifier, true);
					baseAchievement.OnGranted();
					baseAchievement.OnUninstall();
					NetworkUser currentNetworkUser = this.localUser.currentNetworkUser;
					if (currentNetworkUser != null)
					{
						currentNetworkUser.CallCmdReportAchievement(baseAchievement.achievementDef.nameToken);
					}
				}
				num--;
			}
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x00061E74 File Offset: 0x00060074
		public void GrantAchievement(AchievementDef achievementDef)
		{
			for (int i = 0; i < this.achievementsList.Count; i++)
			{
				if (this.achievementsList[i].achievementDef == achievementDef)
				{
					this.achievementsList[i].Grant();
				}
			}
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00061EBC File Offset: 0x000600BC
		public void HandleServerAchievementCompleted(ServerAchievementIndex serverAchievementIndex)
		{
			BaseAchievement baseAchievement = this.achievementsList.FirstOrDefault((BaseAchievement a) => a.achievementDef.serverIndex == serverAchievementIndex);
			if (baseAchievement == null)
			{
				return;
			}
			baseAchievement.Grant();
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00061EF8 File Offset: 0x000600F8
		public float GetAchievementProgress(AchievementDef achievementDef)
		{
			BaseAchievement baseAchievement = this.achievementsList.FirstOrDefault((BaseAchievement a) => a.achievementDef == achievementDef);
			if (baseAchievement == null)
			{
				return 1f;
			}
			return baseAchievement.ProgressForAchievement();
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00061F38 File Offset: 0x00060138
		public void OnInstall(LocalUser localUser)
		{
			this.localUser = localUser;
			this.userProfile = localUser.userProfile;
			UserProfile userProfile = this.userProfile;
			userProfile.onStatsReceived = (Action)Delegate.Combine(userProfile.onStatsReceived, new Action(this.OnUserProfileStatsUpdated));
			foreach (string text in AchievementManager.readOnlyAchievementIdentifiers)
			{
				AchievementDef achievementDef = AchievementManager.GetAchievementDef(text);
				if (this.userProfile.HasAchievement(text))
				{
					if (!this.userProfile.HasUnlockable(achievementDef.unlockableRewardIdentifier))
					{
						Debug.LogFormat("UserProfile {0} has achievement {1} but not its unlockable {2}. Granting.", new object[]
						{
							this.userProfile.name,
							achievementDef.nameToken,
							achievementDef.unlockableRewardIdentifier
						});
						this.userProfile.AddUnlockToken(achievementDef.unlockableRewardIdentifier);
					}
				}
				else
				{
					BaseAchievement baseAchievement = (BaseAchievement)Activator.CreateInstance(achievementDef.type);
					baseAchievement.achievementDef = achievementDef;
					baseAchievement.owner = this;
					this.achievementsList.Add(baseAchievement);
					baseAchievement.OnInstall();
				}
			}
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0006205C File Offset: 0x0006025C
		public void OnUninstall()
		{
			for (int i = this.achievementsList.Count - 1; i >= 0; i--)
			{
				this.achievementsList[i].OnUninstall();
			}
			this.achievementsList.Clear();
			UserProfile userProfile = this.userProfile;
			userProfile.onStatsReceived = (Action)Delegate.Remove(userProfile.onStatsReceived, new Action(this.OnUserProfileStatsUpdated));
			this.userProfile = null;
			this.localUser = null;
		}

		// Token: 0x04001BDC RID: 7132
		private readonly List<BaseAchievement> achievementsList = new List<BaseAchievement>();

		// Token: 0x04001BDD RID: 7133
		public LocalUser localUser;

		// Token: 0x04001BDE RID: 7134
		public UserProfile userProfile;

		// Token: 0x04001BDF RID: 7135
		public int dirtyGrantsCount;

		// Token: 0x04001BE0 RID: 7136
		private readonly bool[] serverAchievementTrackingMask = new bool[AchievementManager.serverAchievementCount];

		// Token: 0x04001BE1 RID: 7137
		private bool serverAchievementTrackingMaskDirty;
	}
}
