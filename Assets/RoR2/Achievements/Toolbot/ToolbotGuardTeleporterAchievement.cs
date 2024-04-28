using System;

namespace RoR2.Achievements.Toolbot
{
	// Token: 0x02000ED3 RID: 3795
	[RegisterAchievement("ToolbotGuardTeleporter", "Skills.Toolbot.Grenade", "RepeatFirstTeleporter", typeof(ToolbotGuardTeleporterAchievement.ToolbotGuardTeleporterServerAchievement))]
	public class ToolbotGuardTeleporterAchievement : BaseAchievement
	{
		// Token: 0x0600566A RID: 22122 RVA: 0x0015FD82 File Offset: 0x0015DF82
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("ToolbotBody");
		}

		// Token: 0x0600566B RID: 22123 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x0600566C RID: 22124 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x02000ED4 RID: 3796
		public class ToolbotGuardTeleporterServerAchievement : BaseServerAchievement
		{
			// Token: 0x0600566E RID: 22126 RVA: 0x0015FE08 File Offset: 0x0015E008
			public override void OnInstall()
			{
				base.OnInstall();
				TeleporterInteraction.onTeleporterBeginChargingGlobal += this.OnTeleporterBeginCharging;
				TeleporterInteraction.onTeleporterChargedGlobal += this.OnTeleporterCharged;
				this.killCount = 0;
				this.beetleQueenBodyIndex = BodyCatalog.FindBodyIndex("BeetleQueen2Body");
			}

			// Token: 0x0600566F RID: 22127 RVA: 0x0015FE54 File Offset: 0x0015E054
			public override void OnUninstall()
			{
				TeleporterInteraction.onTeleporterChargedGlobal -= this.OnTeleporterCharged;
				TeleporterInteraction.onTeleporterBeginChargingGlobal -= this.OnTeleporterBeginCharging;
				this.SetStayedInZone(false);
				base.OnUninstall();
			}

			// Token: 0x06005670 RID: 22128 RVA: 0x0015FE85 File Offset: 0x0015E085
			private void OnTeleporterBeginCharging(TeleporterInteraction teleporterInteraction)
			{
				this.teleporterStartChargingTime = Run.FixedTimeStamp.now;
				this.SetStayedInZone(true);
			}

			// Token: 0x06005671 RID: 22129 RVA: 0x0015FE99 File Offset: 0x0015E099
			private void OnTeleporterCharged(TeleporterInteraction teleporterInteraction)
			{
				this.SetStayedInZone(false);
			}

			// Token: 0x06005672 RID: 22130 RVA: 0x0015FEA4 File Offset: 0x0015E0A4
			private void SetStayedInZone(bool newStayedInZone)
			{
				if (this.stayedInZone == newStayedInZone)
				{
					return;
				}
				this.stayedInZone = newStayedInZone;
				if (this.stayedInZone)
				{
					RoR2Application.onFixedUpdate += this.FixedUpdateTeleporterCharging;
					GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeath;
					this.UpdateStayedInZone();
					return;
				}
				this.killCount = 0;
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeath;
				RoR2Application.onFixedUpdate -= this.FixedUpdateTeleporterCharging;
			}

			// Token: 0x06005673 RID: 22131 RVA: 0x0015FF1C File Offset: 0x0015E11C
			private void FixedUpdateTeleporterCharging()
			{
				this.UpdateStayedInZone();
			}

			// Token: 0x06005674 RID: 22132 RVA: 0x0015FF24 File Offset: 0x0015E124
			private void UpdateStayedInZone()
			{
				if (this.stayedInZone)
				{
					TeleporterInteraction instance = TeleporterInteraction.instance;
					CharacterBody currentBody = base.GetCurrentBody();
					if ((!instance || !instance.holdoutZoneController.IsBodyInChargingRadius(currentBody)) && (this.teleporterStartChargingTime + ToolbotGuardTeleporterAchievement.ToolbotGuardTeleporterServerAchievement.gracePeriod).hasPassed)
					{
						this.SetStayedInZone(false);
					}
				}
			}

			// Token: 0x06005675 RID: 22133 RVA: 0x0015FF7D File Offset: 0x0015E17D
			private void OnCharacterDeath(DamageReport damageReport)
			{
				if (damageReport.victimBodyIndex == this.beetleQueenBodyIndex)
				{
					this.killCount++;
					if (this.killCount >= this.killRequirement)
					{
						base.Grant();
					}
				}
			}

			// Token: 0x0400508C RID: 20620
			private bool stayedInZone;

			// Token: 0x0400508D RID: 20621
			private int killCount;

			// Token: 0x0400508E RID: 20622
			private int killRequirement = 2;

			// Token: 0x0400508F RID: 20623
			private BodyIndex beetleQueenBodyIndex = BodyIndex.None;

			// Token: 0x04005090 RID: 20624
			private Run.FixedTimeStamp teleporterStartChargingTime = Run.FixedTimeStamp.negativeInfinity;

			// Token: 0x04005091 RID: 20625
			private static readonly float gracePeriod = 2f;
		}
	}
}
