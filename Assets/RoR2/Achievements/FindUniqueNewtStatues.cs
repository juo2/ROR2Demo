using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000E9E RID: 3742
	[RegisterAchievement("FindUniqueNewtStatues", "Items.Talisman", null, null)]
	public class FindUniqueNewtStatues : BaseAchievement
	{
		// Token: 0x06005575 RID: 21877 RVA: 0x0015E394 File Offset: 0x0015C594
		public override void OnInstall()
		{
			base.OnInstall();
			this.Check();
			UserProfile.onUnlockableGranted += this.OnUnlockCheck;
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x0015E3B3 File Offset: 0x0015C5B3
		public override void OnUninstall()
		{
			UserProfile.onUnlockableGranted -= this.OnUnlockCheck;
			base.OnUninstall();
		}

		// Token: 0x06005577 RID: 21879 RVA: 0x0015E3CC File Offset: 0x0015C5CC
		public override float ProgressForAchievement()
		{
			return (float)this.UniqueNewtStatueCount() / 8f;
		}

		// Token: 0x06005578 RID: 21880 RVA: 0x0015E3DB File Offset: 0x0015C5DB
		private static bool IsUnlockableNewtStatue(UnlockableDef unlockableDef)
		{
			return unlockableDef.cachedName.StartsWith("NewtStatue.");
		}

		// Token: 0x06005579 RID: 21881 RVA: 0x0015E3F0 File Offset: 0x0015C5F0
		private int UniqueNewtStatueCount()
		{
			StatSheet statSheet = base.userProfile.statSheet;
			int num = 0;
			int i = 0;
			int unlockableCount = statSheet.GetUnlockableCount();
			while (i < unlockableCount)
			{
				if (FindUniqueNewtStatues.IsUnlockableNewtStatue(statSheet.GetUnlockable(i)))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x0600557A RID: 21882 RVA: 0x0015E431 File Offset: 0x0015C631
		private void Check()
		{
			if (this.UniqueNewtStatueCount() >= 8)
			{
				base.Grant();
			}
		}

		// Token: 0x0600557B RID: 21883 RVA: 0x0015E442 File Offset: 0x0015C642
		private void OnUnlockCheck(UserProfile userProfile, UnlockableDef unlockableDef)
		{
			if (userProfile == base.userProfile && FindUniqueNewtStatues.IsUnlockableNewtStatue(unlockableDef))
			{
				this.Check();
			}
		}

		// Token: 0x0400505B RID: 20571
		private const int requirement = 8;
	}
}
