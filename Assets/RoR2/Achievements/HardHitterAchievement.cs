using System;

namespace RoR2.Achievements
{
	// Token: 0x02000EA3 RID: 3747
	[RegisterAchievement("HardHitter", "Items.ShockNearby", null, null)]
	public class HardHitterAchievement : BaseAchievement
	{
		// Token: 0x0600558F RID: 21903 RVA: 0x0015E6A4 File Offset: 0x0015C8A4
		public override void OnInstall()
		{
			base.OnInstall();
			GlobalEventManager.onClientDamageNotified += this.CheckDamage;
		}

		// Token: 0x06005590 RID: 21904 RVA: 0x0015E6BD File Offset: 0x0015C8BD
		public override void OnUninstall()
		{
			GlobalEventManager.onClientDamageNotified -= this.CheckDamage;
			base.OnUninstall();
		}

		// Token: 0x06005591 RID: 21905 RVA: 0x0015E6D6 File Offset: 0x0015C8D6
		public void CheckDamage(DamageDealtMessage damageDealtMessage)
		{
			if (damageDealtMessage.damage >= 5000f && damageDealtMessage.attacker && damageDealtMessage.attacker == base.localUser.cachedBodyObject)
			{
				base.Grant();
			}
		}

		// Token: 0x0400505D RID: 20573
		private const float requirement = 5000f;
	}
}
