using System;
using System.Collections.Generic;
using HG;
using UnityEngine.Networking;

namespace RoR2.Items
{
	// Token: 0x02000BD3 RID: 3027
	public static class ContagiousItemManager
	{
		// Token: 0x060044C8 RID: 17608 RVA: 0x0011E65D File Offset: 0x0011C85D
		[SystemInitializer(new Type[]
		{
			typeof(ItemCatalog)
		})]
		private static void Init()
		{
			ContagiousItemManager.InitTransformationTable();
			Inventory.onInventoryChangedGlobal += ContagiousItemManager.OnInventoryChangedGlobal;
			RoR2Application.onFixedUpdate += ContagiousItemManager.StaticFixedUpdate;
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x0011E686 File Offset: 0x0011C886
		private static void StaticFixedUpdate()
		{
			if (ContagiousItemManager.pendingChanges.Count > 0)
			{
				ContagiousItemManager.ProcessPendingChanges();
			}
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x0011E69C File Offset: 0x0011C89C
		private static void InitTransformationTable()
		{
			ContagiousItemManager.originalToTransformed = new ItemIndex[ItemCatalog.itemCount];
			ItemIndex[] array = ContagiousItemManager.originalToTransformed;
			ItemIndex itemIndex = ItemIndex.None;
			ArrayUtils.SetAll<ItemIndex>(array, itemIndex);
			ContagiousItemManager.itemsToCheck = new bool[ItemCatalog.itemCount];
			foreach (ItemDef.Pair pair in ItemCatalog.GetItemPairsForRelationship(DLC1Content.ItemRelationshipTypes.ContagiousItem))
			{
				ContagiousItemManager.originalToTransformed[(int)pair.itemDef1.itemIndex] = pair.itemDef2.itemIndex;
				ContagiousItemManager.itemsToCheck[(int)pair.itemDef1.itemIndex] = true;
				ContagiousItemManager.itemsToCheck[(int)pair.itemDef2.itemIndex] = true;
				ContagiousItemManager.TransformationInfo transformationInfo = default(ContagiousItemManager.TransformationInfo);
				transformationInfo.originalItem = pair.itemDef1.itemIndex;
				transformationInfo.transformedItem = pair.itemDef2.itemIndex;
				ArrayUtils.ArrayAppend<ContagiousItemManager.TransformationInfo>(ref ContagiousItemManager._transformationInfos, transformationInfo);
			}
			ContagiousItemManager.transformationInfos = new ReadOnlyArray<ContagiousItemManager.TransformationInfo>(ContagiousItemManager._transformationInfos);
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x0011E7A8 File Offset: 0x0011C9A8
		public static ItemIndex GetTransformedItemIndex(ItemIndex itemIndex)
		{
			if (itemIndex >= (ItemIndex)0 && itemIndex < (ItemIndex)ContagiousItemManager.originalToTransformed.Length)
			{
				return ContagiousItemManager.originalToTransformed[(int)itemIndex];
			}
			return ItemIndex.None;
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x0011E7C4 File Offset: 0x0011C9C4
		public static ItemIndex GetOriginalItemIndex(ItemIndex transformedItemIndex)
		{
			foreach (ContagiousItemManager.TransformationInfo transformationInfo in ContagiousItemManager._transformationInfos)
			{
				if (transformedItemIndex == transformationInfo.transformedItem)
				{
					return transformationInfo.originalItem;
				}
			}
			return ItemIndex.None;
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x0011E800 File Offset: 0x0011CA00
		private static int FindInventoryReplacementCandidateIndex(Inventory inventory, ItemIndex originalItem)
		{
			for (int i = 0; i < ContagiousItemManager.pendingChanges.Count; i++)
			{
				ContagiousItemManager.InventoryReplacementCandidate inventoryReplacementCandidate = ContagiousItemManager.pendingChanges[i];
				if (inventoryReplacementCandidate.inventory == inventory && inventoryReplacementCandidate.originalItem == originalItem)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x0011E844 File Offset: 0x0011CA44
		private static void ProcessPendingChanges()
		{
			if (!NetworkServer.active || !Run.instance)
			{
				ContagiousItemManager.pendingChanges.Clear();
				return;
			}
			for (int i = ContagiousItemManager.pendingChanges.Count - 1; i >= 0; i--)
			{
				ContagiousItemManager.InventoryReplacementCandidate inventoryReplacementCandidate = ContagiousItemManager.pendingChanges[i];
				if (inventoryReplacementCandidate.time.hasPassed)
				{
					if (!ContagiousItemManager.StepInventoryInfection(inventoryReplacementCandidate.inventory, inventoryReplacementCandidate.originalItem, 2147483647, inventoryReplacementCandidate.isForced))
					{
						ContagiousItemManager.pendingChanges.RemoveAt(i);
					}
					else
					{
						inventoryReplacementCandidate.time = Run.FixedTimeStamp.now + ContagiousItemManager.transformDelay;
						ContagiousItemManager.pendingChanges[i] = inventoryReplacementCandidate;
					}
				}
			}
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x0011E8F0 File Offset: 0x0011CAF0
		private static void OnInventoryChangedGlobal(Inventory inventory)
		{
			if (!NetworkServer.active)
			{
				return;
			}
			for (int i = 0; i < ContagiousItemManager._transformationInfos.Length; i++)
			{
				ref ContagiousItemManager.TransformationInfo ptr = ref ContagiousItemManager._transformationInfos[i];
				if (inventory.GetItemCount(ptr.transformedItem) > 0)
				{
					ContagiousItemManager.TryQueueReplacement(inventory, ptr.originalItem, ptr.transformedItem, false);
				}
			}
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x0011E948 File Offset: 0x0011CB48
		private static void TryQueueReplacement(Inventory inventory, ItemIndex originalItemIndex, ItemIndex transformedItemIndex, bool isForced)
		{
			if (inventory.GetItemCount(originalItemIndex) > 0 && ContagiousItemManager.FindInventoryReplacementCandidateIndex(inventory, transformedItemIndex) == -1)
			{
				ContagiousItemManager.pendingChanges.Add(new ContagiousItemManager.InventoryReplacementCandidate
				{
					inventory = inventory,
					originalItem = originalItemIndex,
					time = Run.FixedTimeStamp.now + ContagiousItemManager.transformDelay,
					isForced = isForced
				});
			}
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x0011E9AC File Offset: 0x0011CBAC
		public static void TryForceReplacement(Inventory inventory, ItemIndex originalItemIndex)
		{
			ItemIndex transformedItemIndex = ContagiousItemManager.GetTransformedItemIndex(originalItemIndex);
			if (transformedItemIndex != ItemIndex.None && Run.instance.IsItemAvailable(transformedItemIndex))
			{
				ContagiousItemManager.TryQueueReplacement(inventory, originalItemIndex, transformedItemIndex, true);
			}
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x0011E9DC File Offset: 0x0011CBDC
		private static bool StepInventoryInfection(Inventory inventory, ItemIndex originalItem, int limit, bool isForced)
		{
			ItemIndex itemIndex = ContagiousItemManager.originalToTransformed[(int)originalItem];
			int itemCount = inventory.GetItemCount(itemIndex);
			if (isForced || itemCount > 0)
			{
				int itemCount2 = inventory.GetItemCount(originalItem);
				int num = Math.Min(limit, itemCount2);
				if (num > 0)
				{
					inventory.RemoveItem(originalItem, num);
					inventory.GiveItem(itemIndex, num);
					CharacterMaster component = inventory.GetComponent<CharacterMaster>();
					if (component)
					{
						CharacterMasterNotificationQueue.SendTransformNotification(component, originalItem, itemIndex, CharacterMasterNotificationQueue.TransformationType.ContagiousVoid);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x04004342 RID: 17218
		public static readonly float transformDelay = 0.5f;

		// Token: 0x04004343 RID: 17219
		private static ItemIndex[] originalToTransformed = Array.Empty<ItemIndex>();

		// Token: 0x04004344 RID: 17220
		private static bool[] itemsToCheck = Array.Empty<bool>();

		// Token: 0x04004345 RID: 17221
		private static ContagiousItemManager.TransformationInfo[] _transformationInfos = Array.Empty<ContagiousItemManager.TransformationInfo>();

		// Token: 0x04004346 RID: 17222
		public static ReadOnlyArray<ContagiousItemManager.TransformationInfo> transformationInfos = new ReadOnlyArray<ContagiousItemManager.TransformationInfo>(ContagiousItemManager._transformationInfos);

		// Token: 0x04004347 RID: 17223
		private static List<ContagiousItemManager.InventoryReplacementCandidate> pendingChanges = new List<ContagiousItemManager.InventoryReplacementCandidate>();

		// Token: 0x02000BD4 RID: 3028
		public struct TransformationInfo
		{
			// Token: 0x04004348 RID: 17224
			public ItemIndex originalItem;

			// Token: 0x04004349 RID: 17225
			public ItemIndex transformedItem;
		}

		// Token: 0x02000BD5 RID: 3029
		private struct InventoryReplacementCandidate
		{
			// Token: 0x0400434A RID: 17226
			public Inventory inventory;

			// Token: 0x0400434B RID: 17227
			public ItemIndex originalItem;

			// Token: 0x0400434C RID: 17228
			public Run.FixedTimeStamp time;

			// Token: 0x0400434D RID: 17229
			public bool isForced;
		}
	}
}
