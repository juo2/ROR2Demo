using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CC4 RID: 3268
	public class CarouselController : BaseSettingsControl
	{
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06004A7E RID: 19070 RVA: 0x00131AC7 File Offset: 0x0012FCC7
		// (set) Token: 0x06004A7F RID: 19071 RVA: 0x00131ACF File Offset: 0x0012FCCF
		[NotNull]
		public CarouselController.Choice[] choices
		{
			get
			{
				return this._choices;
			}
			set
			{
				this._choices = value;
				this.UpdateFromCurrentValue();
			}
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x00131ADE File Offset: 0x0012FCDE
		protected override void OnUpdateControls()
		{
			this.UpdateFromCurrentValue();
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x00131AE8 File Offset: 0x0012FCE8
		public void MoveCarousel(int direction)
		{
			this.selectionIndex = Mathf.Clamp(this.selectionIndex + direction, 0, this.choices.Length - 1);
			this.UpdateFromSelectionIndex();
			base.SubmitSetting(this.choices[this.selectionIndex].convarValue);
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x00131B35 File Offset: 0x0012FD35
		public void BoolCarousel()
		{
			this.selectionIndex = ((this.selectionIndex == 0) ? 1 : 0);
			this.UpdateFromSelectionIndex();
			base.SubmitSetting(this.choices[this.selectionIndex].convarValue);
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x00131B6C File Offset: 0x0012FD6C
		private void UpdateFromCurrentValue()
		{
			string currentValue = base.GetCurrentValue();
			bool flag = false;
			for (int i = 0; i < this.choices.Length; i++)
			{
				if (this.choices[i].convarValue == currentValue)
				{
					flag = true;
					this.selectionIndex = i;
					break;
				}
			}
			if (!flag && this.forceValidChoice)
			{
				this.selectionIndex = 0;
			}
			this.UpdateFromSelectionIndex();
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x00131BD4 File Offset: 0x0012FDD4
		private void UpdateFromSelectionIndex()
		{
			string text = "OPTION_CUSTOM";
			Sprite sprite = null;
			if (0 <= this.selectionIndex && this.selectionIndex < this.choices.Length)
			{
				CarouselController.Choice choice = this.choices[this.selectionIndex];
				text = choice.suboptionDisplayToken;
				sprite = choice.customSprite;
			}
			else if (this.choices.Length == 0)
			{
				text = string.Empty;
			}
			if (this.optionalText)
			{
				this.optionalText.GetComponent<LanguageTextMeshController>().token = (text ?? string.Empty);
			}
			if (this.optionalImage)
			{
				this.optionalImage.sprite = sprite;
			}
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x00131C78 File Offset: 0x0012FE78
		protected override void Update()
		{
			base.Update();
			bool active = true;
			bool active2 = true;
			if (this.selectionIndex == 0)
			{
				active = false;
			}
			if (this.selectionIndex == this.choices.Length - 1)
			{
				active2 = false;
			}
			if (this.leftArrowButton)
			{
				this.leftArrowButton.SetActive(active);
			}
			if (this.rightArrowButton)
			{
				this.rightArrowButton.SetActive(active2);
			}
		}

		// Token: 0x0400472F RID: 18223
		public GameObject leftArrowButton;

		// Token: 0x04004730 RID: 18224
		public GameObject rightArrowButton;

		// Token: 0x04004731 RID: 18225
		public Image optionalImage;

		// Token: 0x04004732 RID: 18226
		public TextMeshProUGUI optionalText;

		// Token: 0x04004733 RID: 18227
		[SerializeField]
		[NotNull]
		[FormerlySerializedAs("choices")]
		private CarouselController.Choice[] _choices = Array.Empty<CarouselController.Choice>();

		// Token: 0x04004734 RID: 18228
		public bool forceValidChoice;

		// Token: 0x04004735 RID: 18229
		private int selectionIndex;

		// Token: 0x02000CC5 RID: 3269
		[Serializable]
		public struct Choice
		{
			// Token: 0x04004736 RID: 18230
			public string suboptionDisplayToken;

			// Token: 0x04004737 RID: 18231
			public string convarValue;

			// Token: 0x04004738 RID: 18232
			public Sprite customSprite;
		}
	}
}
