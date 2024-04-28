using System;

namespace RoR2.Achievements.Huntress
{
	// Token: 0x02000EEF RID: 3823
	[RegisterAchievement("HuntressCollectCrowbars", "Skills.Huntress.MiniBlink", null, null)]
	public class HuntressCollectCrowbarsAchievement : BaseAchievement
	{
		// Token: 0x06005706 RID: 22278 RVA: 0x00160BDC File Offset: 0x0015EDDC
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("HuntressBody");
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x0015FABA File Offset: 0x0015DCBA
		public override void OnInstall()
		{
			base.OnInstall();
		}

		// Token: 0x06005708 RID: 22280 RVA: 0x00160C61 File Offset: 0x0015EE61
		public override void OnUninstall()
		{
			this.SetCurrentInventory(null);
			base.OnUninstall();
		}

		// Token: 0x06005709 RID: 22281 RVA: 0x00160C70 File Offset: 0x0015EE70
		private void UpdateInventory()
		{
			Inventory inventory = null;
			if (base.localUser.cachedMasterController)
			{
				inventory = base.localUser.cachedMasterController.master.inventory;
			}
			this.SetCurrentInventory(inventory);
		}

		// Token: 0x0600570A RID: 22282 RVA: 0x00160CB0 File Offset: 0x0015EEB0
		private void SetCurrentInventory(Inventory newInventory)
		{
			if (this.currentInventory == newInventory)
			{
				return;
			}
			if (this.currentInventory != null)
			{
				this.currentInventory.onInventoryChanged -= this.OnInventoryChanged;
			}
			this.currentInventory = newInventory;
			if (this.currentInventory != null)
			{
				this.currentInventory.onInventoryChanged += this.OnInventoryChanged;
				this.OnInventoryChanged();
			}
		}

		// Token: 0x0600570B RID: 22283 RVA: 0x00160D12 File Offset: 0x0015EF12
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.localUser.onMasterChanged += this.UpdateInventory;
			this.UpdateInventory();
		}

		// Token: 0x0600570C RID: 22284 RVA: 0x00160D37 File Offset: 0x0015EF37
		protected override void OnBodyRequirementBroken()
		{
			base.localUser.onMasterChanged -= this.UpdateInventory;
			this.SetCurrentInventory(null);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x0600570D RID: 22285 RVA: 0x00160D5D File Offset: 0x0015EF5D
		private void OnInventoryChanged()
		{
			if (HuntressCollectCrowbarsAchievement.requirement <= this.currentInventory.GetItemCount(RoR2Content.Items.Crowbar))
			{
				base.Grant();
			}
		}

		// Token: 0x040050B2 RID: 20658
		private Inventory currentInventory;

		// Token: 0x040050B3 RID: 20659
		private static readonly int requirement = 12;
	}
}
