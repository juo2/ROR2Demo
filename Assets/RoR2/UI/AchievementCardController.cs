using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CAC RID: 3244
	public class AchievementCardController : MonoBehaviour
	{
		// Token: 0x060049FD RID: 18941 RVA: 0x0012FE86 File Offset: 0x0012E086
		private static string GetAchievementParentIdentifier(string achievementIdentifier)
		{
			AchievementDef achievementDef = AchievementManager.GetAchievementDef(achievementIdentifier);
			if (achievementDef == null)
			{
				return null;
			}
			return achievementDef.prerequisiteAchievementIdentifier;
		}

		// Token: 0x060049FE RID: 18942 RVA: 0x0012FE9C File Offset: 0x0012E09C
		private static int CalcAchievementTabCount(string achievementIdentifier)
		{
			int num = -1;
			while (!string.IsNullOrEmpty(achievementIdentifier))
			{
				num++;
				achievementIdentifier = AchievementCardController.GetAchievementParentIdentifier(achievementIdentifier);
			}
			return num;
		}

		// Token: 0x060049FF RID: 18943 RVA: 0x0012FEC4 File Offset: 0x0012E0C4
		public void SetAchievement(string achievementIdentifier, UserProfile userProfile)
		{
			AchievementDef achievementDef = AchievementManager.GetAchievementDef(achievementIdentifier);
			if (achievementDef != null)
			{
				bool flag = userProfile.HasAchievement(achievementIdentifier);
				bool flag2 = userProfile.CanSeeAchievement(achievementIdentifier);
				if (this.iconImage)
				{
					this.iconImage.sprite = (flag ? achievementDef.GetAchievedIcon() : achievementDef.GetUnachievedIcon());
				}
				if (this.nameLabel)
				{
					this.nameLabel.token = (userProfile.CanSeeAchievement(achievementIdentifier) ? achievementDef.nameToken : "???");
				}
				if (this.descriptionLabel)
				{
					this.descriptionLabel.token = (userProfile.CanSeeAchievement(achievementIdentifier) ? achievementDef.descriptionToken : "???");
				}
				if (this.unlockedImage)
				{
					this.unlockedImage.gameObject.SetActive(flag);
				}
				if (this.cantBeAchievedImage)
				{
					this.cantBeAchievedImage.gameObject.SetActive(!flag2);
				}
				if (this.tooltipProvider)
				{
					string overrideBodyText = "???";
					if (flag2)
					{
						if (flag)
						{
							UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(achievementDef.unlockableRewardIdentifier);
							if (unlockableDef != null)
							{
								string @string = Language.GetString("ACHIEVEMENT_CARD_REWARD_FORMAT");
								string string2 = Language.GetString(unlockableDef.nameToken);
								overrideBodyText = string.Format(@string, string2);
							}
						}
						else
						{
							string string3 = Language.GetString("ACHIEVEMENT_CARD_REWARD_FORMAT");
							string arg = "???";
							overrideBodyText = string.Format(string3, arg);
						}
					}
					else
					{
						AchievementDef achievementDef2 = AchievementManager.GetAchievementDef(achievementDef.prerequisiteAchievementIdentifier);
						if (achievementDef2 != null)
						{
							string string4 = Language.GetString("ACHIEVEMENT_CARD_PREREQ_FORMAT");
							string string5 = Language.GetString(achievementDef2.nameToken);
							overrideBodyText = string.Format(string4, string5);
						}
					}
					this.tooltipProvider.titleToken = (flag2 ? achievementDef.nameToken : "???");
					this.tooltipProvider.overrideBodyText = overrideBodyText;
				}
				if (this.tabLayoutElement)
				{
					this.tabLayoutElement.preferredWidth = (float)AchievementCardController.CalcAchievementTabCount(achievementIdentifier) * this.tabWidth;
				}
			}
		}

		// Token: 0x040046C2 RID: 18114
		public Image iconImage;

		// Token: 0x040046C3 RID: 18115
		public LanguageTextMeshController nameLabel;

		// Token: 0x040046C4 RID: 18116
		public LanguageTextMeshController descriptionLabel;

		// Token: 0x040046C5 RID: 18117
		public LayoutElement tabLayoutElement;

		// Token: 0x040046C6 RID: 18118
		public float tabWidth;

		// Token: 0x040046C7 RID: 18119
		public GameObject unlockedImage;

		// Token: 0x040046C8 RID: 18120
		public GameObject cantBeAchievedImage;

		// Token: 0x040046C9 RID: 18121
		public TooltipProvider tooltipProvider;
	}
}
