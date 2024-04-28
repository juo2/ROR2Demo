using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Items
{
	// Token: 0x02000BEB RID: 3051
	public static class SuppressedItemManager
	{
		// Token: 0x06004530 RID: 17712 RVA: 0x0012009C File Offset: 0x0011E29C
		[SystemInitializer(new Type[]
		{
			typeof(ItemCatalog)
		})]
		private static void Init()
		{
			Run.onRunStartGlobal += SuppressedItemManager.OnRunStart;
			Run.onRunDestroyGlobal += SuppressedItemManager.OnRunDestroyGlobal;
			Inventory.onInventoryChangedGlobal += SuppressedItemManager.OnInventoryChangedGlobal;
			RoR2Application.onFixedUpdate += SuppressedItemManager.StaticFixedUpdate;
			SuppressedItemManager.networkedInventoryPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/SuppressedItemInventory");
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06004531 RID: 17713 RVA: 0x001200FC File Offset: 0x0011E2FC
		// (set) Token: 0x06004532 RID: 17714 RVA: 0x00120103 File Offset: 0x0011E303
		public static Inventory suppressedInventory { get; private set; }

		// Token: 0x06004533 RID: 17715 RVA: 0x0012010B File Offset: 0x0011E30B
		public static bool HasItemBeenSuppressed(ItemIndex itemIndex)
		{
			return SuppressedItemManager.transformationMap.ContainsKey(itemIndex);
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x00120118 File Offset: 0x0011E318
		public static bool HasAnyItemBeenSuppressed()
		{
			return SuppressedItemManager.transformationMap.Count > 0;
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x00120128 File Offset: 0x0011E328
		public static bool SuppressItem(ItemIndex suppressedIndex, ItemIndex transformedIndex = ItemIndex.None)
		{
			if (!SuppressedItemManager.transformationMap.ContainsKey(suppressedIndex))
			{
				ItemDef itemDef = ItemCatalog.GetItemDef(suppressedIndex);
				if (itemDef)
				{
					ItemTierDef itemTierDef = ItemTierCatalog.GetItemTierDef(itemDef.tier);
					ItemIndex itemIndex = ItemIndex.None;
					bool flag = false;
					if (itemTierDef)
					{
						switch (itemTierDef.tier)
						{
						case ItemTier.Tier1:
							itemIndex = DLC1Content.Items.ScrapWhiteSuppressed.itemIndex;
							flag = (Run.instance.availableTier1DropList.Count == 1);
							break;
						case ItemTier.Tier2:
							itemIndex = DLC1Content.Items.ScrapGreenSuppressed.itemIndex;
							flag = (Run.instance.availableTier2DropList.Count == 1);
							break;
						case ItemTier.Tier3:
							itemIndex = DLC1Content.Items.ScrapRedSuppressed.itemIndex;
							flag = (Run.instance.availableTier3DropList.Count == 1);
							break;
						}
					}
					if (itemIndex != suppressedIndex)
					{
						PickupIndex pickupIndex = PickupCatalog.FindPickupIndex(suppressedIndex);
						SuppressedItemManager.transformationMap.Add(suppressedIndex, transformedIndex);
						SuppressedItemManager.suppressedInventory.GiveItem(suppressedIndex, 1);
						Run.instance.DisableItemDrop(suppressedIndex);
						if (flag)
						{
							Run.instance.EnableItemDrop(itemIndex);
						}
						if (transformedIndex != ItemIndex.None)
						{
							foreach (Inventory inventory in UnityEngine.Object.FindObjectsOfType<Inventory>())
							{
								if (inventory != SuppressedItemManager.suppressedInventory)
								{
									SuppressedItemManager.TransformItem(inventory, suppressedIndex, transformedIndex);
								}
							}
						}
						foreach (ChestBehavior chestBehavior in UnityEngine.Object.FindObjectsOfType<ChestBehavior>())
						{
							if (chestBehavior.HasRolledPickup(pickupIndex))
							{
								chestBehavior.Roll();
							}
						}
						foreach (OptionChestBehavior optionChestBehavior in UnityEngine.Object.FindObjectsOfType<OptionChestBehavior>())
						{
							if (optionChestBehavior.HasRolledPickup(pickupIndex))
							{
								optionChestBehavior.Roll();
							}
						}
						foreach (ShopTerminalBehavior shopTerminalBehavior in UnityEngine.Object.FindObjectsOfType<ShopTerminalBehavior>())
						{
							if (shopTerminalBehavior.CurrentPickupIndex() == pickupIndex)
							{
								shopTerminalBehavior.GenerateNewPickupServer();
							}
						}
						VoidSuppressorBehavior[] array5 = UnityEngine.Object.FindObjectsOfType<VoidSuppressorBehavior>();
						for (int i = 0; i < array5.Length; i++)
						{
							array5[i].RefreshItems();
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x00120326 File Offset: 0x0011E526
		private static void OnRunStart(Run run)
		{
			if (NetworkServer.active)
			{
				SuppressedItemManager.transformationMap = new Dictionary<ItemIndex, ItemIndex>();
				SuppressedItemManager.suppressedInventory = UnityEngine.Object.Instantiate<GameObject>(SuppressedItemManager.networkedInventoryPrefab).GetComponent<Inventory>();
				NetworkServer.Spawn(SuppressedItemManager.suppressedInventory.gameObject);
			}
		}

		// Token: 0x06004537 RID: 17719 RVA: 0x0012035C File Offset: 0x0011E55C
		private static void OnRunDestroyGlobal(Run run)
		{
			if (SuppressedItemManager.suppressedInventory)
			{
				NetworkServer.Destroy(SuppressedItemManager.suppressedInventory.gameObject);
			}
			SuppressedItemManager.suppressedInventory = null;
		}

		// Token: 0x06004538 RID: 17720 RVA: 0x00120380 File Offset: 0x0011E580
		private static void TransformItem(Inventory inventory, ItemIndex suppressedIndex, ItemIndex transformedIndex)
		{
			int itemCount = inventory.GetItemCount(suppressedIndex);
			if (itemCount > 0)
			{
				inventory.RemoveItem(suppressedIndex, itemCount);
				inventory.GiveItem(transformedIndex, itemCount);
				CharacterMaster component = inventory.GetComponent<CharacterMaster>();
				if (component)
				{
					CharacterMasterNotificationQueue.SendTransformNotification(component, suppressedIndex, transformedIndex, CharacterMasterNotificationQueue.TransformationType.Suppressed);
				}
			}
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x001203C4 File Offset: 0x0011E5C4
		private static void StaticFixedUpdate()
		{
			foreach (Inventory inventory in SuppressedItemManager.pendingTransformationInventories)
			{
				foreach (KeyValuePair<ItemIndex, ItemIndex> keyValuePair in SuppressedItemManager.transformationMap)
				{
					if (keyValuePair.Value != ItemIndex.None)
					{
						SuppressedItemManager.TransformItem(inventory, keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			SuppressedItemManager.pendingTransformationInventories.Clear();
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x00120474 File Offset: 0x0011E674
		private static void OnInventoryChangedGlobal(Inventory inventory)
		{
			if (inventory != SuppressedItemManager.suppressedInventory && !SuppressedItemManager.pendingTransformationInventories.Contains(inventory))
			{
				foreach (KeyValuePair<ItemIndex, ItemIndex> keyValuePair in SuppressedItemManager.transformationMap)
				{
					if (keyValuePair.Value != ItemIndex.None && inventory.GetItemCount(keyValuePair.Key) > 0)
					{
						SuppressedItemManager.pendingTransformationInventories.Add(inventory);
						break;
					}
				}
			}
		}

		// Token: 0x0400438D RID: 17293
		private static Dictionary<ItemIndex, ItemIndex> transformationMap = new Dictionary<ItemIndex, ItemIndex>();

		// Token: 0x0400438E RID: 17294
		private static GameObject networkedInventoryPrefab;

		// Token: 0x04004390 RID: 17296
		private static HashSet<Inventory> pendingTransformationInventories = new HashSet<Inventory>();
	}
}
