using System;
using UnityEngine.Networking;

namespace EntityStates.QuestVolatileBattery
{
	// Token: 0x0200021F RID: 543
	public class Monitor : QuestVolatileBatteryBaseState
	{
		// Token: 0x0600098C RID: 2444 RVA: 0x0002736F File Offset: 0x0002556F
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				this.FixedUpdateServer();
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00027384 File Offset: 0x00025584
		private void FixedUpdateServer()
		{
			if (!base.attachedHealthComponent)
			{
				return;
			}
			float combinedHealthFraction = base.attachedHealthComponent.combinedHealthFraction;
			if (combinedHealthFraction <= Monitor.healthFractionDetonationThreshold && Monitor.healthFractionDetonationThreshold < this.previousHealthFraction)
			{
				this.outer.SetNextState(new CountDown());
			}
			this.previousHealthFraction = combinedHealthFraction;
		}

		// Token: 0x04000B13 RID: 2835
		private float previousHealthFraction;

		// Token: 0x04000B14 RID: 2836
		private static readonly float healthFractionDetonationThreshold = 0.5f;
	}
}
