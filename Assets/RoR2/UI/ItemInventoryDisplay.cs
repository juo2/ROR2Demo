using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D26 RID: 3366
	[RequireComponent(typeof(RectTransform))]
	public class ItemInventoryDisplay : UIBehaviour, ILayoutElement
	{
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06004CAC RID: 19628 RVA: 0x0013C93E File Offset: 0x0013AB3E
		private bool isUninitialized
		{
			get
			{
				return this.rectTransform == null;
			}
		}

		// Token: 0x06004CAD RID: 19629 RVA: 0x0013C94C File Offset: 0x0013AB4C
		public void SetSubscribedInventory([CanBeNull] Inventory newInventory)
		{
			if (this.inventory == newInventory && this.inventory == this.inventoryWasValid)
			{
				return;
			}
			if (this.inventory != null)
			{
				this.inventory.onInventoryChanged -= this.OnInventoryChanged;
				this.inventory = null;
			}
			this.inventory = newInventory;
			this.inventoryWasValid = this.inventory;
			if (this.inventory)
			{
				this.inventory.onInventoryChanged += this.OnInventoryChanged;
			}
			this.OnInventoryChanged();
		}

		// Token: 0x06004CAE RID: 19630 RVA: 0x0013C9E0 File Offset: 0x0013ABE0
		private void OnInventoryChanged()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			if (this.inventory)
			{
				this.inventory.WriteItemStacks(this.itemStacks);
				this.inventory.itemAcquisitionOrder.CopyTo(this.itemOrder);
				this.itemOrderCount = this.inventory.itemAcquisitionOrder.Count;
			}
			else
			{
				Array.Clear(this.itemStacks, 0, this.itemStacks.Length);
				this.itemOrderCount = 0;
			}
			this.RequestUpdateDisplay();
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x0013CA64 File Offset: 0x0013AC64
		private static bool ItemIsVisible(ItemIndex itemIndex)
		{
			ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
			return itemDef != null && !itemDef.hidden;
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x0013CA8C File Offset: 0x0013AC8C
		private void AllocateIcons(int desiredItemCount)
		{
			if (desiredItemCount != this.itemIcons.Count)
			{
				while (this.itemIcons.Count > desiredItemCount)
				{
					UnityEngine.Object.Destroy(this.itemIcons[this.itemIcons.Count - 1].gameObject);
					this.itemIcons.RemoveAt(this.itemIcons.Count - 1);
				}
				ItemInventoryDisplay.LayoutValues layoutValues;
				this.CalculateLayoutValues(out layoutValues, desiredItemCount);
				while (this.itemIcons.Count < desiredItemCount)
				{
					ItemIcon component = UnityEngine.Object.Instantiate<GameObject>(this.itemIconPrefab, this.rectTransform).GetComponent<ItemIcon>();
					this.itemIcons.Add(component);
					this.LayoutIndividualIcon(layoutValues, this.itemIcons.Count - 1);
				}
			}
			this.OnIconCountChanged();
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x0013CB4C File Offset: 0x0013AD4C
		private float CalculateIconScale(int iconCount)
		{
			int num = (int)this.rectTransform.rect.width;
			int num2 = (int)this.maxHeight;
			int num3 = (int)this.itemIconPrefabWidth;
			int num4 = num3;
			int num5 = num3 / 8;
			int num6 = Math.Max(num / num4, 1);
			int num7 = HGMath.IntDivCeil(iconCount, num6);
			while (num4 * num7 > num2)
			{
				num6++;
				num4 = Math.Min(num / num6, num3);
				num7 = HGMath.IntDivCeil(iconCount, num6);
				if (num4 <= num5)
				{
					num4 = num5;
					break;
				}
			}
			num4 = Math.Min(num4, (int)this.maxIconWidth);
			return (float)num4 / (float)num3;
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x0013CBDC File Offset: 0x0013ADDC
		private void OnIconCountChanged()
		{
			float num = this.CalculateIconScale(this.itemIcons.Count);
			if (num != this.currentIconScale)
			{
				this.currentIconScale = num;
				this.OnIconScaleChanged();
			}
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x0013CC11 File Offset: 0x0013AE11
		private void OnIconScaleChanged()
		{
			this.LayoutAllIcons();
		}

		// Token: 0x06004CB4 RID: 19636 RVA: 0x0013CC1C File Offset: 0x0013AE1C
		private void CalculateLayoutValues(out ItemInventoryDisplay.LayoutValues v, int iconCount)
		{
			float num = this.CalculateIconScale(this.itemIcons.Count);
			Rect rect = this.rectTransform.rect;
			v.width = rect.width;
			v.iconSize = this.itemIconPrefabWidth * num;
			v.iconsPerRow = Math.Max((int)v.width / (int)v.iconSize, 1);
			v.rowWidth = (float)v.iconsPerRow * v.iconSize;
			float num2 = (v.width - v.rowWidth) * 0.5f;
			v.rowCount = HGMath.IntDivCeil(this.itemIcons.Count, v.iconsPerRow);
			v.iconLocalScale = new Vector3(num, num, 1f);
			v.topLeftCorner = new Vector3(rect.xMin + num2, rect.yMax - this.verticalMargin);
			v.height = v.iconSize * (float)v.rowCount + this.verticalMargin * 2f;
		}

		// Token: 0x06004CB5 RID: 19637 RVA: 0x0013CD18 File Offset: 0x0013AF18
		private void LayoutAllIcons()
		{
			ItemInventoryDisplay.LayoutValues layoutValues;
			this.CalculateLayoutValues(out layoutValues, this.itemIcons.Count);
			int num = this.itemIcons.Count - (layoutValues.rowCount - 1) * layoutValues.iconsPerRow;
			int i = 0;
			int num2 = 0;
			while (i < layoutValues.rowCount)
			{
				Vector3 topLeftCorner = layoutValues.topLeftCorner;
				topLeftCorner.y += (float)i * -layoutValues.iconSize;
				int num3 = layoutValues.iconsPerRow;
				if (i == layoutValues.rowCount - 1)
				{
					num3 = num;
				}
				int j = 0;
				while (j < num3)
				{
					RectTransform rectTransform = this.itemIcons[num2].rectTransform;
					rectTransform.localScale = layoutValues.iconLocalScale;
					rectTransform.localPosition = topLeftCorner;
					topLeftCorner.x += layoutValues.iconSize;
					j++;
					num2++;
				}
				i++;
			}
		}

		// Token: 0x06004CB6 RID: 19638 RVA: 0x0013CDEC File Offset: 0x0013AFEC
		private void LayoutIndividualIcon(in ItemInventoryDisplay.LayoutValues layoutValues, int i)
		{
			int num = i / layoutValues.iconsPerRow;
			int num2 = i - num * layoutValues.iconsPerRow;
			Vector3 topLeftCorner = layoutValues.topLeftCorner;
			topLeftCorner.x += (float)num2 * layoutValues.iconSize;
			topLeftCorner.y += (float)num * -layoutValues.iconSize;
			RectTransform rectTransform = this.itemIcons[i].rectTransform;
			rectTransform.localPosition = topLeftCorner;
			rectTransform.localScale = layoutValues.iconLocalScale;
		}

		// Token: 0x06004CB7 RID: 19639 RVA: 0x0013CE60 File Offset: 0x0013B060
		private void CacheComponents()
		{
			this.rectTransform = (RectTransform)base.transform;
		}

		// Token: 0x06004CB8 RID: 19640 RVA: 0x0013CE73 File Offset: 0x0013B073
		protected override void Awake()
		{
			base.Awake();
			this.CacheComponents();
			this.itemStacks = ItemCatalog.RequestItemStackArray();
			this.itemOrder = ItemCatalog.RequestItemOrderBuffer();
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x0013CE97 File Offset: 0x0013B097
		protected override void OnDestroy()
		{
			this.SetSubscribedInventory(null);
			ItemCatalog.ReturnItemStackArray(this.itemStacks);
			this.itemStacks = null;
			ItemCatalog.ReturnItemOrderBuffer(this.itemOrder);
			this.itemOrder = null;
			base.OnDestroy();
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x0013CECA File Offset: 0x0013B0CA
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.inventory)
			{
				this.OnInventoryChanged();
			}
			this.RequestUpdateDisplay();
			this.LayoutAllIcons();
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x0013CEF1 File Offset: 0x0013B0F1
		private void RequestUpdateDisplay()
		{
			if (!this.updateRequestPending)
			{
				this.updateRequestPending = true;
				RoR2Application.onNextUpdate += this.UpdateDisplay;
			}
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x0013CF14 File Offset: 0x0013B114
		public void UpdateDisplay()
		{
			this.updateRequestPending = false;
			if (!this || !base.isActiveAndEnabled)
			{
				return;
			}
			ItemIndex[] array = ItemCatalog.RequestItemOrderBuffer();
			int num = 0;
			for (int i = 0; i < this.itemOrderCount; i++)
			{
				if (ItemInventoryDisplay.ItemIsVisible(this.itemOrder[i]))
				{
					array[num++] = this.itemOrder[i];
				}
			}
			this.AllocateIcons(num);
			for (int j = 0; j < num; j++)
			{
				ItemIndex itemIndex = array[j];
				this.itemIcons[j].SetItemIndex(itemIndex, this.itemStacks[(int)itemIndex]);
			}
			ItemCatalog.ReturnItemOrderBuffer(array);
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x0013CFAC File Offset: 0x0013B1AC
		public void SetItems(List<ItemIndex> newItemOrder, int[] newItemStacks)
		{
			this.itemOrderCount = newItemOrder.Count;
			for (int i = 0; i < this.itemOrderCount; i++)
			{
				this.itemOrder[i] = newItemOrder[i];
			}
			Array.Copy(newItemStacks, this.itemStacks, this.itemStacks.Length);
			this.RequestUpdateDisplay();
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x0013CFFF File Offset: 0x0013B1FF
		public void SetItems(ItemIndex[] newItemOrder, int newItemOrderCount, int[] newItemStacks)
		{
			this.itemOrderCount = newItemOrderCount;
			Array.Copy(newItemOrder, this.itemOrder, this.itemOrderCount);
			Array.Copy(newItemStacks, this.itemStacks, this.itemStacks.Length);
			this.RequestUpdateDisplay();
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x0013D034 File Offset: 0x0013B234
		public void ResetItems()
		{
			this.itemOrderCount = 0;
			Array.Clear(this.itemStacks, 0, this.itemStacks.Length);
			this.RequestUpdateDisplay();
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x0013D058 File Offset: 0x0013B258
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (this.rectTransform)
			{
				float width = this.rectTransform.rect.width;
				if (width != this.previousWidth)
				{
					this.previousWidth = width;
					this.LayoutAllIcons();
				}
			}
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x000026ED File Offset: 0x000008ED
		public void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x0013D0A4 File Offset: 0x0013B2A4
		public void CalculateLayoutInputVertical()
		{
			if (this.isUninitialized)
			{
				return;
			}
			ItemInventoryDisplay.LayoutValues layoutValues;
			this.CalculateLayoutValues(out layoutValues, this.itemIcons.Count);
			this.preferredHeight = layoutValues.height;
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06004CC3 RID: 19651 RVA: 0x0013D0D9 File Offset: 0x0013B2D9
		public float minWidth
		{
			get
			{
				return this.preferredWidth;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06004CC4 RID: 19652 RVA: 0x0013D0E4 File Offset: 0x0013B2E4
		public float preferredWidth
		{
			get
			{
				return this.rectTransform.rect.width;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06004CC5 RID: 19653 RVA: 0x000137EE File Offset: 0x000119EE
		public float flexibleWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06004CC6 RID: 19654 RVA: 0x0013D104 File Offset: 0x0013B304
		public float minHeight
		{
			get
			{
				return this.preferredHeight;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06004CC7 RID: 19655 RVA: 0x0013D10C File Offset: 0x0013B30C
		// (set) Token: 0x06004CC8 RID: 19656 RVA: 0x0013D114 File Offset: 0x0013B314
		public float preferredHeight { get; set; }

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06004CC9 RID: 19657 RVA: 0x000137EE File Offset: 0x000119EE
		public float flexibleHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06004CCA RID: 19658 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06004CCB RID: 19659 RVA: 0x0013D11D File Offset: 0x0013B31D
		protected void OnValidate()
		{
			this.CacheComponents();
		}

		// Token: 0x040049B8 RID: 18872
		private RectTransform rectTransform;

		// Token: 0x040049B9 RID: 18873
		public GameObject itemIconPrefab;

		// Token: 0x040049BA RID: 18874
		[FormerlySerializedAs("iconWidth")]
		public float itemIconPrefabWidth = 64f;

		// Token: 0x040049BB RID: 18875
		public float maxIconWidth = 64f;

		// Token: 0x040049BC RID: 18876
		public float maxHeight = 128f;

		// Token: 0x040049BD RID: 18877
		public float verticalMargin = 8f;

		// Token: 0x040049BE RID: 18878
		[HideInInspector]
		[SerializeField]
		private List<ItemIcon> itemIcons;

		// Token: 0x040049BF RID: 18879
		private ItemIndex[] itemOrder;

		// Token: 0x040049C0 RID: 18880
		private int itemOrderCount;

		// Token: 0x040049C1 RID: 18881
		private int[] itemStacks;

		// Token: 0x040049C2 RID: 18882
		private float currentIconScale = 1f;

		// Token: 0x040049C3 RID: 18883
		private float previousWidth;

		// Token: 0x040049C4 RID: 18884
		private bool updateRequestPending;

		// Token: 0x040049C5 RID: 18885
		private Inventory inventory;

		// Token: 0x040049C6 RID: 18886
		private bool inventoryWasValid;

		// Token: 0x02000D27 RID: 3367
		private struct LayoutValues
		{
			// Token: 0x040049C8 RID: 18888
			public float width;

			// Token: 0x040049C9 RID: 18889
			public float height;

			// Token: 0x040049CA RID: 18890
			public float iconSize;

			// Token: 0x040049CB RID: 18891
			public int iconsPerRow;

			// Token: 0x040049CC RID: 18892
			public float rowWidth;

			// Token: 0x040049CD RID: 18893
			public int rowCount;

			// Token: 0x040049CE RID: 18894
			public Vector3 iconLocalScale;

			// Token: 0x040049CF RID: 18895
			public Vector3 topLeftCorner;
		}
	}
}
