using System;
using TMPro;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008C9 RID: 2249
	public class TimerHologramContent : MonoBehaviour
	{
		// Token: 0x06003264 RID: 12900 RVA: 0x000D4A08 File Offset: 0x000D2C08
		private void FixedUpdate()
		{
			if (this.targetTextMesh)
			{
				int num = Mathf.FloorToInt(this.displayValue);
				int num2 = Mathf.FloorToInt((this.displayValue - (float)num) * 100f);
				this.targetTextMesh.text = string.Format("{0:D}.{1:D2}", num, num2);
			}
		}

		// Token: 0x0400337F RID: 13183
		public float displayValue;

		// Token: 0x04003380 RID: 13184
		public TextMeshPro targetTextMesh;
	}
}
