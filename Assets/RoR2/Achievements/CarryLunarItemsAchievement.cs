using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E7E RID: 3710
	[RegisterAchievement("CarryLunarItems", "Items.Meteor", null, null)]
	public class CarryLunarItemsAchievement : BaseAchievement
	{
		// Token: 0x060054F2 RID: 21746 RVA: 0x0015D748 File Offset: 0x0015B948
		public override void OnInstall()
		{
			base.OnInstall();
			base.localUser.onMasterChanged += this.OnMasterChanged;
			this.SetMasterController(base.localUser.cachedMasterController);
		}

		// Token: 0x060054F3 RID: 21747 RVA: 0x0015D778 File Offset: 0x0015B978
		public override void OnUninstall()
		{
			this.SetMasterController(null);
			base.localUser.onMasterChanged -= this.OnMasterChanged;
			base.OnUninstall();
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x0015D7A0 File Offset: 0x0015B9A0
		private void SetMasterController(PlayerCharacterMasterController newMasterController)
		{
			if (this.currentMasterController == newMasterController)
			{
				return;
			}
			if (this.currentInventory != null)
			{
				this.currentInventory.onInventoryChanged -= this.OnInventoryChanged;
			}
			this.currentMasterController = newMasterController;
			PlayerCharacterMasterController playerCharacterMasterController = this.currentMasterController;
			Inventory inventory;
			if (playerCharacterMasterController == null)
			{
				inventory = null;
			}
			else
			{
				CharacterMaster master = playerCharacterMasterController.master;
				inventory = ((master != null) ? master.inventory : null);
			}
			this.currentInventory = inventory;
			if (this.currentInventory != null)
			{
				this.currentInventory.onInventoryChanged += this.OnInventoryChanged;
			}
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x0015D820 File Offset: 0x0015BA20
		private void OnInventoryChanged()
		{
			if (this.currentInventory)
			{
				int num = 5;
				EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(this.currentInventory.currentEquipmentIndex);
				if (equipmentDef != null && equipmentDef.isLunar)
				{
					num--;
				}
				EquipmentDef equipmentDef2 = EquipmentCatalog.GetEquipmentDef(this.currentInventory.alternateEquipmentIndex);
				if (equipmentDef2 != null && equipmentDef2.isLunar)
				{
					num--;
				}
				if (this.currentInventory.HasAtLeastXTotalItemsOfTier(ItemTier.Lunar, num))
				{
					base.Grant();
				}
			}
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x0015D89D File Offset: 0x0015BA9D
		private void OnMasterChanged()
		{
			this.SetMasterController(base.localUser.cachedMasterController);
		}

		// Token: 0x04005049 RID: 20553
		public const int requirement = 5;

		// Token: 0x0400504A RID: 20554
		private PlayerCharacterMasterController currentMasterController;

		// Token: 0x0400504B RID: 20555
		private Inventory currentInventory;
	}
}
