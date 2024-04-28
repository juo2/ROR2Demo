using System;

namespace RoR2.Achievements.Captain
{
	// Token: 0x02000EFA RID: 3834
	[RegisterAchievement("CaptainSupplyDropFinale", "Skills.Captain.UtilityAlt1", "CompleteMainEnding", typeof(CaptainSupplyDropFinaleAchievement.CaptainSupplyDropFinaleServerAchievement))]
	public class CaptainSupplyDropFinaleAchievement : BaseAchievement
	{
		// Token: 0x06005746 RID: 22342 RVA: 0x0015D615 File Offset: 0x0015B815
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("CaptainBody");
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x06005748 RID: 22344 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x02000EFB RID: 3835
		private class CaptainSupplyDropFinaleServerAchievement : BaseServerAchievement
		{
			// Token: 0x0600574A RID: 22346 RVA: 0x0016125C File Offset: 0x0015F45C
			public override void OnInstall()
			{
				base.OnInstall();
				this.requiredVictimBodyIndex = BodyCatalog.FindBodyIndex("BrotherHurtBody");
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathGlobal;
				GlobalEventManager.onServerDamageDealt += this.OnServerDamageDealt;
				this.lastHitTime = float.NegativeInfinity;
			}

			// Token: 0x0600574B RID: 22347 RVA: 0x001612AC File Offset: 0x0015F4AC
			public override void OnUninstall()
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeathGlobal;
				GlobalEventManager.onServerDamageDealt -= this.OnServerDamageDealt;
				base.OnUninstall();
			}

			// Token: 0x0600574C RID: 22348 RVA: 0x001612D8 File Offset: 0x0015F4D8
			private void OnServerDamageDealt(DamageReport damageReport)
			{
				if (damageReport.victimBodyIndex == this.requiredVictimBodyIndex && this.serverAchievementTracker.networkUser.master == damageReport.attackerMaster && damageReport.attackerMaster && this.DoesDamageQualify(damageReport))
				{
					this.lastHitTime = Run.FixedTimeStamp.now.t;
				}
			}

			// Token: 0x0600574D RID: 22349 RVA: 0x00161334 File Offset: 0x0015F534
			private void OnCharacterDeathGlobal(DamageReport damageReport)
			{
				if (damageReport.victimBodyIndex == this.requiredVictimBodyIndex && this.serverAchievementTracker.networkUser.master == damageReport.attackerMaster && damageReport.attackerMaster && (this.DoesDamageQualify(damageReport) || Run.FixedTimeStamp.now.t - this.lastHitTime <= 0.1f))
				{
					base.Grant();
				}
			}

			// Token: 0x0600574E RID: 22350 RVA: 0x0016139C File Offset: 0x0015F59C
			private bool DoesDamageQualify(DamageReport damageReport)
			{
				GenericDisplayNameProvider component = damageReport.damageInfo.inflictor.GetComponent<GenericDisplayNameProvider>();
				return component && component.displayToken != null && component.displayToken.StartsWith("CAPTAIN_SUPPLY_");
			}

			// Token: 0x040050BF RID: 20671
			private const float lastHitWindowSeconds = 0.1f;

			// Token: 0x040050C0 RID: 20672
			private BodyIndex requiredVictimBodyIndex;

			// Token: 0x040050C1 RID: 20673
			private float lastHitTime;
		}
	}
}
