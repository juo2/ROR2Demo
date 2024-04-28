using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E8D RID: 3725
	[RegisterAchievement("DefeatSuperRoboBallBoss", "Characters.Loader", null, typeof(DefeatSuperRoboBallBossAchievement.DefeatSuperRoboBallBossServerAchievement))]
	public class DefeatSuperRoboBallBossAchievement : BaseAchievement
	{
		// Token: 0x06005534 RID: 21812 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x0015D5B8 File Offset: 0x0015B7B8
		public override void OnUninstall()
		{
			base.OnUninstall();
		}

		// Token: 0x02000E8E RID: 3726
		private class DefeatSuperRoboBallBossServerAchievement : BaseServerAchievement
		{
			// Token: 0x06005537 RID: 21815 RVA: 0x0015DECC File Offset: 0x0015C0CC
			public override void OnInstall()
			{
				base.OnInstall();
				this.superRoboBallBossBodyIndex = BodyCatalog.FindBodyIndex("SuperRoboBallBossBody");
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathGlobal;
			}

			// Token: 0x06005538 RID: 21816 RVA: 0x0015DEF5 File Offset: 0x0015C0F5
			private void OnCharacterDeathGlobal(DamageReport damageReport)
			{
				if (damageReport.victimBodyIndex == this.superRoboBallBossBodyIndex && damageReport.victimTeamIndex != TeamIndex.Player)
				{
					base.Grant();
				}
			}

			// Token: 0x06005539 RID: 21817 RVA: 0x0015DF14 File Offset: 0x0015C114
			public override void OnUninstall()
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeathGlobal;
				base.OnUninstall();
			}

			// Token: 0x04005055 RID: 20565
			private BodyIndex superRoboBallBossBodyIndex;
		}
	}
}
