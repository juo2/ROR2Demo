using System;

namespace RoR2.Achievements.Railgunner
{
	// Token: 0x02000EDB RID: 3803
	[RegisterAchievement("RailgunnerDealMassiveDamage", "Skills.Railgunner.UtilityAlt1", null, null)]
	public class RailgunnerDealMassiveDamageAchievement : BaseAchievement
	{
		// Token: 0x06005694 RID: 22164 RVA: 0x001600A8 File Offset: 0x0015E2A8
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("RailgunnerBody");
		}

		// Token: 0x06005695 RID: 22165 RVA: 0x00160255 File Offset: 0x0015E455
		protected override void OnBodyRequirementMet()
		{
			GlobalEventManager.onClientDamageNotified += this.onClientDamageNotified;
		}

		// Token: 0x06005696 RID: 22166 RVA: 0x00160268 File Offset: 0x0015E468
		protected override void OnBodyRequirementBroken()
		{
			GlobalEventManager.onClientDamageNotified -= this.onClientDamageNotified;
		}

		// Token: 0x06005697 RID: 22167 RVA: 0x0016027B File Offset: 0x0015E47B
		private void onClientDamageNotified(DamageDealtMessage message)
		{
			if (message.attacker == base.localUser.cachedBodyObject && message.damage >= 1000000f)
			{
				base.Grant();
			}
		}

		// Token: 0x04005099 RID: 20633
		private const float minimumDamage = 1000000f;
	}
}
