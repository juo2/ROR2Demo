using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000892 RID: 2194
	public class ShrineCleanseBehavior : MonoBehaviour, IInteractable
	{
		// Token: 0x06003054 RID: 12372 RVA: 0x000CD852 File Offset: 0x000CBA52
		public string GetContextString(Interactor activator)
		{
			return Language.GetString(this.contextToken);
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000CD860 File Offset: 0x000CBA60
		private static bool InventoryIsCleansable(Inventory inventory)
		{
			for (int i = 0; i < ShrineCleanseBehavior.cleansableItems.Length; i++)
			{
				if (inventory.GetItemCount(ShrineCleanseBehavior.cleansableItems[i]) > 0)
				{
					return true;
				}
			}
			int j = 0;
			int equipmentSlotCount = inventory.GetEquipmentSlotCount();
			while (j < equipmentSlotCount)
			{
				EquipmentState equipment = inventory.GetEquipment((uint)j);
				for (int k = 0; k < ShrineCleanseBehavior.cleansableEquipments.Length; k++)
				{
					if (equipment.equipmentIndex == ShrineCleanseBehavior.cleansableEquipments[k])
					{
						return true;
					}
				}
				j++;
			}
			return false;
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000CD8D8 File Offset: 0x000CBAD8
		private static int CleanseInventoryServer(Inventory inventory)
		{
			int num = 0;
			for (int i = 0; i < ShrineCleanseBehavior.cleansableItems.Length; i++)
			{
				ItemIndex itemIndex = ShrineCleanseBehavior.cleansableItems[i];
				int itemCount = inventory.GetItemCount(itemIndex);
				if (itemCount != 0)
				{
					inventory.RemoveItem(itemIndex, itemCount);
					num += itemCount;
				}
			}
			int num2 = 0;
			int j = 0;
			int equipmentSlotCount = inventory.GetEquipmentSlotCount();
			while (j < equipmentSlotCount)
			{
				EquipmentState equipment = inventory.GetEquipment((uint)j);
				for (int k = 0; k < ShrineCleanseBehavior.cleansableEquipments.Length; k++)
				{
					if (equipment.equipmentIndex == ShrineCleanseBehavior.cleansableEquipments[k])
					{
						inventory.SetEquipment(EquipmentState.empty, (uint)j);
						num2++;
					}
				}
				j++;
			}
			return num + num2;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000CD97C File Offset: 0x000CBB7C
		public Interactability GetInteractability(Interactor activator)
		{
			CharacterBody component = activator.GetComponent<CharacterBody>();
			if (component)
			{
				Inventory inventory = component.inventory;
				if (inventory && ShrineCleanseBehavior.InventoryIsCleansable(inventory))
				{
					return Interactability.Available;
				}
			}
			return Interactability.ConditionsNotMet;
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000CD9B4 File Offset: 0x000CBBB4
		public void OnInteractionBegin(Interactor activator)
		{
			CharacterBody component = activator.GetComponent<CharacterBody>();
			if (component)
			{
				Inventory inventory = component.inventory;
				if (inventory)
				{
					ShrineCleanseBehavior.CleanseInventoryServer(inventory);
					EffectManager.SimpleEffect(this.activationEffectPrefab, base.transform.position, base.transform.rotation, true);
				}
			}
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool ShouldIgnoreSpherecastForInteractibility(Interactor activator)
		{
			return false;
		}

		// Token: 0x0600305A RID: 12378 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public bool ShouldShowOnScanner()
		{
			return true;
		}

		// Token: 0x0600305B RID: 12379 RVA: 0x000CDA08 File Offset: 0x000CBC08
		[SystemInitializer(new Type[]
		{
			typeof(ItemCatalog),
			typeof(EquipmentCatalog)
		})]
		private static void Init()
		{
			List<ItemIndex> list = new List<ItemIndex>();
			List<EquipmentIndex> list2 = new List<EquipmentIndex>();
			ItemIndex itemIndex = (ItemIndex)0;
			ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount;
			while (itemIndex < itemCount)
			{
				ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
				if (itemDef.tier == ItemTier.Lunar || itemDef.ContainsTag(ItemTag.Cleansable))
				{
					list.Add(itemIndex);
				}
				itemIndex++;
			}
			EquipmentIndex equipmentIndex = (EquipmentIndex)0;
			EquipmentIndex equipmentCount = (EquipmentIndex)EquipmentCatalog.equipmentCount;
			while (equipmentIndex < equipmentCount)
			{
				if (EquipmentCatalog.GetEquipmentDef(equipmentIndex).isLunar)
				{
					list2.Add(equipmentIndex);
				}
				equipmentIndex++;
			}
			ShrineCleanseBehavior.cleansableItems = list.ToArray();
			ShrineCleanseBehavior.cleansableEquipments = list2.ToArray();
		}

		// Token: 0x040031FE RID: 12798
		public string contextToken;

		// Token: 0x040031FF RID: 12799
		public GameObject activationEffectPrefab;

		// Token: 0x04003200 RID: 12800
		private static ItemIndex[] cleansableItems = Array.Empty<ItemIndex>();

		// Token: 0x04003201 RID: 12801
		private static EquipmentIndex[] cleansableEquipments = Array.Empty<EquipmentIndex>();
	}
}
