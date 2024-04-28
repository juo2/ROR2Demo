using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A1D RID: 2589
	public class RuleCategoryDef
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x000F72A4 File Offset: 0x000F54A4
		public bool isHidden
		{
			get
			{
				Func<bool> func = this.hiddenTest;
				return func != null && func();
			}
		}

		// Token: 0x04003B46 RID: 15174
		public int position;

		// Token: 0x04003B47 RID: 15175
		public string displayToken;

		// Token: 0x04003B48 RID: 15176
		public string subtitleToken;

		// Token: 0x04003B49 RID: 15177
		public string emptyTipToken;

		// Token: 0x04003B4A RID: 15178
		public string editToken;

		// Token: 0x04003B4B RID: 15179
		public Color color;

		// Token: 0x04003B4C RID: 15180
		public List<RuleDef> children = new List<RuleDef>();

		// Token: 0x04003B4D RID: 15181
		public RuleCatalog.RuleCategoryType ruleCategoryType;

		// Token: 0x04003B4E RID: 15182
		public Func<bool> hiddenTest;
	}
}
