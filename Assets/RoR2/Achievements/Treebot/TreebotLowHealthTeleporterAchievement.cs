using System;

namespace RoR2.Achievements.Treebot
{
	// Token: 0x02000ECF RID: 3791
	[RegisterAchievement("TreebotLowHealthTeleporter", "Skills.Treebot.Barrage", "RescueTreebot", null)]
	public class TreebotLowHealthTeleporterAchievement : BaseAchievement
	{
		// Token: 0x06005654 RID: 22100 RVA: 0x0015FAAE File Offset: 0x0015DCAE
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("TreebotBody");
		}

		// Token: 0x06005655 RID: 22101 RVA: 0x0015FBE7 File Offset: 0x0015DDE7
		private void OnTeleporterBeginChargingGlobal(TeleporterInteraction teleporterInteraction)
		{
			this.failed = false;
			this.healthMonitor.SetActive(true);
		}

		// Token: 0x06005656 RID: 22102 RVA: 0x0015FBFC File Offset: 0x0015DDFC
		private void OnTeleporterChargedGlobal(TeleporterInteraction teleporterInteraction)
		{
			if (!this.failed)
			{
				base.Grant();
			}
		}

		// Token: 0x06005657 RID: 22103 RVA: 0x0015FC0C File Offset: 0x0015DE0C
		private void SubscribeHealthMonitor()
		{
			RoR2Application.onFixedUpdate += this.OnFixedUpdateMonitorHealth;
		}

		// Token: 0x06005658 RID: 22104 RVA: 0x0015FC1F File Offset: 0x0015DE1F
		private void UnsubscribeHealthMonitor()
		{
			RoR2Application.onFixedUpdate -= this.OnFixedUpdateMonitorHealth;
		}

		// Token: 0x06005659 RID: 22105 RVA: 0x0015FC32 File Offset: 0x0015DE32
		private void OnFixedUpdateMonitorHealth()
		{
			if (!this.healthComponent || TreebotLowHealthTeleporterAchievement.requirement < this.healthComponent.combinedHealthFraction)
			{
				this.failed = true;
				this.healthMonitor.SetActive(false);
			}
		}

		// Token: 0x0600565A RID: 22106 RVA: 0x0015FC66 File Offset: 0x0015DE66
		public override void OnInstall()
		{
			base.OnInstall();
			this.healthMonitor = new ToggleAction(new Action(this.SubscribeHealthMonitor), new Action(this.UnsubscribeHealthMonitor));
		}

		// Token: 0x0600565B RID: 22107 RVA: 0x0015FC94 File Offset: 0x0015DE94
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			TeleporterInteraction.onTeleporterBeginChargingGlobal += this.OnTeleporterBeginChargingGlobal;
			TeleporterInteraction.onTeleporterChargedGlobal += this.OnTeleporterChargedGlobal;
			base.localUser.onBodyChanged += this.OnBodyChanged;
			this.OnBodyChanged();
		}

		// Token: 0x0600565C RID: 22108 RVA: 0x0015FCE6 File Offset: 0x0015DEE6
		private void OnBodyChanged()
		{
			CharacterBody cachedBody = base.localUser.cachedBody;
			this.healthComponent = ((cachedBody != null) ? cachedBody.healthComponent : null);
		}

		// Token: 0x0600565D RID: 22109 RVA: 0x0015FD08 File Offset: 0x0015DF08
		protected override void OnBodyRequirementBroken()
		{
			base.localUser.onBodyChanged -= this.OnBodyChanged;
			TeleporterInteraction.onTeleporterChargedGlobal -= this.OnTeleporterChargedGlobal;
			TeleporterInteraction.onTeleporterBeginChargingGlobal -= this.OnTeleporterBeginChargingGlobal;
			this.healthMonitor.SetActive(false);
			this.healthComponent = null;
			base.OnBodyRequirementBroken();
		}

		// Token: 0x04005085 RID: 20613
		private static readonly float requirement = 0.5f;

		// Token: 0x04005086 RID: 20614
		private ToggleAction healthMonitor;

		// Token: 0x04005087 RID: 20615
		private HealthComponent targetHealthComponent;

		// Token: 0x04005088 RID: 20616
		private bool failed = true;

		// Token: 0x04005089 RID: 20617
		private HealthComponent healthComponent;
	}
}
