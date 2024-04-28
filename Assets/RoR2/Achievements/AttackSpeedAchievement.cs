using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E72 RID: 3698
	[RegisterAchievement("AttackSpeed", "Items.AttackSpeedOnCrit", null, null)]
	public class AttackSpeedAchievement : BaseAchievement
	{
		// Token: 0x060054A5 RID: 21669 RVA: 0x0015CF63 File Offset: 0x0015B163
		public override void OnInstall()
		{
			base.OnInstall();
			RoR2Application.onUpdate += this.CheckAttackSpeed;
		}

		// Token: 0x060054A6 RID: 21670 RVA: 0x0015CF7C File Offset: 0x0015B17C
		public override void OnUninstall()
		{
			RoR2Application.onUpdate -= this.CheckAttackSpeed;
			base.OnUninstall();
		}

		// Token: 0x060054A7 RID: 21671 RVA: 0x0015CF95 File Offset: 0x0015B195
		public void CheckAttackSpeed()
		{
			if (base.localUser != null && base.localUser.cachedBody && base.localUser.cachedBody.attackSpeed >= 3f)
			{
				base.Grant();
			}
		}

		// Token: 0x0400503A RID: 20538
		private const float requirement = 3f;
	}
}
