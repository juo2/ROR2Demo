using System;
using UnityEngine;

namespace EntityStates.VagrantNovaItem
{
	// Token: 0x02000168 RID: 360
	public class RechargeState : BaseVagrantNovaItemState
	{
		// Token: 0x0600064A RID: 1610 RVA: 0x0001B144 File Offset: 0x00019344
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				int itemStack = base.GetItemStack();
				float num = RechargeState.baseDuration / (float)(itemStack + 1);
				float num2 = base.fixedAge / num;
				if (num2 >= 1f)
				{
					num2 = 1f;
					this.outer.SetNextState(new ReadyState());
				}
				base.SetChargeSparkEmissionRateMultiplier(RechargeState.particleEmissionCurve.Evaluate(num2));
			}
		}

		// Token: 0x040007AC RID: 1964
		public static float baseDuration = 30f;

		// Token: 0x040007AD RID: 1965
		public static AnimationCurve particleEmissionCurve;

		// Token: 0x040007AE RID: 1966
		private float rechargeDuration;
	}
}
