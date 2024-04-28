using System;
using EntityStates.Railgunner.Weapon;

namespace RoR2.Achievements.Railgunner
{
	// Token: 0x02000ED7 RID: 3799
	[RegisterAchievement("RailgunnerAirborneMultiKill", "Skills.Railgunner.SpecialAlt1", null, typeof(RailgunnerAirborneMultiKillAchievement.RailgunnerAirborneMultiKillServerAchievement))]
	public class RailgunnerAirborneMultiKillAchievement : BaseAchievement
	{
		// Token: 0x06005680 RID: 22144 RVA: 0x001600A8 File Offset: 0x0015E2A8
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("RailgunnerBody");
		}

		// Token: 0x06005681 RID: 22145 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x06005682 RID: 22146 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x04005094 RID: 20628
		private static readonly int requirement = 3;

		// Token: 0x02000ED8 RID: 3800
		private class RailgunnerAirborneMultiKillServerAchievement : BaseServerAchievement
		{
			// Token: 0x06005685 RID: 22149 RVA: 0x001600BC File Offset: 0x0015E2BC
			public override void OnInstall()
			{
				base.OnInstall();
				RoR2Application.onFixedUpdate += this.OnFixedUpdate;
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeath;
				BaseFireSnipe.onFireSnipe += this.OnFireSnipe;
			}

			// Token: 0x06005686 RID: 22150 RVA: 0x001600F7 File Offset: 0x0015E2F7
			private void OnFireSnipe(BaseFireSnipe state)
			{
				this.hasFiredSuperSnipe = (state is FireSnipeSuper);
			}

			// Token: 0x06005687 RID: 22151 RVA: 0x00160108 File Offset: 0x0015E308
			public override void OnUninstall()
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeath;
				RoR2Application.onFixedUpdate -= this.OnFixedUpdate;
				base.OnUninstall();
			}

			// Token: 0x06005688 RID: 22152 RVA: 0x00160134 File Offset: 0x0015E334
			private bool CharacterIsInAir()
			{
				CharacterBody currentBody = base.networkUser.GetCurrentBody();
				return currentBody && currentBody.characterMotor && !currentBody.characterMotor.isGrounded;
			}

			// Token: 0x06005689 RID: 22153 RVA: 0x00160172 File Offset: 0x0015E372
			private void OnFixedUpdate()
			{
				this.killCount = 0;
				this.hasFiredSuperSnipe = false;
			}

			// Token: 0x0600568A RID: 22154 RVA: 0x00160184 File Offset: 0x0015E384
			private void OnCharacterDeath(DamageReport damageReport)
			{
				if (damageReport.attackerMaster == base.networkUser.master && damageReport.attackerMaster != null && this.CharacterIsInAir() && this.hasFiredSuperSnipe)
				{
					this.killCount++;
					if (RailgunnerAirborneMultiKillAchievement.requirement <= this.killCount)
					{
						base.Grant();
					}
				}
			}

			// Token: 0x04005095 RID: 20629
			private int killCount;

			// Token: 0x04005096 RID: 20630
			private bool hasFiredSuperSnipe;
		}
	}
}
