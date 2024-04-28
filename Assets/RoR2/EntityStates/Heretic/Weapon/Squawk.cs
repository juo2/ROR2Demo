using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Heretic.Weapon
{
	// Token: 0x0200032C RID: 812
	public class Squawk : EntityState
	{
		// Token: 0x06000E93 RID: 3731 RVA: 0x0003EF53 File Offset: 0x0003D153
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(this.soundName, base.gameObject);
			this.outer.SetNextStateToMain();
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x0400124C RID: 4684
		[SerializeField]
		public string soundName;
	}
}
