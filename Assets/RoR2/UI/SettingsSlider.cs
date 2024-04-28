using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D83 RID: 3459
	public class SettingsSlider : BaseSettingsControl
	{
		// Token: 0x06004F44 RID: 20292 RVA: 0x00147F10 File Offset: 0x00146110
		protected new void Awake()
		{
			base.Awake();
			if (this.slider)
			{
				this.slider.minValue = this.minValue;
				this.slider.maxValue = this.maxValue;
				this.slider.onValueChanged.AddListener(new UnityAction<float>(this.OnSliderValueChanged));
			}
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x00147F6E File Offset: 0x0014616E
		private void OnSliderValueChanged(float newValue)
		{
			if (base.inUpdateControls)
			{
				return;
			}
			base.SubmitSetting(TextSerialization.ToStringInvariant(newValue));
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x00147F88 File Offset: 0x00146188
		private void OnInputFieldSubmit(string newString)
		{
			if (base.inUpdateControls)
			{
				return;
			}
			float value;
			if (TextSerialization.TryParseInvariant(newString, out value))
			{
				base.SubmitSetting(TextSerialization.ToStringInvariant(value));
			}
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x00147FB4 File Offset: 0x001461B4
		protected override void OnUpdateControls()
		{
			base.OnUpdateControls();
			float value;
			if (TextSerialization.TryParseInvariant(base.GetCurrentValue(), out value))
			{
				float num = Mathf.Clamp(value, this.minValue, this.maxValue);
				if (this.slider)
				{
					this.slider.value = num;
				}
				if (this.valueText)
				{
					this.valueText.text = string.Format(CultureInfo.InvariantCulture, this.formatString, num);
				}
			}
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x00148030 File Offset: 0x00146230
		public void MoveSlider(float delta)
		{
			if (this.slider)
			{
				this.slider.normalizedValue = this.slider.normalizedValue + delta;
			}
		}

		// Token: 0x04004BF2 RID: 19442
		public Slider slider;

		// Token: 0x04004BF3 RID: 19443
		public HGTextMeshProUGUI valueText;

		// Token: 0x04004BF4 RID: 19444
		public float minValue;

		// Token: 0x04004BF5 RID: 19445
		public float maxValue;

		// Token: 0x04004BF6 RID: 19446
		public string formatString = "{0:0.00}";
	}
}
