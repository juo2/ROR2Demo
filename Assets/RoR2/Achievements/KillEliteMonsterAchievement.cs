using System;

namespace RoR2.Achievements
{
	// Token: 0x02000EA9 RID: 3753
	[RegisterAchievement("KillEliteMonster", "Items.Medkit", null, typeof(KillEliteMonsterAchievement.KillEliteMonsterServerAchievement))]
	public class KillEliteMonsterAchievement : BaseAchievement
	{
		// Token: 0x060055A4 RID: 21924 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x060055A5 RID: 21925 RVA: 0x0015DB42 File Offset: 0x0015BD42
		public override void OnUninstall()
		{
			base.SetServerTracked(false);
			base.OnUninstall();
		}

		// Token: 0x02000EAA RID: 3754
		private class KillEliteMonsterServerAchievement : BaseServerAchievement
		{
			// Token: 0x060055A7 RID: 21927 RVA: 0x0015E86C File Offset: 0x0015CA6C
			public override void OnInstall()
			{
				base.OnInstall();
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeath;
			}

			// Token: 0x060055A8 RID: 21928 RVA: 0x0015E885 File Offset: 0x0015CA85
			public override void OnUninstall()
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeath;
				base.OnUninstall();
			}

			// Token: 0x060055A9 RID: 21929 RVA: 0x0015E8A0 File Offset: 0x0015CAA0
			private void OnCharacterDeath(DamageReport damageReport)
			{
				if (!damageReport.victimIsElite)
				{
					return;
				}
				if (!damageReport.attackerMaster)
				{
					return;
				}
				if (damageReport.attackerMaster.gameObject == this.serverAchievementTracker.networkUser.masterObject)
				{
					base.Grant();
				}
			}
		}
	}
}
