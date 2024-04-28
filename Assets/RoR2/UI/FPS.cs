using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CA9 RID: 3241
	public class FPS : MonoBehaviour
	{
		// Token: 0x060049E4 RID: 18916 RVA: 0x0012FA84 File Offset: 0x0012DC84
		private void Update()
		{
			this.stopwatch += Time.unscaledDeltaTime;
			this.deltaTime += (Time.unscaledDeltaTime - this.deltaTime) * 0.1f;
			if (this.stopwatch >= 1f)
			{
				this.stopwatch -= 1f;
				float num = this.deltaTime * 1000f;
				float num2 = 1f / this.deltaTime;
				string text = string.Format("{0:0.0} ms \n{1:0.} fps", num, num2);
				this.targetText.text = text;
			}
		}

		// Token: 0x040046B3 RID: 18099
		public TextMeshProUGUI targetText;

		// Token: 0x040046B4 RID: 18100
		private float deltaTime;

		// Token: 0x040046B5 RID: 18101
		private float stopwatch;

		// Token: 0x040046B6 RID: 18102
		private const float updateTime = 1f;
	}
}
