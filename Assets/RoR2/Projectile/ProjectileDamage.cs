using System;
using UnityEngine;

namespace RoR2.Projectile
{
	// Token: 0x02000B89 RID: 2953
	public class ProjectileDamage : MonoBehaviour
	{
		// Token: 0x06004342 RID: 17218 RVA: 0x00117012 File Offset: 0x00115212
		public void SetDamageTypeViaInt(int newDamageType)
		{
			this.damageType = (DamageType)newDamageType;
		}

		// Token: 0x0400415A RID: 16730
		[HideInInspector]
		public float damage;

		// Token: 0x0400415B RID: 16731
		[HideInInspector]
		public bool crit;

		// Token: 0x0400415C RID: 16732
		[HideInInspector]
		public float force;

		// Token: 0x0400415D RID: 16733
		[HideInInspector]
		public DamageColorIndex damageColorIndex;

		// Token: 0x0400415E RID: 16734
		public DamageType damageType;

		// Token: 0x0400415F RID: 16735
		[Tooltip("If true, we cap the maximum stacks for this attacker")]
		public bool useDotMaxStacksFromAttacker;

		// Token: 0x04004160 RID: 16736
		[Tooltip("The number of maximum stacks (if we're capping)")]
		public uint dotMaxStacksFromAttacker = uint.MaxValue;
	}
}
