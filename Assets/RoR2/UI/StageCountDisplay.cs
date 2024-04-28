using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D90 RID: 3472
	public class StageCountDisplay : MonoBehaviour
	{
		// Token: 0x06004F81 RID: 20353 RVA: 0x00148F14 File Offset: 0x00147114
		private void Update()
		{
			string text = "-";
			if (Run.instance)
			{
				text = (Run.instance.stageClearCount + 1).ToString();
			}
			this.text.text = Language.GetStringFormatted("STAGE_COUNT_FORMAT", new object[]
			{
				text
			});
		}

		// Token: 0x04004C2E RID: 19502
		public TextMeshProUGUI text;
	}
}
