using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D70 RID: 3440
	public class RectTransformDimensionsChangeEvent : MonoBehaviour
	{
		// Token: 0x1400010E RID: 270
		// (add) Token: 0x06004ED7 RID: 20183 RVA: 0x00145EC4 File Offset: 0x001440C4
		// (remove) Token: 0x06004ED8 RID: 20184 RVA: 0x00145EFC File Offset: 0x001440FC
		public event Action onRectTransformDimensionsChange;

		// Token: 0x06004ED9 RID: 20185 RVA: 0x00145F31 File Offset: 0x00144131
		private void OnRectTransformDimensionsChange()
		{
			if (this.onRectTransformDimensionsChange != null)
			{
				this.onRectTransformDimensionsChange();
			}
		}
	}
}
