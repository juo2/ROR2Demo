using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D08 RID: 3336
	public class HGPopoutPanel : MonoBehaviour
	{
		// Token: 0x06004C00 RID: 19456 RVA: 0x0013920C File Offset: 0x0013740C
		private void OnEnable()
		{
			for (int i = 0; i < HGPopoutPanel.instances.Count; i++)
			{
				HGPopoutPanel hgpopoutPanel = HGPopoutPanel.instances[i];
				if (this.popoutPanelLayer == hgpopoutPanel.popoutPanelLayer)
				{
					hgpopoutPanel.gameObject.SetActive(false);
				}
			}
			HGPopoutPanel.instances.Add(this);
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x0013925F File Offset: 0x0013745F
		private void OnDisable()
		{
			HGPopoutPanel.instances.Remove(this);
		}

		// Token: 0x040048D7 RID: 18647
		public int popoutPanelLayer;

		// Token: 0x040048D8 RID: 18648
		[Header("Optional Referenced Components")]
		public RectTransform popoutPanelContentContainer;

		// Token: 0x040048D9 RID: 18649
		public LanguageTextMeshController popoutPanelTitleText;

		// Token: 0x040048DA RID: 18650
		public LanguageTextMeshController popoutPanelSubtitleText;

		// Token: 0x040048DB RID: 18651
		public LanguageTextMeshController popoutPanelDescriptionText;

		// Token: 0x040048DC RID: 18652
		public static List<HGPopoutPanel> instances = new List<HGPopoutPanel>();
	}
}
