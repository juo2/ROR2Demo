using System;
using UnityEngine;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000448 RID: 1096
	public class StaggerBaseState : BaseState
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public virtual EntityState nextState
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00057572 File Offset: 0x00055772
		public override void OnEnter()
		{
			base.OnEnter();
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0005757A File Offset: 0x0005577A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(this.nextState);
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}

		// Token: 0x0400190A RID: 6410
		[SerializeField]
		public float duration;
	}
}
