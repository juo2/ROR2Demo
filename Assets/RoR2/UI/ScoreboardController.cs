using System;
using System.Collections.ObjectModel;
using RoR2.Items;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D7B RID: 3451
	public class ScoreboardController : MonoBehaviour
	{
		// Token: 0x06004F20 RID: 20256 RVA: 0x0014782C File Offset: 0x00145A2C
		private void Awake()
		{
			this.stripAllocator = new UIElementAllocator<ScoreboardStrip>(this.container, this.stripPrefab, true, false);
		}

		// Token: 0x06004F21 RID: 20257 RVA: 0x00147847 File Offset: 0x00145A47
		private void SetStripCount(int newCount)
		{
			this.stripAllocator.AllocateElements(newCount);
		}

		// Token: 0x06004F22 RID: 20258 RVA: 0x00147858 File Offset: 0x00145A58
		private void Rebuild()
		{
			ReadOnlyCollection<PlayerCharacterMasterController> instances = PlayerCharacterMasterController.instances;
			int count = instances.Count;
			this.SetStripCount(count);
			for (int i = 0; i < count; i++)
			{
				this.stripAllocator.elements[i].SetMaster(instances[i].master);
			}
		}

		// Token: 0x06004F23 RID: 20259 RVA: 0x001478A7 File Offset: 0x00145AA7
		private void PlayerEventToRebuild(PlayerCharacterMasterController playerCharacterMasterController)
		{
			this.Rebuild();
		}

		// Token: 0x06004F24 RID: 20260 RVA: 0x001478B0 File Offset: 0x00145AB0
		private void OnEnable()
		{
			if (SuppressedItemManager.suppressedInventory)
			{
				ItemInventoryDisplay itemInventoryDisplay = this.suppressedItemDisplay;
				if (itemInventoryDisplay != null)
				{
					itemInventoryDisplay.SetSubscribedInventory(SuppressedItemManager.suppressedInventory);
				}
				SuppressedItemManager.suppressedInventory.onInventoryChanged += this.OnInventoryChanged;
			}
			this.OnInventoryChanged();
			PlayerCharacterMasterController.onPlayerAdded += this.PlayerEventToRebuild;
			PlayerCharacterMasterController.onPlayerRemoved += this.PlayerEventToRebuild;
			this.Rebuild();
		}

		// Token: 0x06004F25 RID: 20261 RVA: 0x00147924 File Offset: 0x00145B24
		private void OnDisable()
		{
			if (SuppressedItemManager.suppressedInventory)
			{
				ItemInventoryDisplay itemInventoryDisplay = this.suppressedItemDisplay;
				if (itemInventoryDisplay != null)
				{
					itemInventoryDisplay.SetSubscribedInventory(null);
				}
				SuppressedItemManager.suppressedInventory.onInventoryChanged -= this.OnInventoryChanged;
			}
			PlayerCharacterMasterController.onPlayerRemoved -= this.PlayerEventToRebuild;
			PlayerCharacterMasterController.onPlayerAdded -= this.PlayerEventToRebuild;
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x00147987 File Offset: 0x00145B87
		private void OnInventoryChanged()
		{
			ItemInventoryDisplay itemInventoryDisplay = this.suppressedItemDisplay;
			if (itemInventoryDisplay == null)
			{
				return;
			}
			GameObject gameObject = itemInventoryDisplay.gameObject;
			if (gameObject == null)
			{
				return;
			}
			gameObject.SetActive(SuppressedItemManager.HasAnyItemBeenSuppressed());
		}

		// Token: 0x04004BD6 RID: 19414
		public GameObject stripPrefab;

		// Token: 0x04004BD7 RID: 19415
		public RectTransform container;

		// Token: 0x04004BD8 RID: 19416
		[SerializeField]
		private ItemInventoryDisplay suppressedItemDisplay;

		// Token: 0x04004BD9 RID: 19417
		private UIElementAllocator<ScoreboardStrip> stripAllocator;
	}
}
