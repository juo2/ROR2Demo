using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D25 RID: 3365
	[RequireComponent(typeof(RectTransform))]
	public class ItemIcon : MonoBehaviour
	{
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06004CA6 RID: 19622 RVA: 0x0013C7B0 File Offset: 0x0013A9B0
		// (set) Token: 0x06004CA7 RID: 19623 RVA: 0x0013C7B8 File Offset: 0x0013A9B8
		public RectTransform rectTransform { get; private set; }

		// Token: 0x06004CA8 RID: 19624 RVA: 0x0013C7C1 File Offset: 0x0013A9C1
		private void Awake()
		{
			this.CacheRectTransform();
		}

		// Token: 0x06004CA9 RID: 19625 RVA: 0x0013C7C9 File Offset: 0x0013A9C9
		public void CacheRectTransform()
		{
			if (this.rectTransform == null)
			{
				this.rectTransform = (RectTransform)base.transform;
			}
		}

		// Token: 0x06004CAA RID: 19626 RVA: 0x0013C7E4 File Offset: 0x0013A9E4
		public void SetItemIndex(ItemIndex newItemIndex, int newItemCount)
		{
			if (this.itemIndex == newItemIndex && this.itemCount == newItemCount)
			{
				return;
			}
			this.itemIndex = newItemIndex;
			this.itemCount = newItemCount;
			string titleToken = "";
			string bodyToken = "";
			Color color = Color.white;
			Color bodyColor = new Color(0.6f, 0.6f, 0.6f, 1f);
			ItemDef itemDef = ItemCatalog.GetItemDef(this.itemIndex);
			if (itemDef != null)
			{
				this.image.texture = itemDef.pickupIconTexture;
				if (this.itemCount > 1)
				{
					this.stackText.enabled = true;
					this.stackText.text = string.Format("x{0}", this.itemCount);
				}
				else
				{
					this.stackText.enabled = false;
				}
				titleToken = itemDef.nameToken;
				bodyToken = itemDef.pickupToken;
				color = ColorCatalog.GetColor(itemDef.darkColorIndex);
			}
			if (this.glowImage)
			{
				this.glowImage.color = new Color(color.r, color.g, color.b, 0.75f);
			}
			if (this.tooltipProvider)
			{
				this.tooltipProvider.titleToken = titleToken;
				this.tooltipProvider.bodyToken = bodyToken;
				this.tooltipProvider.titleColor = color;
				this.tooltipProvider.bodyColor = bodyColor;
			}
		}

		// Token: 0x040049B2 RID: 18866
		public RawImage glowImage;

		// Token: 0x040049B3 RID: 18867
		public RawImage image;

		// Token: 0x040049B4 RID: 18868
		public TextMeshProUGUI stackText;

		// Token: 0x040049B5 RID: 18869
		public TooltipProvider tooltipProvider;

		// Token: 0x040049B6 RID: 18870
		private ItemIndex itemIndex;

		// Token: 0x040049B7 RID: 18871
		private int itemCount;
	}
}
