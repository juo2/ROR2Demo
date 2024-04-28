using System;

namespace RoR2.Achievements.Mage
{
	// Token: 0x02000EE1 RID: 3809
	[RegisterAchievement("MageAirborneMultiKill", "Skills.Mage.FlyUp", "FreeMage", typeof(MageAirborneMultiKillAchievement.MageAirborneMultiKillServerAchievement))]
	public class MageAirborneMultiKillAchievement : BaseAchievement
	{
		// Token: 0x060056C0 RID: 22208 RVA: 0x00160754 File Offset: 0x0015E954
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("MageBody");
		}

		// Token: 0x060056C1 RID: 22209 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x060056C2 RID: 22210 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x040050A6 RID: 20646
		private static readonly int requirement = 15;

		// Token: 0x02000EE2 RID: 3810
		private class MageAirborneMultiKillServerAchievement : BaseServerAchievement
		{
			// Token: 0x060056C5 RID: 22213 RVA: 0x00160769 File Offset: 0x0015E969
			public override void OnInstall()
			{
				base.OnInstall();
				RoR2Application.onFixedUpdate += this.OnFixedUpdate;
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeath;
			}

			// Token: 0x060056C6 RID: 22214 RVA: 0x00160793 File Offset: 0x0015E993
			public override void OnUninstall()
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeath;
				RoR2Application.onFixedUpdate -= this.OnFixedUpdate;
				base.OnUninstall();
			}

			// Token: 0x060056C7 RID: 22215 RVA: 0x001607C0 File Offset: 0x0015E9C0
			private bool CharacterIsInAir()
			{
				CharacterBody currentBody = base.networkUser.GetCurrentBody();
				return currentBody && currentBody.characterMotor && !currentBody.characterMotor.isGrounded;
			}

			// Token: 0x060056C8 RID: 22216 RVA: 0x001607FE File Offset: 0x0015E9FE
			private void OnFixedUpdate()
			{
				if (!this.CharacterIsInAir())
				{
					this.killCount = 0;
				}
			}

			// Token: 0x060056C9 RID: 22217 RVA: 0x00160810 File Offset: 0x0015EA10
			private void OnCharacterDeath(DamageReport damageReport)
			{
				if (damageReport.attackerMaster == base.networkUser.master && damageReport.attackerMaster != null && this.CharacterIsInAir())
				{
					this.killCount++;
					if (MageAirborneMultiKillAchievement.requirement <= this.killCount)
					{
						base.Grant();
					}
				}
			}

			// Token: 0x040050A7 RID: 20647
			private int killCount;
		}
	}
}
