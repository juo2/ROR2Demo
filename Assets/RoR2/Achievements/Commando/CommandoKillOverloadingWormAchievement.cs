using System;

namespace RoR2.Achievements.Commando
{
	// Token: 0x02000EFF RID: 3839
	[RegisterAchievement("CommandoKillOverloadingWorm", "Skills.Commando.FireShotgunBlast", null, typeof(CommandoKillOverloadingWormAchievement.CommandoKillOverloadingWormServerAchievement))]
	public class CommandoKillOverloadingWormAchievement : BaseAchievement
	{
		// Token: 0x0600575A RID: 22362 RVA: 0x001613DC File Offset: 0x0015F5DC
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("CommandoBody");
		}

		// Token: 0x0600575B RID: 22363 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x0600575C RID: 22364 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x040050C3 RID: 20675
		private int commandoBodyIndex;

		// Token: 0x02000F00 RID: 3840
		public class CommandoKillOverloadingWormServerAchievement : BaseServerAchievement
		{
			// Token: 0x0600575E RID: 22366 RVA: 0x00161453 File Offset: 0x0015F653
			public override void OnInstall()
			{
				base.OnInstall();
				this.overloadingWormBodyIndex = BodyCatalog.FindBodyIndex("ElectricWormBody");
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeath;
			}

			// Token: 0x0600575F RID: 22367 RVA: 0x0016147C File Offset: 0x0015F67C
			public override void OnUninstall()
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeath;
				base.OnUninstall();
			}

			// Token: 0x06005760 RID: 22368 RVA: 0x00161495 File Offset: 0x0015F695
			private void OnCharacterDeath(DamageReport damageReport)
			{
				if (damageReport.victimBody && damageReport.victimBody.bodyIndex == this.overloadingWormBodyIndex && base.IsCurrentBody(damageReport.damageInfo.attacker))
				{
					base.Grant();
				}
			}

			// Token: 0x040050C4 RID: 20676
			private BodyIndex overloadingWormBodyIndex;
		}
	}
}
