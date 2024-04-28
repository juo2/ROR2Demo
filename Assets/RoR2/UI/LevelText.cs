using System;
using System.Text;
using HG;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D2E RID: 3374
	[RequireComponent(typeof(RectTransform))]
	public class LevelText : MonoBehaviour
	{
		// Token: 0x06004CF6 RID: 19702 RVA: 0x0013DA10 File Offset: 0x0013BC10
		private void SetDisplayData(uint newDisplayData)
		{
			if (this.displayData == newDisplayData)
			{
				return;
			}
			this.displayData = newDisplayData;
			uint value = this.displayData;
			LevelText.sharedStringBuilder.Clear();
			LevelText.sharedStringBuilder.AppendUint(value, 1U, uint.MaxValue);
			this.targetText.SetText(LevelText.sharedStringBuilder);
		}

		// Token: 0x06004CF7 RID: 19703 RVA: 0x0013DA5E File Offset: 0x0013BC5E
		private void Update()
		{
			if (this.source)
			{
				this.SetDisplayData(HG.Convert.FloorToUIntClamped(this.source.level));
			}
		}

		// Token: 0x06004CF8 RID: 19704 RVA: 0x0013DA83 File Offset: 0x0013BC83
		private void OnValidate()
		{
			if (!this.targetText)
			{
				Debug.LogError("targetText must be assigned.");
			}
		}

		// Token: 0x040049FA RID: 18938
		public CharacterBody source;

		// Token: 0x040049FB RID: 18939
		public TextMeshProUGUI targetText;

		// Token: 0x040049FC RID: 18940
		private uint displayData;

		// Token: 0x040049FD RID: 18941
		private static readonly StringBuilder sharedStringBuilder = new StringBuilder();
	}
}
