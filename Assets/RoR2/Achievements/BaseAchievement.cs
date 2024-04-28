using System;
using RoR2.EntitlementManagement;
using RoR2.ExpansionManagement;

namespace RoR2.Achievements
{
	// Token: 0x02000E74 RID: 3700
	public abstract class BaseAchievement
	{
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060054AF RID: 21679 RVA: 0x0015D03E File Offset: 0x0015B23E
		// (set) Token: 0x060054B0 RID: 21680 RVA: 0x0015D046 File Offset: 0x0015B246
		private protected LocalUser localUser { protected get; private set; }

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060054B1 RID: 21681 RVA: 0x0015D04F File Offset: 0x0015B24F
		// (set) Token: 0x060054B2 RID: 21682 RVA: 0x0015D057 File Offset: 0x0015B257
		private protected UserProfile userProfile { protected get; private set; }

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060054B3 RID: 21683 RVA: 0x0015D060 File Offset: 0x0015B260
		protected bool isUserAlive
		{
			get
			{
				return this.localUser != null && this.localUser.cachedBody && this.localUser.cachedBody.healthComponent && this.localUser.cachedBody.healthComponent.alive;
			}
		}

		// Token: 0x060054B4 RID: 21684 RVA: 0x0015D0B8 File Offset: 0x0015B2B8
		public virtual void OnInstall()
		{
			this.localUser = this.owner.localUser;
			this.userProfile = this.owner.userProfile;
			this.requiredBodyIndex = this.LookUpRequiredBodyIndex();
			if (this.requiredBodyIndex != BodyIndex.None)
			{
				this.localUser.onBodyChanged += this.HandleBodyChangedForBodyRequirement;
				Run.onRunDestroyGlobal += this.SetBodyRequirementBrokenOnRunEnd;
			}
		}

		// Token: 0x060054B5 RID: 21685 RVA: 0x000137EE File Offset: 0x000119EE
		public virtual float ProgressForAchievement()
		{
			return 0f;
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x0015D124 File Offset: 0x0015B324
		public virtual void OnUninstall()
		{
			if (this.achievementDef.serverTrackerType != null)
			{
				this.SetServerTracked(false);
			}
			if (this.requiredBodyIndex != BodyIndex.None)
			{
				Run.onRunDestroyGlobal -= this.SetBodyRequirementBrokenOnRunEnd;
				this.localUser.onBodyChanged -= this.HandleBodyChangedForBodyRequirement;
				this.meetsBodyRequirement = false;
			}
			this.owner = null;
			this.localUser = null;
			this.userProfile = null;
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x0015D198 File Offset: 0x0015B398
		public void Grant()
		{
			if (this.shouldGrant)
			{
				return;
			}
			UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(this.achievementDef.unlockableRewardIdentifier);
			if (unlockableDef)
			{
				ExpansionDef expansionDefForUnlockable = UnlockableCatalog.GetExpansionDefForUnlockable(unlockableDef.index);
				if (expansionDefForUnlockable && expansionDefForUnlockable.requiredEntitlement && !EntitlementManager.localUserEntitlementTracker.AnyUserHasEntitlement(expansionDefForUnlockable.requiredEntitlement))
				{
					return;
				}
			}
			if (this.localUser.currentNetworkUser && !this.localUser.currentNetworkUser.isParticipating)
			{
				return;
			}
			this.shouldGrant = true;
			this.owner.dirtyGrantsCount++;
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x0015D23C File Offset: 0x0015B43C
		public virtual void OnGranted()
		{
			if (!string.IsNullOrEmpty(this.achievementDef.unlockableRewardIdentifier))
			{
				if (this.localUser.currentNetworkUser)
				{
					UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(this.achievementDef.unlockableRewardIdentifier);
					this.localUser.currentNetworkUser.CallCmdReportUnlock(unlockableDef.index);
				}
				this.userProfile.AddUnlockToken(this.achievementDef.unlockableRewardIdentifier);
			}
		}

		// Token: 0x060054B9 RID: 21689 RVA: 0x0015D2AA File Offset: 0x0015B4AA
		public void SetServerTracked(bool shouldTrack)
		{
			this.owner.SetServerAchievementTracked(this.achievementDef.serverIndex, shouldTrack);
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060054BA RID: 21690 RVA: 0x0015D2C3 File Offset: 0x0015B4C3
		// (set) Token: 0x060054BB RID: 21691 RVA: 0x0015D2CB File Offset: 0x0015B4CB
		private protected BodyIndex requiredBodyIndex { protected get; private set; } = BodyIndex.None;

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060054BC RID: 21692 RVA: 0x0015D2D4 File Offset: 0x0015B4D4
		// (set) Token: 0x060054BD RID: 21693 RVA: 0x0015D2DC File Offset: 0x0015B4DC
		protected bool meetsBodyRequirement
		{
			get
			{
				return this._meetsBodyRequirement;
			}
			set
			{
				if (this._meetsBodyRequirement == value)
				{
					return;
				}
				this._meetsBodyRequirement = value;
				if (this._meetsBodyRequirement)
				{
					this.OnBodyRequirementMet();
					return;
				}
				this.OnBodyRequirementBroken();
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060054BE RID: 21694 RVA: 0x0015D304 File Offset: 0x0015B504
		protected virtual bool wantsBodyCallbacks { get; }

		// Token: 0x060054BF RID: 21695 RVA: 0x000EDC1C File Offset: 0x000EBE1C
		protected virtual BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyIndex.None;
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x0015D30C File Offset: 0x0015B50C
		private void HandleBodyChangedForBodyRequirement()
		{
			bool flag = this.localUser.cachedBody;
			bool meetsBodyRequirement = this.meetsBodyRequirement;
			if (flag)
			{
				meetsBodyRequirement = (this.localUser.cachedBody.bodyIndex == this.requiredBodyIndex);
			}
			this.meetsBodyRequirement = meetsBodyRequirement;
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void OnBodyRequirementMet()
		{
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void OnBodyRequirementBroken()
		{
		}

		// Token: 0x060054C3 RID: 21699 RVA: 0x0015D352 File Offset: 0x0015B552
		private void SetBodyRequirementBrokenOnRunEnd(Run run)
		{
			this.meetsBodyRequirement = false;
		}

		// Token: 0x0400503C RID: 20540
		public UserAchievementManager owner;

		// Token: 0x0400503F RID: 20543
		public bool shouldGrant;

		// Token: 0x04005040 RID: 20544
		public AchievementDef achievementDef;

		// Token: 0x04005042 RID: 20546
		private bool _meetsBodyRequirement;
	}
}
