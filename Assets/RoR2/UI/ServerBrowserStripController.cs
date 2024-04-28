using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D80 RID: 3456
	[RequireComponent(typeof(RectTransform))]
	public class ServerBrowserStripController : MonoBehaviour
	{
		// Token: 0x06004F38 RID: 20280 RVA: 0x00147DB1 File Offset: 0x00145FB1
		private void Awake()
		{
			this.rectTransform = (RectTransform)base.transform;
		}

		// Token: 0x04004BEA RID: 19434
		private RectTransform rectTransform;

		// Token: 0x04004BEB RID: 19435
		public HGTextMeshProUGUI nameLabel;

		// Token: 0x04004BEC RID: 19436
		public HGTextMeshProUGUI addressLabel;

		// Token: 0x04004BED RID: 19437
		public HGTextMeshProUGUI playerCountLabel;

		// Token: 0x04004BEE RID: 19438
		public HGTextMeshProUGUI latencyLabel;
	}
}
