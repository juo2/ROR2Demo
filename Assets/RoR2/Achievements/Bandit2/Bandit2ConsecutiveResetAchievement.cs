using System;
using RoR2.Skills;

namespace RoR2.Achievements.Bandit2
{
	// Token: 0x02000F04 RID: 3844
	[RegisterAchievement("Bandit2ConsecutiveReset", "Skills.Bandit2.Rifle", "CompleteThreeStages", typeof(Bandit2ConsecutiveResetAchievement.Bandit2ConsecutiveResetServerAchievement))]
	public class Bandit2ConsecutiveResetAchievement : BaseAchievement
	{
		// Token: 0x0600576F RID: 22383 RVA: 0x0016172B File Offset: 0x0015F92B
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("Bandit2Body");
		}

		// Token: 0x06005770 RID: 22384 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x06005771 RID: 22385 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x040050C8 RID: 20680
		private static readonly int requirement = 15;

		// Token: 0x02000F05 RID: 3845
		private class Bandit2ConsecutiveResetServerAchievement : BaseServerAchievement
		{
			// Token: 0x170007FF RID: 2047
			// (get) Token: 0x06005774 RID: 22388 RVA: 0x00161740 File Offset: 0x0015F940
			// (set) Token: 0x06005775 RID: 22389 RVA: 0x00161748 File Offset: 0x0015F948
			private CharacterBody trackedBody
			{
				get
				{
					return this._trackedBody;
				}
				set
				{
					if (this._trackedBody == value)
					{
						return;
					}
					if (this._trackedBody != null)
					{
						this._trackedBody.onSkillActivatedServer -= this.OnBodySKillActivatedServer;
					}
					this._trackedBody = value;
					if (this._trackedBody != null)
					{
						this._trackedBody.onSkillActivatedServer += this.OnBodySKillActivatedServer;
						this.progress = 0;
						this.waitingForKill = false;
					}
				}
			}

			// Token: 0x06005776 RID: 22390 RVA: 0x001617B2 File Offset: 0x0015F9B2
			private void OnBodySKillActivatedServer(GenericSkill skillSlot)
			{
				if (skillSlot.skillDef == this.requiredSkillDef && this.requiredSkillDef != null)
				{
					if (this.waitingForKill)
					{
						this.progress = 0;
					}
					this.waitingForKill = true;
				}
			}

			// Token: 0x06005777 RID: 22391 RVA: 0x001617E0 File Offset: 0x0015F9E0
			public override void OnInstall()
			{
				base.OnInstall();
				this.requiredSkillDef = SkillCatalog.GetSkillDef(SkillCatalog.FindSkillIndexByName("Bandit2.ResetRevolver"));
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathGlobal;
				RoR2Application.onFixedUpdate += this.FixedUpdate;
			}

			// Token: 0x06005778 RID: 22392 RVA: 0x0016181F File Offset: 0x0015FA1F
			public override void OnUninstall()
			{
				this.trackedBody = null;
				RoR2Application.onFixedUpdate -= this.FixedUpdate;
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeathGlobal;
				base.OnUninstall();
			}

			// Token: 0x06005779 RID: 22393 RVA: 0x00161850 File Offset: 0x0015FA50
			private void FixedUpdate()
			{
				this.trackedBody = base.GetCurrentBody();
			}

			// Token: 0x0600577A RID: 22394 RVA: 0x00161860 File Offset: 0x0015FA60
			private void OnCharacterDeathGlobal(DamageReport damageReport)
			{
				if (damageReport.attackerBody == this.trackedBody && damageReport.attackerBody && (damageReport.damageInfo.damageType & DamageType.ResetCooldownsOnKill) == DamageType.ResetCooldownsOnKill)
				{
					this.waitingForKill = false;
					this.progress++;
					if (this.progress >= Bandit2ConsecutiveResetAchievement.requirement)
					{
						base.Grant();
					}
				}
			}

			// Token: 0x040050C9 RID: 20681
			private int progress;

			// Token: 0x040050CA RID: 20682
			private SkillDef requiredSkillDef;

			// Token: 0x040050CB RID: 20683
			private bool waitingForKill;

			// Token: 0x040050CC RID: 20684
			private CharacterBody _trackedBody;
		}
	}
}
