using System;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CE0 RID: 3296
	[RequireComponent(typeof(HudElement))]
	[RequireComponent(typeof(RectTransform))]
	public class CrosshairController : MonoBehaviour
	{
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06004B29 RID: 19241 RVA: 0x00134E0D File Offset: 0x0013300D
		// (set) Token: 0x06004B2A RID: 19242 RVA: 0x00134E15 File Offset: 0x00133015
		public RectTransform rectTransform { get; private set; }

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06004B2B RID: 19243 RVA: 0x00134E1E File Offset: 0x0013301E
		// (set) Token: 0x06004B2C RID: 19244 RVA: 0x00134E26 File Offset: 0x00133026
		public HudElement hudElement { get; private set; }

		// Token: 0x06004B2D RID: 19245 RVA: 0x00134E2F File Offset: 0x0013302F
		private void Awake()
		{
			this.rectTransform = base.GetComponent<RectTransform>();
			this.hudElement = base.GetComponent<HudElement>();
			this.SetCrosshairSpread();
			this.SetSkillStockDisplays();
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x00134E58 File Offset: 0x00133058
		private void SetCrosshairSpread()
		{
			float num = 0f;
			if (this.hudElement.targetCharacterBody)
			{
				num = this.hudElement.targetCharacterBody.spreadBloomAngle;
			}
			for (int i = 0; i < this.spriteSpreadPositions.Length; i++)
			{
				CrosshairController.SpritePosition spritePosition = this.spriteSpreadPositions[i];
				spritePosition.target.localPosition = Vector3.Lerp(spritePosition.zeroPosition, spritePosition.onePosition, num / this.maxSpreadAngle);
			}
			for (int j = 0; j < this.remapSprites.Length; j++)
			{
				this.remapSprites[j].color = new Color(1f, 1f, 1f, Util.Remap(num / this.maxSpreadAngle, 0f, 1f, this.minSpreadAlpha, this.maxSpreadAlpha));
			}
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x00134F2C File Offset: 0x0013312C
		private void SetSkillStockDisplays()
		{
			if (this.hudElement.targetCharacterBody)
			{
				SkillLocator component = this.hudElement.targetCharacterBody.GetComponent<SkillLocator>();
				for (int i = 0; i < this.skillStockSpriteDisplays.Length; i++)
				{
					bool active = false;
					CrosshairController.SkillStockSpriteDisplay skillStockSpriteDisplay = this.skillStockSpriteDisplays[i];
					GenericSkill skill = component.GetSkill(skillStockSpriteDisplay.skillSlot);
					if (skill && skill.stock >= skillStockSpriteDisplay.minimumStockCountToBeValid && (skill.stock <= skillStockSpriteDisplay.maximumStockCountToBeValid || skillStockSpriteDisplay.maximumStockCountToBeValid < 0) && (skillStockSpriteDisplay.requiredSkillDef == null || skill.skillDef == skillStockSpriteDisplay.requiredSkillDef))
					{
						active = true;
					}
					skillStockSpriteDisplay.target.SetActive(active);
				}
			}
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x00134FF5 File Offset: 0x001331F5
		private void LateUpdate()
		{
			this.SetCrosshairSpread();
			this.SetSkillStockDisplays();
		}

		// Token: 0x040047DC RID: 18396
		public CrosshairController.SpritePosition[] spriteSpreadPositions;

		// Token: 0x040047DD RID: 18397
		public CrosshairController.SkillStockSpriteDisplay[] skillStockSpriteDisplays;

		// Token: 0x040047DE RID: 18398
		public RawImage[] remapSprites;

		// Token: 0x040047DF RID: 18399
		public float minSpreadAlpha;

		// Token: 0x040047E0 RID: 18400
		public float maxSpreadAlpha;

		// Token: 0x040047E1 RID: 18401
		[Tooltip("The angle the crosshair represents when alpha = 1")]
		public float maxSpreadAngle;

		// Token: 0x040047E2 RID: 18402
		private MaterialPropertyBlock _propBlock;

		// Token: 0x02000CE1 RID: 3297
		[Serializable]
		public struct SpritePosition
		{
			// Token: 0x040047E3 RID: 18403
			public RectTransform target;

			// Token: 0x040047E4 RID: 18404
			public Vector3 zeroPosition;

			// Token: 0x040047E5 RID: 18405
			public Vector3 onePosition;
		}

		// Token: 0x02000CE2 RID: 3298
		[Serializable]
		public struct SkillStockSpriteDisplay
		{
			// Token: 0x040047E6 RID: 18406
			public GameObject target;

			// Token: 0x040047E7 RID: 18407
			public SkillSlot skillSlot;

			// Token: 0x040047E8 RID: 18408
			public SkillDef requiredSkillDef;

			// Token: 0x040047E9 RID: 18409
			public int minimumStockCountToBeValid;

			// Token: 0x040047EA RID: 18410
			public int maximumStockCountToBeValid;
		}
	}
}
