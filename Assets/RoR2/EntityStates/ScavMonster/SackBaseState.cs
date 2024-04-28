using System;
using UnityEngine;

namespace EntityStates.ScavMonster
{
	// Token: 0x020001DE RID: 478
	public class SackBaseState : BaseState
	{
		// Token: 0x06000888 RID: 2184 RVA: 0x0002409B File Offset: 0x0002229B
		public override void OnEnter()
		{
			base.OnEnter();
			this.muzzleTransform = base.FindModelChild(SackBaseState.muzzleName);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0000F997 File Offset: 0x0000DB97
		public override void FixedUpdate()
		{
			base.FixedUpdate();
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000A02 RID: 2562
		public static string muzzleName;

		// Token: 0x04000A03 RID: 2563
		protected Transform muzzleTransform;
	}
}
