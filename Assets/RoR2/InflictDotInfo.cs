using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006B6 RID: 1718
	public struct InflictDotInfo
	{
		// Token: 0x040026D5 RID: 9941
		public GameObject victimObject;

		// Token: 0x040026D6 RID: 9942
		public GameObject attackerObject;

		// Token: 0x040026D7 RID: 9943
		public DotController.DotIndex dotIndex;

		// Token: 0x040026D8 RID: 9944
		public float duration;

		// Token: 0x040026D9 RID: 9945
		public float damageMultiplier;

		// Token: 0x040026DA RID: 9946
		public uint? maxStacksFromAttacker;

		// Token: 0x040026DB RID: 9947
		public float? totalDamage;

		// Token: 0x040026DC RID: 9948
		public DotController.DotIndex? preUpgradeDotIndex;
	}
}
