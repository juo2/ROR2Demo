using System;
using EntityStates.Railgunner.Weapon;

namespace RoR2.Achievements.Railgunner
{
	// Token: 0x02000EDA RID: 3802
	[RegisterAchievement("RailgunnerConsecutiveWeakPoints", "Skills.Railgunner.SecondaryAlt1", null, null)]
	public class RailgunnerConsecutiveWeakPointsAchievement : BaseAchievement
	{
		// Token: 0x0600568E RID: 22158 RVA: 0x001600A8 File Offset: 0x0015E2A8
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("RailgunnerBody");
		}

		// Token: 0x0600568F RID: 22159 RVA: 0x001601DD File Offset: 0x0015E3DD
		protected override void OnBodyRequirementMet()
		{
			BaseFireSnipe.onWeakPointHit += this.OnWeakPointHit;
			BaseFireSnipe.onWeakPointMissed += this.OnWeakPointMissed;
		}

		// Token: 0x06005690 RID: 22160 RVA: 0x00160201 File Offset: 0x0015E401
		protected override void OnBodyRequirementBroken()
		{
			BaseFireSnipe.onWeakPointHit -= this.OnWeakPointHit;
			BaseFireSnipe.onWeakPointMissed -= this.OnWeakPointMissed;
			this.consecutiveCount = 0;
		}

		// Token: 0x06005691 RID: 22161 RVA: 0x0016022C File Offset: 0x0015E42C
		private void OnWeakPointMissed()
		{
			this.consecutiveCount = 0;
		}

		// Token: 0x06005692 RID: 22162 RVA: 0x00160235 File Offset: 0x0015E435
		private void OnWeakPointHit(DamageInfo damageInfo)
		{
			this.consecutiveCount++;
			if (this.consecutiveCount >= 30)
			{
				base.Grant();
			}
		}

		// Token: 0x04005097 RID: 20631
		private const int requirement = 30;

		// Token: 0x04005098 RID: 20632
		private int consecutiveCount;
	}
}
