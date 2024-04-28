using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2
{
	// Token: 0x020005C8 RID: 1480
	public class AnimateImageAlpha : MonoBehaviour
	{
		// Token: 0x06001AC6 RID: 6854 RVA: 0x00072EF9 File Offset: 0x000710F9
		private void OnEnable()
		{
			this.stopwatch = 0f;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00072EF9 File Offset: 0x000710F9
		public void ResetStopwatch()
		{
			this.stopwatch = 0f;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00072F08 File Offset: 0x00071108
		private void LateUpdate()
		{
			this.stopwatch += Time.unscaledDeltaTime;
			int num = 0;
			foreach (Image image in this.images)
			{
				num++;
				float a = this.alphaCurve.Evaluate((this.stopwatch + this.delayBetweenElements * (float)num) / this.timeMax);
				Color color = image.color;
				image.color = new Color(color.r, color.g, color.b, a);
			}
		}

		// Token: 0x040020ED RID: 8429
		public AnimationCurve alphaCurve;

		// Token: 0x040020EE RID: 8430
		public Image[] images;

		// Token: 0x040020EF RID: 8431
		public float timeMax = 5f;

		// Token: 0x040020F0 RID: 8432
		public float delayBetweenElements;

		// Token: 0x040020F1 RID: 8433
		[HideInInspector]
		public float stopwatch;
	}
}
