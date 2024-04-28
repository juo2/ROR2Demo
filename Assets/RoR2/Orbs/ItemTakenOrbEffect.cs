using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B14 RID: 2836
	[RequireComponent(typeof(EffectComponent))]
	public class ItemTakenOrbEffect : MonoBehaviour
	{
		// Token: 0x060040BB RID: 16571 RVA: 0x0010BC98 File Offset: 0x00109E98
		private void Start()
		{
			ItemDef itemDef = ItemCatalog.GetItemDef((ItemIndex)Util.UintToIntMinusOne(base.GetComponent<EffectComponent>().effectData.genericUInt));
			ColorCatalog.ColorIndex colorIndex = ColorCatalog.ColorIndex.Error;
			Sprite sprite = null;
			if (itemDef != null)
			{
				colorIndex = itemDef.colorIndex;
				sprite = itemDef.pickupIconSprite;
			}
			Color color = ColorCatalog.GetColor(colorIndex);
			this.trailToColor.startColor = this.trailToColor.startColor * color;
			this.trailToColor.endColor = this.trailToColor.endColor * color;
			for (int i = 0; i < this.particlesToColor.Length; i++)
			{
				ParticleSystem particleSystem = this.particlesToColor[i];
				particleSystem.main.startColor = color;
				particleSystem.Play();
			}
			for (int j = 0; j < this.spritesToColor.Length; j++)
			{
				this.spritesToColor[j].color = color;
			}
			this.iconSpriteRenderer.sprite = sprite;
		}

		// Token: 0x04003F18 RID: 16152
		public TrailRenderer trailToColor;

		// Token: 0x04003F19 RID: 16153
		public ParticleSystem[] particlesToColor;

		// Token: 0x04003F1A RID: 16154
		public SpriteRenderer[] spritesToColor;

		// Token: 0x04003F1B RID: 16155
		public SpriteRenderer iconSpriteRenderer;
	}
}
