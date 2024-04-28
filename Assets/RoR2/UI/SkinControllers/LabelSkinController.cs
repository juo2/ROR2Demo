using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI.SkinControllers
{
	// Token: 0x02000DC1 RID: 3521
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class LabelSkinController : BaseSkinController
	{
		// Token: 0x0600509B RID: 20635 RVA: 0x0014D478 File Offset: 0x0014B678
		protected new void Awake()
		{
			this.label = base.GetComponent<TextMeshProUGUI>();
			base.Awake();
		}

		// Token: 0x0600509C RID: 20636 RVA: 0x0014D48C File Offset: 0x0014B68C
		protected override void OnSkinUI()
		{
			switch (this.labelType)
			{
			case LabelSkinController.LabelType.Default:
				this.skinData.bodyTextStyle.Apply(this.label, this.useRecommendedAlignment);
				return;
			case LabelSkinController.LabelType.Header:
				this.skinData.headerTextStyle.Apply(this.label, this.useRecommendedAlignment);
				return;
			case LabelSkinController.LabelType.Detail:
				this.skinData.detailTextStyle.Apply(this.label, this.useRecommendedAlignment);
				return;
			default:
				return;
			}
		}

		// Token: 0x04004D2A RID: 19754
		public LabelSkinController.LabelType labelType;

		// Token: 0x04004D2B RID: 19755
		public bool useRecommendedAlignment = true;

		// Token: 0x04004D2C RID: 19756
		private TextMeshProUGUI label;

		// Token: 0x02000DC2 RID: 3522
		public enum LabelType
		{
			// Token: 0x04004D2E RID: 19758
			Default,
			// Token: 0x04004D2F RID: 19759
			Header,
			// Token: 0x04004D30 RID: 19760
			Detail
		}
	}
}
