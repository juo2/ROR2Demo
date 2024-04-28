using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D20 RID: 3360
	public class InfiniteTowerWaveCounter : MonoBehaviour
	{
		// Token: 0x06004C88 RID: 19592 RVA: 0x0013C18E File Offset: 0x0013A38E
		private void OnEnable()
		{
			this.runInstance = (Run.instance as InfiniteTowerRun);
		}

		// Token: 0x06004C89 RID: 19593 RVA: 0x0013C1A0 File Offset: 0x0013A3A0
		private void Update()
		{
			if (this.runInstance && this.counterText)
			{
				this.counterText.text = Language.GetStringFormatted(this.token, new object[]
				{
					this.runInstance.waveIndex
				});
			}
		}

		// Token: 0x04004995 RID: 18837
		[Tooltip("The text we're setting")]
		[SerializeField]
		private TextMeshProUGUI counterText;

		// Token: 0x04004996 RID: 18838
		[Tooltip("The language token for the text field")]
		[SerializeField]
		private string token;

		// Token: 0x04004997 RID: 18839
		private InfiniteTowerRun runInstance;
	}
}
