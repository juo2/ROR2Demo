using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020004B8 RID: 1208
	public struct AchievementIndex
	{
		// Token: 0x060015AD RID: 5549 RVA: 0x00060B31 File Offset: 0x0005ED31
		public static AchievementIndex operator ++(AchievementIndex achievementIndex)
		{
			achievementIndex.intValue++;
			return achievementIndex;
		}

		// Token: 0x04001BAB RID: 7083
		[SerializeField]
		public int intValue;
	}
}
