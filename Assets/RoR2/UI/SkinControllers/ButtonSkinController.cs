using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI.SkinControllers
{
	// Token: 0x02000DC0 RID: 3520
	[RequireComponent(typeof(Button))]
	public class ButtonSkinController : BaseSkinController
	{
		// Token: 0x0600508F RID: 20623 RVA: 0x0014D231 File Offset: 0x0014B431
		private void CacheComponents()
		{
			this.button = base.GetComponent<Button>();
			this.layoutElement = base.GetComponent<LayoutElement>();
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x0014D24B File Offset: 0x0014B44B
		protected new void Awake()
		{
			this.CacheComponents();
			base.Awake();
		}

		// Token: 0x06005091 RID: 20625 RVA: 0x0014D259 File Offset: 0x0014B459
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			RoR2Application.onUpdate += ButtonSkinController.StaticUpdate;
		}

		// Token: 0x06005092 RID: 20626 RVA: 0x0014D26C File Offset: 0x0014B46C
		private void OnEnable()
		{
			ButtonSkinController.instancesList.Add(this);
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x0014D279 File Offset: 0x0014B479
		private void OnDisable()
		{
			ButtonSkinController.instancesList.Remove(this);
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x0014D288 File Offset: 0x0014B488
		private static void StaticUpdate()
		{
			foreach (ButtonSkinController buttonSkinController in ButtonSkinController.instancesList)
			{
				buttonSkinController.UpdateLabelStyle(ref buttonSkinController.skinData.buttonStyle);
			}
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x0014D2E4 File Offset: 0x0014B4E4
		private void UpdateLabelStyle(ref UISkinData.ButtonStyle buttonStyle)
		{
			if (this.useRecommendedLabel)
			{
				TextMeshProUGUI componentInChildren = this.button.GetComponentInChildren<TextMeshProUGUI>();
				if (componentInChildren)
				{
					if (this.button.interactable)
					{
						buttonStyle.interactableTextStyle.Apply(componentInChildren, this.useRecommendedAlignment);
						return;
					}
					buttonStyle.disabledTextStyle.Apply(componentInChildren, this.useRecommendedAlignment);
				}
			}
		}

		// Token: 0x06005096 RID: 20630 RVA: 0x0014D33F File Offset: 0x0014B53F
		protected override void OnSkinUI()
		{
			this.ApplyButtonStyle(ref this.skinData.buttonStyle);
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x0014D354 File Offset: 0x0014B554
		private void ApplyButtonStyle(ref UISkinData.ButtonStyle buttonStyle)
		{
			if (this.useRecommendedMaterial)
			{
				this.button.image.material = buttonStyle.material;
			}
			this.button.colors = buttonStyle.colors;
			if (this.useRecommendedImage)
			{
				this.button.image.sprite = buttonStyle.sprite;
			}
			if (this.useRecommendedButtonWidth)
			{
				((RectTransform)base.transform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, buttonStyle.recommendedWidth);
			}
			if (this.useRecommendedButtonHeight)
			{
				((RectTransform)base.transform).SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, buttonStyle.recommendedHeight);
			}
			if (this.layoutElement)
			{
				if (this.useRecommendedButtonWidth)
				{
					this.layoutElement.preferredWidth = buttonStyle.recommendedWidth;
				}
				if (this.useRecommendedButtonHeight)
				{
					this.layoutElement.preferredHeight = buttonStyle.recommendedHeight;
				}
			}
			this.UpdateLabelStyle(ref buttonStyle);
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x0014D432 File Offset: 0x0014B632
		private void OnValidate()
		{
			this.CacheComponents();
		}

		// Token: 0x04004D21 RID: 19745
		private static readonly List<ButtonSkinController> instancesList = new List<ButtonSkinController>();

		// Token: 0x04004D22 RID: 19746
		private Button button;

		// Token: 0x04004D23 RID: 19747
		public bool useRecommendedButtonWidth = true;

		// Token: 0x04004D24 RID: 19748
		public bool useRecommendedButtonHeight = true;

		// Token: 0x04004D25 RID: 19749
		public bool useRecommendedImage = true;

		// Token: 0x04004D26 RID: 19750
		public bool useRecommendedMaterial = true;

		// Token: 0x04004D27 RID: 19751
		public bool useRecommendedAlignment = true;

		// Token: 0x04004D28 RID: 19752
		public bool useRecommendedLabel = true;

		// Token: 0x04004D29 RID: 19753
		private LayoutElement layoutElement;
	}
}
