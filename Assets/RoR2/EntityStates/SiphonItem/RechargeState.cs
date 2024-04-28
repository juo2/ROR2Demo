using System;
using UnityEngine;

namespace EntityStates.SiphonItem
{
	// Token: 0x020001C9 RID: 457
	public class RechargeState : BaseSiphonItemState
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x00022C1C File Offset: 0x00020E1C
		public override void OnEnter()
		{
			base.OnEnter();
			base.TurnOffHealingFX();
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00022C2C File Offset: 0x00020E2C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				int itemStack = base.GetItemStack();
				float num = RechargeState.baseDuration / (float)(itemStack + 1);
				if (base.fixedAge / num >= 1f)
				{
					this.outer.SetNextState(new ReadyState());
				}
			}
		}

		// Token: 0x040009A5 RID: 2469
		public static float baseDuration = 30f;

		// Token: 0x040009A6 RID: 2470
		public static AnimationCurve particleEmissionCurve;

		// Token: 0x040009A7 RID: 2471
		private float rechargeDuration;
	}
}
