using System;
using System.Collections.Generic;

namespace RoR2.Achievements.Bandit2
{
	// Token: 0x02000F06 RID: 3846
	[RegisterAchievement("Bandit2RevolverFinale", "Skills.Bandit2.SkullRevolver", "CompleteThreeStages", typeof(Bandit2RevolverFinaleAchievement.Bandit2RevolverFinaleServerAchievement))]
	public class Bandit2RevolverFinaleAchievement : BaseAchievement
	{
		// Token: 0x0600577C RID: 22396 RVA: 0x0016172B File Offset: 0x0015F92B
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("Bandit2Body");
		}

		// Token: 0x0600577D RID: 22397 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x0600577E RID: 22398 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x02000F07 RID: 3847
		private class Bandit2RevolverFinaleServerAchievement : BaseServerAchievement
		{
			// Token: 0x06005780 RID: 22400 RVA: 0x001618C0 File Offset: 0x0015FAC0
			public override void OnInstall()
			{
				base.OnInstall();
				this.requiredVictimBodyIndices = new List<BodyIndex>();
				this.requiredVictimBodyIndices.Add(BodyCatalog.FindBodyIndex("BrotherHurtBody"));
				this.requiredVictimBodyIndices.Add(BodyCatalog.FindBodyIndex("MiniVoidRaidCrabBodyPhase3"));
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathGlobal;
				GlobalEventManager.onServerDamageDealt += this.OnServerDamageDealt;
				this.lastHitTime = float.NegativeInfinity;
			}

			// Token: 0x06005781 RID: 22401 RVA: 0x00161935 File Offset: 0x0015FB35
			public override void OnUninstall()
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeathGlobal;
				GlobalEventManager.onServerDamageDealt -= this.OnServerDamageDealt;
				base.OnUninstall();
			}

			// Token: 0x06005782 RID: 22402 RVA: 0x00161960 File Offset: 0x0015FB60
			private void OnServerDamageDealt(DamageReport damageReport)
			{
				if (this.requiredVictimBodyIndices.Contains(damageReport.victimBodyIndex) && this.serverAchievementTracker.networkUser.master == damageReport.attackerMaster && damageReport.attackerMaster && this.DoesDamageQualify(damageReport))
				{
					this.lastHitTime = Run.FixedTimeStamp.now.t;
				}
			}

			// Token: 0x06005783 RID: 22403 RVA: 0x001619C0 File Offset: 0x0015FBC0
			private void OnCharacterDeathGlobal(DamageReport damageReport)
			{
				if (this.requiredVictimBodyIndices.Contains(damageReport.victimBodyIndex) && this.serverAchievementTracker.networkUser.master == damageReport.attackerMaster && damageReport.attackerMaster && (this.DoesDamageQualify(damageReport) || Run.FixedTimeStamp.now.t - this.lastHitTime <= 0.1f))
				{
					base.Grant();
				}
			}

			// Token: 0x06005784 RID: 22404 RVA: 0x00161A2C File Offset: 0x0015FC2C
			private bool DoesDamageQualify(DamageReport damageReport)
			{
				return (damageReport.damageInfo.damageType & DamageType.ResetCooldownsOnKill) == DamageType.ResetCooldownsOnKill;
			}

			// Token: 0x040050CD RID: 20685
			private const float lastHitWindowSeconds = 0.1f;

			// Token: 0x040050CE RID: 20686
			private List<BodyIndex> requiredVictimBodyIndices;

			// Token: 0x040050CF RID: 20687
			private float lastHitTime;
		}
	}
}
