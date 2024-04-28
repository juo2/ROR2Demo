using System;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000EB2 RID: 3762
	[RegisterAchievement("LogCollector", "Items.Scanner", null, null)]
	public class LogCollectorAchievement : BaseAchievement
	{
		// Token: 0x060055C6 RID: 21958 RVA: 0x0015EB07 File Offset: 0x0015CD07
		public override void OnInstall()
		{
			base.OnInstall();
			this.Check();
			UserProfile.onUnlockableGranted += this.OnUnlockCheck;
		}

		// Token: 0x060055C7 RID: 21959 RVA: 0x0015EB26 File Offset: 0x0015CD26
		public override void OnUninstall()
		{
			UserProfile.onUnlockableGranted -= this.OnUnlockCheck;
			base.OnUninstall();
		}

		// Token: 0x060055C8 RID: 21960 RVA: 0x0015EB3F File Offset: 0x0015CD3F
		public override float ProgressForAchievement()
		{
			return (float)this.MonsterLogCount() / 10f;
		}

		// Token: 0x060055C9 RID: 21961 RVA: 0x0015EB4E File Offset: 0x0015CD4E
		private static bool IsUnlockableMonsterLog(UnlockableDef unlockableDef)
		{
			return unlockableDef.cachedName.StartsWith("Logs.");
		}

		// Token: 0x060055CA RID: 21962 RVA: 0x0015EB60 File Offset: 0x0015CD60
		private int MonsterLogCount()
		{
			StatSheet statSheet = base.userProfile.statSheet;
			int num = 0;
			int i = 0;
			int unlockableCount = statSheet.GetUnlockableCount();
			while (i < unlockableCount)
			{
				if (LogCollectorAchievement.IsUnlockableMonsterLog(statSheet.GetUnlockable(i)))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060055CB RID: 21963 RVA: 0x0015EBA1 File Offset: 0x0015CDA1
		private void Check()
		{
			if (this.MonsterLogCount() >= 10)
			{
				base.Grant();
			}
		}

		// Token: 0x060055CC RID: 21964 RVA: 0x0015EBB3 File Offset: 0x0015CDB3
		private void OnUnlockCheck(UserProfile userProfile, UnlockableDef unlockableDef)
		{
			if (userProfile == base.userProfile && LogCollectorAchievement.IsUnlockableMonsterLog(unlockableDef))
			{
				this.Check();
			}
		}

		// Token: 0x04005067 RID: 20583
		private const int requirement = 10;
	}
}
