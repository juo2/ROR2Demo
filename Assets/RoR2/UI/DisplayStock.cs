using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CF6 RID: 3318
	public class DisplayStock : MonoBehaviour
	{
		// Token: 0x06004B8E RID: 19342 RVA: 0x001368F4 File Offset: 0x00134AF4
		private void Awake()
		{
			this.hudElement = base.GetComponent<HudElement>();
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x00136904 File Offset: 0x00134B04
		private void Update()
		{
			if (this.hudElement.targetCharacterBody)
			{
				if (!this.skillLocator)
				{
					this.skillLocator = this.hudElement.targetCharacterBody.GetComponent<SkillLocator>();
				}
				if (this.skillLocator)
				{
					GenericSkill skill = this.skillLocator.GetSkill(this.skillSlot);
					if (skill)
					{
						for (int i = 0; i < this.stockImages.Length; i++)
						{
							if (skill.stock > i)
							{
								this.stockImages[i].sprite = this.fullStockSprite;
								this.stockImages[i].color = this.fullStockColor;
							}
							else
							{
								this.stockImages[i].sprite = this.emptyStockSprite;
								this.stockImages[i].color = this.emptyStockColor;
							}
						}
					}
				}
			}
		}

		// Token: 0x04004849 RID: 18505
		public SkillSlot skillSlot;

		// Token: 0x0400484A RID: 18506
		public Image[] stockImages;

		// Token: 0x0400484B RID: 18507
		public Sprite fullStockSprite;

		// Token: 0x0400484C RID: 18508
		public Color fullStockColor;

		// Token: 0x0400484D RID: 18509
		public Sprite emptyStockSprite;

		// Token: 0x0400484E RID: 18510
		public Color emptyStockColor;

		// Token: 0x0400484F RID: 18511
		private HudElement hudElement;

		// Token: 0x04004850 RID: 18512
		private SkillLocator skillLocator;
	}
}
