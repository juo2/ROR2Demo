using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004C2 RID: 1218
	public class AchievementDef
	{
		// Token: 0x06001613 RID: 5651 RVA: 0x00061AEC File Offset: 0x0005FCEC
		public void SetAchievedIcon(Sprite icon)
		{
			this.achievedIcon = icon;
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00061AF8 File Offset: 0x0005FCF8
		public Sprite GetAchievedIcon()
		{
			if (!this.achievedIcon)
			{
				this.achievedIcon = LegacyResourcesAPI.Load<Sprite>(this.iconPath);
				if (!this.achievedIcon)
				{
					this.achievedIcon = LegacyResourcesAPI.Load<Sprite>("Textures/AchievementIcons/texPlaceholderAchievement");
				}
			}
			return this.achievedIcon;
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x00061B46 File Offset: 0x0005FD46
		public Sprite GetUnachievedIcon()
		{
			return LegacyResourcesAPI.Load<Sprite>("Textures/MiscIcons/texUnlockIcon");
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00061B54 File Offset: 0x0005FD54
		public string GetAchievementSoundString()
		{
			if (this.unlockableRewardIdentifier.Contains("Characters."))
			{
				return "Play_UI_achievementUnlock_enhanced";
			}
			if (this.unlockableRewardIdentifier.Contains("Skills.") || this.unlockableRewardIdentifier.Contains("Skins."))
			{
				return "Play_UI_skill_unlock";
			}
			return "Play_UI_achievementUnlock";
		}

		// Token: 0x04001BCA RID: 7114
		public AchievementIndex index;

		// Token: 0x04001BCB RID: 7115
		public ServerAchievementIndex serverIndex = new ServerAchievementIndex
		{
			intValue = -1
		};

		// Token: 0x04001BCC RID: 7116
		public string identifier;

		// Token: 0x04001BCD RID: 7117
		public string unlockableRewardIdentifier;

		// Token: 0x04001BCE RID: 7118
		public string prerequisiteAchievementIdentifier;

		// Token: 0x04001BCF RID: 7119
		public string nameToken;

		// Token: 0x04001BD0 RID: 7120
		public string descriptionToken;

		// Token: 0x04001BD1 RID: 7121
		public string iconPath;

		// Token: 0x04001BD2 RID: 7122
		public Type type;

		// Token: 0x04001BD3 RID: 7123
		public Type serverTrackerType;

		// Token: 0x04001BD4 RID: 7124
		private static readonly string[] emptyStringArray = Array.Empty<string>();

		// Token: 0x04001BD5 RID: 7125
		public string[] childAchievementIdentifiers = AchievementDef.emptyStringArray;

		// Token: 0x04001BD6 RID: 7126
		private Sprite achievedIcon;

		// Token: 0x04001BD7 RID: 7127
		private Sprite unachievedIcon;
	}
}
