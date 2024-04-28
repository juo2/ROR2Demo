using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CFF RID: 3327
	public class FlashPanel : MonoBehaviour
	{
		// Token: 0x06004BC9 RID: 19401 RVA: 0x00137AE8 File Offset: 0x00135CE8
		private void Start()
		{
			this.image = this.flashRectTransform.GetComponent<Image>();
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x00137AFC File Offset: 0x00135CFC
		private void Update()
		{
			this.flashRectTransform.anchorMin = new Vector2(0f, 0f);
			this.flashRectTransform.anchorMax = new Vector2(1f, 1f);
			if (this.alwaysFlash)
			{
				this.isFlashing = true;
			}
			if (this.isFlashing)
			{
				this.theta += Time.deltaTime * this.freq;
			}
			if (this.theta > 1f)
			{
				if (this.alwaysFlash)
				{
					this.theta -= this.theta - this.theta % 1f;
				}
				else
				{
					this.theta = 1f;
				}
				this.isFlashing = false;
			}
			float num = 1f - (1f + Mathf.Cos(this.theta * 3.1415927f * 0.5f + 1.5707964f));
			this.flashRectTransform.sizeDelta = new Vector2(1f + num * 20f * this.strength, 1f + num * 20f * this.strength);
			if (this.image)
			{
				Color color = this.image.color;
				color.a = (1f - num) * this.strength * this.flashAlpha;
				this.image.color = color;
			}
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x00137C59 File Offset: 0x00135E59
		public void Flash()
		{
			this.theta = 0f;
			this.isFlashing = true;
		}

		// Token: 0x04004887 RID: 18567
		public RectTransform flashRectTransform;

		// Token: 0x04004888 RID: 18568
		public float strength = 1f;

		// Token: 0x04004889 RID: 18569
		public float freq = 1f;

		// Token: 0x0400488A RID: 18570
		public float flashAlpha = 0.7f;

		// Token: 0x0400488B RID: 18571
		public bool alwaysFlash = true;

		// Token: 0x0400488C RID: 18572
		private bool isFlashing;

		// Token: 0x0400488D RID: 18573
		private float theta = 1f;

		// Token: 0x0400488E RID: 18574
		private Image image;
	}
}
