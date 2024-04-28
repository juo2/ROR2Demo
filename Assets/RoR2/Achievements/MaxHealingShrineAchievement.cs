using System;

namespace RoR2.Achievements
{
	// Token: 0x02000EB6 RID: 3766
	[RegisterAchievement("MaxHealingShrine", "Items.PassiveHealing", null, typeof(MaxHealingShrineAchievement.MaxHealingShrineServerAchievement))]
	public class MaxHealingShrineAchievement : BaseAchievement
	{
		// Token: 0x060055D9 RID: 21977 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x0015D5B8 File Offset: 0x0015B7B8
		public override void OnUninstall()
		{
			base.OnUninstall();
		}

		// Token: 0x02000EB7 RID: 3767
		private class MaxHealingShrineServerAchievement : BaseServerAchievement
		{
			// Token: 0x060055DC RID: 21980 RVA: 0x0015ED27 File Offset: 0x0015CF27
			public override void OnInstall()
			{
				base.OnInstall();
				ShrineHealingBehavior.onActivated += this.OnHealingShrineActivated;
			}

			// Token: 0x060055DD RID: 21981 RVA: 0x0015ED40 File Offset: 0x0015CF40
			public override void OnUninstall()
			{
				ShrineHealingBehavior.onActivated -= this.OnHealingShrineActivated;
				base.OnUninstall();
			}

			// Token: 0x060055DE RID: 21982 RVA: 0x0015ED5C File Offset: 0x0015CF5C
			private void OnHealingShrineActivated(ShrineHealingBehavior shrine, Interactor activator)
			{
				if (shrine.purchaseCount >= shrine.maxPurchaseCount)
				{
					CharacterBody currentBody = base.GetCurrentBody();
					if (currentBody && currentBody.gameObject == activator.gameObject)
					{
						base.Grant();
					}
				}
			}

			// Token: 0x04005069 RID: 20585
			private const int requirement = 2;
		}
	}
}
