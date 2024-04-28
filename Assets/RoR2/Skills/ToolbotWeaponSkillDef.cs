using System;
using UnityEngine;

namespace RoR2.Skills
{
	// Token: 0x02000C1E RID: 3102
	public class ToolbotWeaponSkillDef : SkillDef
	{
		// Token: 0x04004413 RID: 17427
		public string stanceName;

		// Token: 0x04004414 RID: 17428
		public string entrySound;

		// Token: 0x04004415 RID: 17429
		public string entryAnimState;

		// Token: 0x04004416 RID: 17430
		public string enterGestureAnimState;

		// Token: 0x04004417 RID: 17431
		public string exitAnimState;

		// Token: 0x04004418 RID: 17432
		public string exitGestureAnimState;

		// Token: 0x04004419 RID: 17433
		public int animatorWeaponIndex;

		// Token: 0x0400441A RID: 17434
		public GameObject crosshairPrefab;

		// Token: 0x0400441B RID: 17435
		public AnimationCurve crosshairSpreadCurve;
	}
}
