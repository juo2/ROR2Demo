using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D8A RID: 3466
	public class SkillIcon : MonoBehaviour
	{
		// Token: 0x06004F67 RID: 20327 RVA: 0x001485C0 File Offset: 0x001467C0
		private void Update()
		{
			if (this.targetSkill)
			{
				if (this.tooltipProvider)
				{
					Color color = this.targetSkill.characterBody.bodyColor;
					SurvivorCatalog.GetSurvivorIndexFromBodyIndex(this.targetSkill.characterBody.bodyIndex);
					float h;
					float s;
					float num;
					Color.RGBToHSV(color, out h, out s, out num);
					num = ((num > 0.7f) ? 0.7f : num);
					color = Color.HSVToRGB(h, s, num);
					this.tooltipProvider.titleColor = color;
					this.tooltipProvider.titleToken = this.targetSkill.skillNameToken;
					this.tooltipProvider.bodyToken = this.targetSkill.skillDescriptionToken;
				}
				float cooldownRemaining = this.targetSkill.cooldownRemaining;
				float num2 = this.targetSkill.CalculateFinalRechargeInterval();
				int stock = this.targetSkill.stock;
				bool flag = stock > 0 || cooldownRemaining == 0f;
				bool flag2 = this.targetSkill.IsReady();
				if (this.previousStock < stock)
				{
					Util.PlaySound("Play_UI_cooldownRefresh", RoR2Application.instance.gameObject);
				}
				if (this.animator)
				{
					if (this.targetSkill.maxStock > 1)
					{
						this.animator.SetBool(this.animatorStackString, true);
					}
					else
					{
						this.animator.SetBool(this.animatorStackString, false);
					}
				}
				if (this.isReadyPanelObject)
				{
					this.isReadyPanelObject.SetActive(flag2);
				}
				if (!this.wasReady && flag && this.flashPanelObject)
				{
					this.flashPanelObject.SetActive(true);
				}
				if (this.cooldownText)
				{
					if (flag)
					{
						this.cooldownText.gameObject.SetActive(false);
					}
					else
					{
						SkillIcon.sharedStringBuilder.Clear();
						SkillIcon.sharedStringBuilder.AppendInt(Mathf.CeilToInt(cooldownRemaining), 1U, uint.MaxValue);
						this.cooldownText.SetText(SkillIcon.sharedStringBuilder);
						this.cooldownText.gameObject.SetActive(true);
					}
				}
				if (this.iconImage)
				{
					this.iconImage.enabled = true;
					this.iconImage.color = (flag2 ? Color.white : Color.gray);
					this.iconImage.sprite = this.targetSkill.icon;
				}
				if (this.cooldownRemapPanel)
				{
					float num3 = 1f;
					if (num2 >= Mathf.Epsilon)
					{
						num3 = 1f - cooldownRemaining / num2;
					}
					float num4 = num3;
					this.cooldownRemapPanel.enabled = (num4 < 1f);
					this.cooldownRemapPanel.color = new Color(1f, 1f, 1f, num3);
				}
				if (this.stockText)
				{
					if (this.targetSkill.maxStock > 1)
					{
						this.stockText.gameObject.SetActive(true);
						SkillIcon.sharedStringBuilder.Clear();
						SkillIcon.sharedStringBuilder.AppendInt(this.targetSkill.stock, 1U, uint.MaxValue);
						this.stockText.SetText(SkillIcon.sharedStringBuilder);
					}
					else
					{
						this.stockText.gameObject.SetActive(false);
					}
				}
				this.wasReady = flag;
				this.previousStock = stock;
				return;
			}
			if (this.tooltipProvider)
			{
				this.tooltipProvider.bodyColor = Color.gray;
				this.tooltipProvider.titleToken = "";
				this.tooltipProvider.bodyToken = "";
			}
			if (this.cooldownText)
			{
				this.cooldownText.gameObject.SetActive(false);
			}
			if (this.stockText)
			{
				this.stockText.gameObject.SetActive(false);
			}
			if (this.iconImage)
			{
				this.iconImage.enabled = false;
				this.iconImage.sprite = null;
			}
		}

		// Token: 0x04004C0C RID: 19468
		public SkillSlot targetSkillSlot;

		// Token: 0x04004C0D RID: 19469
		public PlayerCharacterMasterController playerCharacterMasterController;

		// Token: 0x04004C0E RID: 19470
		public GenericSkill targetSkill;

		// Token: 0x04004C0F RID: 19471
		public Image iconImage;

		// Token: 0x04004C10 RID: 19472
		public RawImage cooldownRemapPanel;

		// Token: 0x04004C11 RID: 19473
		public TextMeshProUGUI cooldownText;

		// Token: 0x04004C12 RID: 19474
		public TextMeshProUGUI stockText;

		// Token: 0x04004C13 RID: 19475
		public GameObject flashPanelObject;

		// Token: 0x04004C14 RID: 19476
		public GameObject isReadyPanelObject;

		// Token: 0x04004C15 RID: 19477
		public Animator animator;

		// Token: 0x04004C16 RID: 19478
		public string animatorStackString;

		// Token: 0x04004C17 RID: 19479
		public bool wasReady;

		// Token: 0x04004C18 RID: 19480
		public int previousStock;

		// Token: 0x04004C19 RID: 19481
		public TooltipProvider tooltipProvider;

		// Token: 0x04004C1A RID: 19482
		private static readonly StringBuilder sharedStringBuilder = new StringBuilder();
	}
}
