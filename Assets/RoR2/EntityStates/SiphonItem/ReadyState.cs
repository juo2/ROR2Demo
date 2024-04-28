using System;
using RoR2;

namespace EntityStates.SiphonItem
{
	// Token: 0x020001CA RID: 458
	public class ReadyState : BaseSiphonItemState
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x00022C8C File Offset: 0x00020E8C
		public override void OnEnter()
		{
			base.OnEnter();
			CharacterBody attachedBody = base.attachedBody;
			this.attachedHealthComponent = ((attachedBody != null) ? attachedBody.healthComponent : null);
			base.TurnOffHealingFX();
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00022CB2 File Offset: 0x00020EB2
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && this.attachedHealthComponent.combinedHealthFraction <= ReadyState.healthFractionThreshold)
			{
				this.outer.SetNextState(new ChargeState());
			}
		}

		// Token: 0x040009A8 RID: 2472
		public static float healthFractionThreshold = 0.5f;

		// Token: 0x040009A9 RID: 2473
		private HealthComponent attachedHealthComponent;
	}
}
