using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E87 RID: 3719
	[RegisterAchievement("CompleteTeleporterWithoutInjury", "Items.SecondarySkillMagazine", null, null)]
	public class CompleteTeleporterWithoutInjuryAchievement : BaseAchievement
	{
		// Token: 0x06005519 RID: 21785 RVA: 0x0015DBC8 File Offset: 0x0015BDC8
		public override void OnInstall()
		{
			base.OnInstall();
			TeleporterInteraction.onTeleporterBeginChargingGlobal += this.OnTeleporterBeginCharging;
			TeleporterInteraction.onTeleporterChargedGlobal += this.OnTeleporterCharged;
			GlobalEventManager.onClientDamageNotified += this.OnClientDamageNotified;
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x0015DC03 File Offset: 0x0015BE03
		public override void OnUninstall()
		{
			TeleporterInteraction.onTeleporterBeginChargingGlobal -= this.OnTeleporterBeginCharging;
			TeleporterInteraction.onTeleporterChargedGlobal -= this.OnTeleporterCharged;
			GlobalEventManager.onClientDamageNotified -= this.OnClientDamageNotified;
			base.OnUninstall();
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x0015DC3E File Offset: 0x0015BE3E
		private void OnTeleporterBeginCharging(TeleporterInteraction teleporterInteraction)
		{
			this.hasBeenHit = false;
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x0015DC47 File Offset: 0x0015BE47
		private void OnTeleporterCharged(TeleporterInteraction teleporterInteraction)
		{
			this.Check();
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x0015DC4F File Offset: 0x0015BE4F
		private void OnClientDamageNotified(DamageDealtMessage damageDealtMessage)
		{
			if (!this.hasBeenHit && damageDealtMessage.victim && damageDealtMessage.victim == base.localUser.cachedBodyObject)
			{
				this.hasBeenHit = true;
			}
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x0015DC88 File Offset: 0x0015BE88
		private void Check()
		{
			if (base.localUser.cachedBody && base.localUser.cachedBody.healthComponent && base.localUser.cachedBody.healthComponent.alive && !this.hasBeenHit)
			{
				base.Grant();
			}
		}

		// Token: 0x04005052 RID: 20562
		private bool hasBeenHit;
	}
}
