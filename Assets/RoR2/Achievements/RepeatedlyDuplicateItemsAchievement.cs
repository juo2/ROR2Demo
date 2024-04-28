using System;

namespace RoR2.Achievements
{
	// Token: 0x02000EBE RID: 3774
	[RegisterAchievement("RepeatedlyDuplicateItems", "Items.Firework", null, typeof(RepeatedlyDuplicateItemsAchievement.RepeatedlyDuplicateItemsServerAchievement))]
	public class RepeatedlyDuplicateItemsAchievement : BaseAchievement
	{
		// Token: 0x06005601 RID: 22017 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x06005602 RID: 22018 RVA: 0x0015D5B8 File Offset: 0x0015B7B8
		public override void OnUninstall()
		{
			base.OnUninstall();
		}

		// Token: 0x02000EBF RID: 3775
		private class RepeatedlyDuplicateItemsServerAchievement : BaseServerAchievement
		{
			// Token: 0x06005604 RID: 22020 RVA: 0x0015F1C2 File Offset: 0x0015D3C2
			public override void OnInstall()
			{
				base.OnInstall();
				PurchaseInteraction.onItemSpentOnPurchase += this.OnItemSpentOnPurchase;
				Run.onRunStartGlobal += this.OnRunStartGlobal;
			}

			// Token: 0x06005605 RID: 22021 RVA: 0x0015F1EC File Offset: 0x0015D3EC
			public override void OnUninstall()
			{
				base.OnInstall();
				PurchaseInteraction.onItemSpentOnPurchase -= this.OnItemSpentOnPurchase;
				Run.onRunStartGlobal -= this.OnRunStartGlobal;
			}

			// Token: 0x06005606 RID: 22022 RVA: 0x0015F216 File Offset: 0x0015D416
			private void OnRunStartGlobal(Run run)
			{
				this.progress = 0;
			}

			// Token: 0x06005607 RID: 22023 RVA: 0x0015F220 File Offset: 0x0015D420
			private void OnItemSpentOnPurchase(PurchaseInteraction purchaseInteraction, Interactor interactor)
			{
				CharacterBody currentBody = this.serverAchievementTracker.networkUser.GetCurrentBody();
				if (currentBody && currentBody.GetComponent<Interactor>() == interactor && purchaseInteraction.gameObject.name.Contains("Duplicator"))
				{
					ShopTerminalBehavior component = purchaseInteraction.GetComponent<ShopTerminalBehavior>();
					if (component)
					{
						PickupDef pickupDef = PickupCatalog.GetPickupDef(component.CurrentPickupIndex());
						ItemIndex itemIndex = (pickupDef != null) ? pickupDef.itemIndex : ItemIndex.None;
						if (this.trackingItemIndex != itemIndex)
						{
							this.trackingItemIndex = itemIndex;
							this.progress = 0;
						}
						this.progress++;
						if (this.progress >= 7)
						{
							base.Grant();
						}
					}
				}
			}

			// Token: 0x04005074 RID: 20596
			private const int requirement = 7;

			// Token: 0x04005075 RID: 20597
			private ItemIndex trackingItemIndex = ItemIndex.None;

			// Token: 0x04005076 RID: 20598
			private int progress;
		}
	}
}
