using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D1D RID: 3357
	public class ImageFillController : MonoBehaviour
	{
		// Token: 0x06004C7E RID: 19582 RVA: 0x0013BDC4 File Offset: 0x00139FC4
		public void OnEnable()
		{
			this.SetTValue(0f);
		}

		// Token: 0x06004C7F RID: 19583 RVA: 0x0013BDD4 File Offset: 0x00139FD4
		public void SetTValue(float t)
		{
			float fillAmount = this.fillScalar * t;
			foreach (Image image in this.images)
			{
				if (image)
				{
					image.fillAmount = fillAmount;
				}
			}
		}

		// Token: 0x04004983 RID: 18819
		[SerializeField]
		private Image[] images;

		// Token: 0x04004984 RID: 18820
		[SerializeField]
		private float fillScalar;
	}
}
