using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2
{
	// Token: 0x020008CD RID: 2253
	[RequireComponent(typeof(GridLayoutGroup))]
	[ExecuteAlways]
	public class AdjustGridLayoutCellSize : MonoBehaviour
	{
		// Token: 0x0600327B RID: 12923 RVA: 0x000D537B File Offset: 0x000D357B
		private void Update()
		{
			this.UpdateCellSize();
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x000D5384 File Offset: 0x000D3584
		private void UpdateCellSize()
		{
			if (!this.gridlayout)
			{
				return;
			}
			this.maxConstraintCount = this.gridlayout.constraintCount;
			this.layoutRect = this.gridlayout.gameObject.GetComponent<RectTransform>();
			float num = (float)(this.maxConstraintCount - 1) * this.gridlayout.spacing.x + (float)this.gridlayout.padding.left + (float)this.gridlayout.padding.right;
			float num2 = (float)(this.maxConstraintCount - 1) * this.gridlayout.spacing.y + (float)this.gridlayout.padding.top + (float)this.gridlayout.padding.bottom;
			float width = this.layoutRect.rect.width;
			float height = this.layoutRect.rect.height;
			float num3 = width - num;
			float num4 = height - num2;
			float x = num3 / (float)this.maxConstraintCount;
			float y = num4 / (float)this.maxConstraintCount;
			Vector2 cellSize = new Vector2(this.gridlayout.cellSize.x, this.gridlayout.cellSize.y);
			if (this.expandingSetting == AdjustGridLayoutCellSize.ExpandSetting.X || this.expandingSetting == AdjustGridLayoutCellSize.ExpandSetting.Both)
			{
				cellSize.x = x;
			}
			if (this.expandingSetting == AdjustGridLayoutCellSize.ExpandSetting.Y || this.expandingSetting == AdjustGridLayoutCellSize.ExpandSetting.Both)
			{
				cellSize.y = y;
			}
			this.gridlayout.cellSize = cellSize;
		}

		// Token: 0x040033A7 RID: 13223
		public AdjustGridLayoutCellSize.ExpandSetting expandingSetting;

		// Token: 0x040033A8 RID: 13224
		public GridLayoutGroup gridlayout;

		// Token: 0x040033A9 RID: 13225
		private int maxConstraintCount;

		// Token: 0x040033AA RID: 13226
		private RectTransform layoutRect;

		// Token: 0x020008CE RID: 2254
		public enum ExpandSetting
		{
			// Token: 0x040033AC RID: 13228
			X,
			// Token: 0x040033AD RID: 13229
			Y,
			// Token: 0x040033AE RID: 13230
			Both
		}
	}
}
