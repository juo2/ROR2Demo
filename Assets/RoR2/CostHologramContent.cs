using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000686 RID: 1670
	public class CostHologramContent : MonoBehaviour
	{
		// Token: 0x0600209D RID: 8349 RVA: 0x0008C2C8 File Offset: 0x0008A4C8
		private void FixedUpdate()
		{
			if (this.targetTextMesh)
			{
				CostHologramContent.sharedStringBuilder.Clear();
				Color color = Color.white;
				CostTypeDef costTypeDef = CostTypeCatalog.GetCostTypeDef(this.costType);
				if (costTypeDef != null)
				{
					costTypeDef.BuildCostStringStyled(this.displayValue, CostHologramContent.sharedStringBuilder, true, false);
					color = costTypeDef.GetCostColor(true);
				}
				this.targetTextMesh.SetText(CostHologramContent.sharedStringBuilder);
				this.targetTextMesh.color = color;
			}
		}

		// Token: 0x040025DD RID: 9693
		public int displayValue;

		// Token: 0x040025DE RID: 9694
		public TextMeshPro targetTextMesh;

		// Token: 0x040025DF RID: 9695
		public CostTypeIndex costType;

		// Token: 0x040025E0 RID: 9696
		private static readonly StringBuilder sharedStringBuilder = new StringBuilder();
	}
}
