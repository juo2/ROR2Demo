using System;
using EntityStates.Loader;
using UnityEngine;

namespace RoR2.Achievements
{
	// Token: 0x02000EAF RID: 3759
	[RegisterAchievement("LoaderBigSlam", "Skills.Loader.ZapFist", "DefeatSuperRoboBallBoss", null)]
	public class LoaderBigSlamAchievement : BaseAchievement
	{
		// Token: 0x060055B8 RID: 21944 RVA: 0x0015E97A File Offset: 0x0015CB7A
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("LoaderBody");
		}

		// Token: 0x060055B9 RID: 21945 RVA: 0x0015E986 File Offset: 0x0015CB86
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			BaseSwingChargedFist.onHitAuthorityGlobal += this.SwingChargedFistOnOnHitAuthorityGlobal;
		}

		// Token: 0x060055BA RID: 21946 RVA: 0x0015E99F File Offset: 0x0015CB9F
		protected override void OnBodyRequirementBroken()
		{
			BaseSwingChargedFist.onHitAuthorityGlobal -= this.SwingChargedFistOnOnHitAuthorityGlobal;
			base.OnBodyRequirementBroken();
		}

		// Token: 0x060055BB RID: 21947 RVA: 0x0015E9B8 File Offset: 0x0015CBB8
		private void SwingChargedFistOnOnHitAuthorityGlobal(BaseSwingChargedFist state)
		{
			if (state.outer.commonComponents.characterBody == base.localUser.cachedBody)
			{
				Debug.LogFormat("{0}/{1}", new object[]
				{
					state.punchSpeed,
					LoaderBigSlamAchievement.requirement
				});
				if (state.punchSpeed >= LoaderBigSlamAchievement.requirement)
				{
					base.Grant();
				}
			}
		}

		// Token: 0x04005062 RID: 20578
		private static readonly float requirement = (float)(300.0 * HGUnitConversions.milesToMeters / (1.0 * HGUnitConversions.hoursToSeconds));
	}
}
