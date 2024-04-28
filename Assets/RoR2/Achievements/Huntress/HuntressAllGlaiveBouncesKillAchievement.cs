using System;
using RoR2.Orbs;

namespace RoR2.Achievements.Huntress
{
	// Token: 0x02000EEC RID: 3820
	[RegisterAchievement("HuntressAllGlaiveBouncesKill", "Skills.Huntress.FlurryArrow", null, typeof(HuntressAllGlaiveBouncesKillAchievement.HuntressAllGlaiveBouncesKillServerAchievement))]
	public class HuntressAllGlaiveBouncesKillAchievement : BaseAchievement
	{
		// Token: 0x060056FC RID: 22268 RVA: 0x00160BDC File Offset: 0x0015EDDC
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("HuntressBody");
		}

		// Token: 0x060056FD RID: 22269 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x060056FE RID: 22270 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x02000EED RID: 3821
		private class HuntressAllGlaiveBouncesKillServerAchievement : BaseServerAchievement
		{
			// Token: 0x06005700 RID: 22272 RVA: 0x00160BE8 File Offset: 0x0015EDE8
			public override void OnInstall()
			{
				base.OnInstall();
				LightningOrb.onLightningOrbKilledOnAllBounces += this.OnLightningOrbKilledOnAllBounces;
			}

			// Token: 0x06005701 RID: 22273 RVA: 0x00160C04 File Offset: 0x0015EE04
			private void OnLightningOrbKilledOnAllBounces(LightningOrb lightningOrb)
			{
				CharacterBody currentBody = base.networkUser.GetCurrentBody();
				if (!currentBody)
				{
					return;
				}
				if (lightningOrb.attacker == currentBody.gameObject && lightningOrb.lightningType == LightningOrb.LightningType.HuntressGlaive)
				{
					base.Grant();
				}
			}

			// Token: 0x06005702 RID: 22274 RVA: 0x00160C48 File Offset: 0x0015EE48
			public override void OnUninstall()
			{
				LightningOrb.onLightningOrbKilledOnAllBounces -= this.OnLightningOrbKilledOnAllBounces;
				base.OnUninstall();
			}
		}
	}
}
