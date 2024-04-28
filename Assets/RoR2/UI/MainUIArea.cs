using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D4B RID: 3403
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class MainUIArea : MonoBehaviour
	{
		// Token: 0x06004DF5 RID: 19957 RVA: 0x0014106C File Offset: 0x0013F26C
		private void Awake()
		{
			this.rectTransform = base.GetComponent<RectTransform>();
			this.parentRectTransform = this.rectTransform.parent.GetComponent<RectTransform>();
		}

		// Token: 0x06004DF6 RID: 19958 RVA: 0x00141090 File Offset: 0x0013F290
		private void Update()
		{
			Rect rect = this.parentRectTransform.rect;
			float num = rect.width * 0.05f;
			float num2 = rect.height * 0.05f;
			this.rectTransform.offsetMin = new Vector2(num, num2);
			this.rectTransform.offsetMax = new Vector2(-num, -num2);
		}

		// Token: 0x04004A97 RID: 19095
		private RectTransform rectTransform;

		// Token: 0x04004A98 RID: 19096
		private RectTransform parentRectTransform;
	}
}
