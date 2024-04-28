using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI.SkinControllers
{
	// Token: 0x02000DC5 RID: 3525
	[RequireComponent(typeof(ScrollRect))]
	public class ScrollRectSkinController : BaseSkinController
	{
		// Token: 0x060050A1 RID: 20641 RVA: 0x0014D59F File Offset: 0x0014B79F
		protected new void Awake()
		{
			this.scrollRect = base.GetComponent<ScrollRect>();
			base.Awake();
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x0014D5B4 File Offset: 0x0014B7B4
		protected override void OnSkinUI()
		{
			Image component = base.GetComponent<Image>();
			if (component)
			{
				this.skinData.scrollRectStyle.backgroundPanelStyle.Apply(component);
			}
			if (this.scrollRect.verticalScrollbar)
			{
				this.SkinScrollbar(this.scrollRect.verticalScrollbar);
			}
			if (this.scrollRect.horizontalScrollbar)
			{
				this.SkinScrollbar(this.scrollRect.horizontalScrollbar);
			}
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x0014D62C File Offset: 0x0014B82C
		private void SkinScrollbar(Scrollbar scrollbar)
		{
			this.skinData.scrollRectStyle.scrollbarBackgroundStyle.Apply(scrollbar.GetComponent<Image>());
			scrollbar.colors = this.skinData.scrollRectStyle.scrollbarHandleColors;
			scrollbar.handleRect.GetComponent<Image>().sprite = this.skinData.scrollRectStyle.scrollbarHandleImage;
		}

		// Token: 0x04004D37 RID: 19767
		private ScrollRect scrollRect;
	}
}
