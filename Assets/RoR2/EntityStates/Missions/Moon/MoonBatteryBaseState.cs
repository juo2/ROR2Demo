using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Missions.Moon
{
	// Token: 0x02000240 RID: 576
	public abstract class MoonBatteryBaseState : BaseState
	{
		// Token: 0x06000A3C RID: 2620 RVA: 0x0002A7F9 File Offset: 0x000289F9
		public override void OnEnter()
		{
			base.OnEnter();
			this.purchaseInteraction = base.GetComponent<PurchaseInteraction>();
			this.animators = this.outer.GetComponentsInChildren<Animator>();
		}

		// Token: 0x04000BFC RID: 3068
		protected PurchaseInteraction purchaseInteraction;

		// Token: 0x04000BFD RID: 3069
		protected Animator[] animators;
	}
}
