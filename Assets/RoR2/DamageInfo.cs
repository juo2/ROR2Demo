using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200057E RID: 1406
	public class DamageInfo
	{
		// Token: 0x0600193C RID: 6460 RVA: 0x0006CF09 File Offset: 0x0006B109
		public void ModifyDamageInfo(HurtBox.DamageModifier damageModifier)
		{
			switch (damageModifier)
			{
			case HurtBox.DamageModifier.Normal:
			case HurtBox.DamageModifier.SniperTarget:
				break;
			case HurtBox.DamageModifier.Weak:
				this.damageType |= DamageType.WeakPointHit;
				break;
			default:
				return;
			}
		}

		// Token: 0x04001F87 RID: 8071
		public float damage;

		// Token: 0x04001F88 RID: 8072
		public bool crit;

		// Token: 0x04001F89 RID: 8073
		public GameObject inflictor;

		// Token: 0x04001F8A RID: 8074
		public GameObject attacker;

		// Token: 0x04001F8B RID: 8075
		public Vector3 position;

		// Token: 0x04001F8C RID: 8076
		public Vector3 force;

		// Token: 0x04001F8D RID: 8077
		public bool rejected;

		// Token: 0x04001F8E RID: 8078
		public ProcChainMask procChainMask;

		// Token: 0x04001F8F RID: 8079
		public float procCoefficient = 1f;

		// Token: 0x04001F90 RID: 8080
		public DamageType damageType;

		// Token: 0x04001F91 RID: 8081
		public DamageColorIndex damageColorIndex;

		// Token: 0x04001F92 RID: 8082
		public DotController.DotIndex dotIndex = DotController.DotIndex.None;

		// Token: 0x04001F93 RID: 8083
		public bool canRejectForce = true;
	}
}
