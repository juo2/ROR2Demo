using System;
using Facepunch.Steamworks;

namespace RoR2
{
	// Token: 0x020009CC RID: 2508
	public class AchievementSystemSteam : AchievementSystem
	{
		// Token: 0x06003983 RID: 14723 RVA: 0x000EFD16 File Offset: 0x000EDF16
		public override void AddAchievement(string achievementName)
		{
			Client.Instance.Achievements.Trigger(achievementName, true);
		}
	}
}
