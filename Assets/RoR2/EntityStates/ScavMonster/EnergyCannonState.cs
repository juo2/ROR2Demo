using System;
using UnityEngine;

namespace EntityStates.ScavMonster
{
	// Token: 0x020001D5 RID: 469
	public class EnergyCannonState : BaseState
	{
		// Token: 0x06000863 RID: 2147 RVA: 0x000237D6 File Offset: 0x000219D6
		public override void OnEnter()
		{
			base.OnEnter();
			this.muzzleTransform = base.FindModelChild(EnergyCannonState.muzzleName);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0000F997 File Offset: 0x0000DB97
		public override void FixedUpdate()
		{
			base.FixedUpdate();
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040009D4 RID: 2516
		public static string muzzleName;

		// Token: 0x040009D5 RID: 2517
		protected Transform muzzleTransform;
	}
}
