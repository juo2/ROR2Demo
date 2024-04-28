using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D52 RID: 3410
	[RequireComponent(typeof(RectTransform))]
	public class MoneyText : MonoBehaviour
	{
		// Token: 0x06004E33 RID: 20019 RVA: 0x00142AA0 File Offset: 0x00140CA0
		public void Update()
		{
			this.coinSoundCooldown -= Time.deltaTime;
			if (this.targetText)
			{
				if (this.updateTimer <= 0f)
				{
					int num = 0;
					if (this.displayAmount != this.targetValue)
					{
						int num2;
						num = Math.DivRem(this.targetValue - this.displayAmount, 3, out num2);
						if (num2 != 0)
						{
							num += Math.Sign(num2);
						}
						if (num > 0)
						{
							if (this.coinSoundCooldown <= 0f)
							{
								this.coinSoundCooldown = 0.025f;
								Util.PlaySound(this.sound, RoR2Application.instance.gameObject);
							}
							if (this.flashPanel)
							{
								this.flashPanel.Flash();
							}
						}
						this.displayAmount += num;
					}
					float num3 = Mathf.Min(0.5f / (float)num, 0.25f);
					this.targetText.text = TextSerialization.ToStringNumeric(this.displayAmount);
					this.updateTimer = num3;
				}
				this.updateTimer -= Time.deltaTime;
			}
		}

		// Token: 0x04004ADA RID: 19162
		public TextMeshProUGUI targetText;

		// Token: 0x04004ADB RID: 19163
		public FlashPanel flashPanel;

		// Token: 0x04004ADC RID: 19164
		private int displayAmount;

		// Token: 0x04004ADD RID: 19165
		private float updateTimer;

		// Token: 0x04004ADE RID: 19166
		private float coinSoundCooldown;

		// Token: 0x04004ADF RID: 19167
		public int targetValue;

		// Token: 0x04004AE0 RID: 19168
		public string sound = "Play_UI_coin";
	}
}
