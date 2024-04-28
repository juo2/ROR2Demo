using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008D8 RID: 2264
	[RequireComponent(typeof(Canvas))]
	public class RefreshCanvasDrawOrder : MonoBehaviour
	{
		// Token: 0x060032BE RID: 12990 RVA: 0x000D63F0 File Offset: 0x000D45F0
		private void OnEnable()
		{
			if (this.canvas != null)
			{
				if (!this.cachedOriginalCanvasSortingOrder)
				{
					this.originalCanvasSortingOrder = this.canvas.sortingOrder;
					this.cachedOriginalCanvasSortingOrder = true;
				}
				this.canvas.overrideSorting = true;
				this.canvas.sortingOrder = -1000;
				this.canvas.sortingOrder = this.originalCanvasSortingOrder + this.canvasSortingOrderDelta;
			}
		}

		// Token: 0x040033E8 RID: 13288
		public int canvasSortingOrderDelta = 1;

		// Token: 0x040033E9 RID: 13289
		public Canvas canvas;

		// Token: 0x040033EA RID: 13290
		private int originalCanvasSortingOrder;

		// Token: 0x040033EB RID: 13291
		private bool cachedOriginalCanvasSortingOrder;
	}
}
