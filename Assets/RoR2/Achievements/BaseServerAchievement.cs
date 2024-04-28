using System;
using RoR2.EntitlementManagement;
using RoR2.ExpansionManagement;
using UnityEngine;

namespace RoR2.Achievements
{
	// Token: 0x02000E75 RID: 3701
	public class BaseServerAchievement
	{
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060054C5 RID: 21701 RVA: 0x0015D36A File Offset: 0x0015B56A
		public NetworkUser networkUser
		{
			get
			{
				return this.serverAchievementTracker.networkUser;
			}
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x0015D377 File Offset: 0x0015B577
		protected CharacterBody GetCurrentBody()
		{
			return this.networkUser.GetCurrentBody();
		}

		// Token: 0x060054C7 RID: 21703 RVA: 0x0015D384 File Offset: 0x0015B584
		protected bool IsCurrentBody(GameObject gameObject)
		{
			CharacterBody currentBody = this.GetCurrentBody();
			return currentBody && currentBody.gameObject == gameObject;
		}

		// Token: 0x060054C8 RID: 21704 RVA: 0x0015D3AC File Offset: 0x0015B5AC
		protected bool IsCurrentBody(CharacterBody characterBody)
		{
			CharacterBody currentBody = this.GetCurrentBody();
			return currentBody && currentBody == characterBody;
		}

		// Token: 0x060054C9 RID: 21705 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OnInstall()
		{
		}

		// Token: 0x060054CA RID: 21706 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OnUninstall()
		{
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x0015D3D0 File Offset: 0x0015B5D0
		protected void Grant()
		{
			UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(this.achievementDef.unlockableRewardIdentifier);
			if (unlockableDef)
			{
				ExpansionDef expansionDefForUnlockable = UnlockableCatalog.GetExpansionDefForUnlockable(unlockableDef.index);
				if (expansionDefForUnlockable && expansionDefForUnlockable.requiredEntitlement && !EntitlementManager.networkUserEntitlementTracker.UserHasEntitlement(this.networkUser, expansionDefForUnlockable.requiredEntitlement))
				{
					return;
				}
			}
			this.serverAchievementTracker.CallRpcGrantAchievement(this.achievementDef.serverIndex);
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x0015D448 File Offset: 0x0015B648
		public static BaseServerAchievement Instantiate(ServerAchievementIndex serverAchievementIndex)
		{
			AchievementDef achievementDef = AchievementManager.GetAchievementDef(serverAchievementIndex);
			if (achievementDef == null || achievementDef.serverTrackerType == null)
			{
				return null;
			}
			BaseServerAchievement baseServerAchievement = (BaseServerAchievement)Activator.CreateInstance(achievementDef.serverTrackerType);
			baseServerAchievement.achievementDef = achievementDef;
			return baseServerAchievement;
		}

		// Token: 0x04005044 RID: 20548
		public ServerAchievementTracker serverAchievementTracker;

		// Token: 0x04005045 RID: 20549
		public AchievementDef achievementDef;
	}
}
