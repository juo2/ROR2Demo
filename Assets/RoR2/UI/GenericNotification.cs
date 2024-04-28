using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D02 RID: 3330
	public class GenericNotification : MonoBehaviour
	{
		// Token: 0x06004BDC RID: 19420 RVA: 0x0013886E File Offset: 0x00136A6E
		public void SetNotificationT(float t)
		{
			this.canvasGroup.alpha = 1f - Mathf.Clamp01(t - this.fadeOutT) / (1f - this.fadeOutT);
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x0013889C File Offset: 0x00136A9C
		public void SetItem(ItemDef itemDef)
		{
			this.titleText.token = itemDef.nameToken;
			this.descriptionText.token = itemDef.pickupToken;
			if (itemDef.pickupIconTexture != null)
			{
				this.iconImage.texture = itemDef.pickupIconTexture;
			}
			this.titleTMP.color = ColorCatalog.GetColor(itemDef.colorIndex);
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x00138908 File Offset: 0x00136B08
		public void SetEquipment(EquipmentDef equipmentDef)
		{
			this.titleText.token = equipmentDef.nameToken;
			this.descriptionText.token = equipmentDef.pickupToken;
			if (equipmentDef.pickupIconTexture)
			{
				this.iconImage.texture = equipmentDef.pickupIconTexture;
			}
			this.titleTMP.color = ColorCatalog.GetColor(equipmentDef.colorIndex);
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x00138970 File Offset: 0x00136B70
		public void SetArtifact(ArtifactDef artifactDef)
		{
			this.titleText.token = artifactDef.nameToken;
			this.descriptionText.token = artifactDef.descriptionToken;
			this.iconImage.texture = artifactDef.smallIconSelectedSprite.texture;
			this.titleTMP.color = ColorCatalog.GetColor(ColorCatalog.ColorIndex.Artifact);
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x001389CC File Offset: 0x00136BCC
		public void SetPreviousItem(ItemDef itemDef)
		{
			if (this.previousIconImage && itemDef.pickupIconTexture)
			{
				this.previousIconImage.texture = itemDef.pickupIconTexture;
			}
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x001389F9 File Offset: 0x00136BF9
		public void SetPreviousEquipment(EquipmentDef equipmentDef)
		{
			if (this.previousIconImage && equipmentDef.pickupIconTexture)
			{
				this.previousIconImage.texture = equipmentDef.pickupIconTexture;
			}
		}

		// Token: 0x040048AD RID: 18605
		public LanguageTextMeshController titleText;

		// Token: 0x040048AE RID: 18606
		public TextMeshProUGUI titleTMP;

		// Token: 0x040048AF RID: 18607
		public LanguageTextMeshController descriptionText;

		// Token: 0x040048B0 RID: 18608
		public RawImage iconImage;

		// Token: 0x040048B1 RID: 18609
		public RawImage previousIconImage;

		// Token: 0x040048B2 RID: 18610
		public CanvasGroup canvasGroup;

		// Token: 0x040048B3 RID: 18611
		public float fadeOutT = 0.916f;
	}
}
