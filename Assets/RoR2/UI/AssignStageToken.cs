using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CBC RID: 3260
	public class AssignStageToken : MonoBehaviour
	{
		// Token: 0x06004A53 RID: 19027 RVA: 0x00131324 File Offset: 0x0012F524
		private void Start()
		{
			SceneDef mostRecentSceneDef = SceneCatalog.mostRecentSceneDef;
			this.titleText.SetText(Language.GetString(mostRecentSceneDef.nameToken), true);
			this.subtitleText.SetText(Language.GetString(mostRecentSceneDef.subtitleToken), true);
		}

		// Token: 0x04004709 RID: 18185
		public TextMeshProUGUI titleText;

		// Token: 0x0400470A RID: 18186
		public TextMeshProUGUI subtitleText;
	}
}
