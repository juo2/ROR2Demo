using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Missions.ArtifactWorld.TrialController
{
	// Token: 0x02000261 RID: 609
	public class FinishTrial : ArtifactTrialControllerBaseState
	{
		// Token: 0x06000ABB RID: 2747 RVA: 0x0002BE04 File Offset: 0x0002A004
		public override void OnEnter()
		{
			base.OnEnter();
			this.childLocator.FindChild("FinishTrial").gameObject.SetActive(true);
			AchievementDef achievementDef = AchievementManager.GetAchievementDef(this.achievementName);
			if (achievementDef != null)
			{
				foreach (LocalUser user in LocalUserManager.readOnlyLocalUsersList)
				{
					AchievementManager.GetUserAchievementManager(user).GrantAchievement(achievementDef);
				}
			}
		}

		// Token: 0x04000C40 RID: 3136
		[SerializeField]
		public string achievementName;
	}
}
