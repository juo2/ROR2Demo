using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CEA RID: 3306
	[RequireComponent(typeof(Image))]
	internal class CurrentDifficultyIconController : MonoBehaviour
	{
		// Token: 0x06004B52 RID: 19282 RVA: 0x001356E4 File Offset: 0x001338E4
		private void Start()
		{
			if (Run.instance)
			{
				DifficultyDef difficultyDef = DifficultyCatalog.GetDifficultyDef(Run.instance.selectedDifficulty);
				base.GetComponent<Image>().sprite = difficultyDef.GetIconSprite();
			}
		}
	}
}
