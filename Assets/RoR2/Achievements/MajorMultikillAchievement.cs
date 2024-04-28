using System;
using UnityEngine;

namespace RoR2.Achievements
{
	// Token: 0x02000EB4 RID: 3764
	[RegisterAchievement("MajorMultikill", "Items.BurnNearby", null, typeof(MajorMultikillAchievement.MajorMultikillServerAchievement))]
	public class MajorMultikillAchievement : BaseAchievement
	{
		// Token: 0x060055D2 RID: 21970 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x060055D3 RID: 21971 RVA: 0x0015D5B8 File Offset: 0x0015B7B8
		public override void OnUninstall()
		{
			base.OnUninstall();
		}

		// Token: 0x02000EB5 RID: 3765
		private class MajorMultikillServerAchievement : BaseServerAchievement
		{
			// Token: 0x060055D5 RID: 21973 RVA: 0x0015EC96 File Offset: 0x0015CE96
			public override void OnInstall()
			{
				base.OnInstall();
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeath;
			}

			// Token: 0x060055D6 RID: 21974 RVA: 0x0015ECAF File Offset: 0x0015CEAF
			public override void OnUninstall()
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeath;
				base.OnUninstall();
			}

			// Token: 0x060055D7 RID: 21975 RVA: 0x0015ECC8 File Offset: 0x0015CEC8
			private void OnCharacterDeath(DamageReport damageReport)
			{
				GameObject attacker = damageReport.damageInfo.attacker;
				if (!attacker)
				{
					return;
				}
				CharacterBody component = attacker.GetComponent<CharacterBody>();
				if (!component)
				{
					return;
				}
				if (component.multiKillCount >= 15 && component.masterObject == this.serverAchievementTracker.networkUser.masterObject)
				{
					base.Grant();
				}
			}

			// Token: 0x04005068 RID: 20584
			private const int multiKillThreshold = 15;
		}
	}
}
