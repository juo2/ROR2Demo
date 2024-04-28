using System;
using Epic.OnlineServices.Achievements;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200099B RID: 2459
	public class AchievementSystemEOS : AchievementSystem
	{
		// Token: 0x060037D4 RID: 14292 RVA: 0x000EAA90 File Offset: 0x000E8C90
		public AchievementSystemEOS()
		{
			this._achievementsInterface = EOSPlatformManager.GetPlatformInterface().GetAchievementsInterface();
			QueryPlayerAchievementsOptions options = new QueryPlayerAchievementsOptions
			{
				LocalUserId = EOSLoginManager.loggedInProductId,
				TargetUserId = EOSLoginManager.loggedInProductId
			};
			this._achievementsInterface.QueryPlayerAchievements(options, null, null);
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000EAAE0 File Offset: 0x000E8CE0
		public override void AddAchievement(string achievementName)
		{
			if (!string.IsNullOrEmpty(achievementName))
			{
				achievementName = achievementName.ToUpper();
			}
			else
			{
				Debug.LogError("Invalid achievement name. Achievement is null or empty.");
			}
			if (this.IsValidAchievement(achievementName))
			{
				UnlockAchievementsOptions options = new UnlockAchievementsOptions
				{
					AchievementIds = new string[]
					{
						achievementName
					},
					UserId = EOSLoginManager.loggedInProductId
				};
				this._achievementsInterface.UnlockAchievements(options, null, null);
				return;
			}
			Debug.LogError("Invalid achievement name: " + achievementName + ". Make sure the achievement's name is defined in the EOS Developer Portal.");
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000EAB5C File Offset: 0x000E8D5C
		private bool IsValidAchievement(string achievementName)
		{
			CopyAchievementDefinitionV2ByAchievementIdOptions options = new CopyAchievementDefinitionV2ByAchievementIdOptions
			{
				AchievementId = achievementName
			};
			DefinitionV2 definitionV;
			this._achievementsInterface.CopyAchievementDefinitionV2ByAchievementId(options, out definitionV);
			return definitionV != null && !definitionV.IsHidden;
		}

		// Token: 0x04003801 RID: 14337
		private AchievementsInterface _achievementsInterface;
	}
}
