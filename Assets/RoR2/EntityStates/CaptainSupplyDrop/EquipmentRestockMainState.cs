using System;
using RoR2;
using UnityEngine;

namespace EntityStates.CaptainSupplyDrop
{
	// Token: 0x02000416 RID: 1046
	public class EquipmentRestockMainState : BaseMainState
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldShowEnergy
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00053F78 File Offset: 0x00052178
		protected override string GetContextString(Interactor activator)
		{
			return Language.GetString("CAPTAIN_SUPPLY_EQUIPMENT_RESTOCK_INTERACTION");
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00053F84 File Offset: 0x00052184
		protected override Interactability GetInteractability(Interactor activator)
		{
			CharacterBody component = activator.GetComponent<CharacterBody>();
			Inventory inventory;
			if (!component || !(inventory = component.inventory))
			{
				return Interactability.Disabled;
			}
			if (this.activationCost >= this.energyComponent.energy)
			{
				return Interactability.ConditionsNotMet;
			}
			if (inventory.GetEquipmentRestockableChargeCount(inventory.activeEquipmentSlot) <= 0)
			{
				return Interactability.ConditionsNotMet;
			}
			return Interactability.Available;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00053FD8 File Offset: 0x000521D8
		protected override void OnInteractionBegin(Interactor activator)
		{
			this.energyComponent.TakeEnergy(this.activationCost);
			Inventory inventory = activator.GetComponent<CharacterBody>().inventory;
			inventory.RestockEquipmentCharges(inventory.activeEquipmentSlot, 1);
		}

		// Token: 0x04001838 RID: 6200
		[SerializeField]
		public float activationCost;
	}
}
