using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RoR2.UI
{
	// Token: 0x02000D7E RID: 3454
	[Obsolete("Use the options in HGButton instead.")]
	public class SelectableDescriptionUpdater : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x06004F32 RID: 20274 RVA: 0x00147D8C File Offset: 0x00145F8C
		public void OnPointerExit(PointerEventData eventData)
		{
			this.languageTextMeshController.token = "";
		}

		// Token: 0x06004F33 RID: 20275 RVA: 0x00147D8C File Offset: 0x00145F8C
		public void OnDeselect(BaseEventData eventData)
		{
			this.languageTextMeshController.token = "";
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x00147D9E File Offset: 0x00145F9E
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.languageTextMeshController.token = this.selectableDescriptionToken;
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x00147D9E File Offset: 0x00145F9E
		public void OnSelect(BaseEventData eventData)
		{
			this.languageTextMeshController.token = this.selectableDescriptionToken;
		}

		// Token: 0x04004BE8 RID: 19432
		public LanguageTextMeshController languageTextMeshController;

		// Token: 0x04004BE9 RID: 19433
		public string selectableDescriptionToken;
	}
}
