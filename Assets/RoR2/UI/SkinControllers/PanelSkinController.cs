using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI.SkinControllers
{
	// Token: 0x02000DC3 RID: 3523
	[RequireComponent(typeof(Image))]
	public class PanelSkinController : BaseSkinController
	{
		// Token: 0x0600509E RID: 20638 RVA: 0x0014D518 File Offset: 0x0014B718
		protected new void Awake()
		{
			this.image = base.GetComponent<Image>();
			base.Awake();
		}

		// Token: 0x0600509F RID: 20639 RVA: 0x0014D52C File Offset: 0x0014B72C
		protected override void OnSkinUI()
		{
			switch (this.panelType)
			{
			case PanelSkinController.PanelType.Default:
				this.skinData.mainPanelStyle.Apply(this.image);
				return;
			case PanelSkinController.PanelType.Header:
				this.skinData.headerPanelStyle.Apply(this.image);
				return;
			case PanelSkinController.PanelType.Detail:
				this.skinData.detailPanelStyle.Apply(this.image);
				return;
			default:
				return;
			}
		}

		// Token: 0x04004D31 RID: 19761
		public PanelSkinController.PanelType panelType;

		// Token: 0x04004D32 RID: 19762
		private Image image;

		// Token: 0x02000DC4 RID: 3524
		public enum PanelType
		{
			// Token: 0x04004D34 RID: 19764
			Default,
			// Token: 0x04004D35 RID: 19765
			Header,
			// Token: 0x04004D36 RID: 19766
			Detail
		}
	}
}
