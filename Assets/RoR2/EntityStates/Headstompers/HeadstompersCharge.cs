using System;
using UnityEngine;

namespace EntityStates.Headstompers
{
	// Token: 0x0200032F RID: 815
	public class HeadstompersCharge : BaseHeadstompersState
	{
		// Token: 0x06000EA3 RID: 3747 RVA: 0x0003F1FA File Offset: 0x0003D3FA
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.FixedUpdateAuthority();
			}
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0003F210 File Offset: 0x0003D410
		private void FixedUpdateAuthority()
		{
			if (base.ReturnToIdleIfGroundedAuthority())
			{
				return;
			}
			this.inputStopwatch = (base.slamButtonDown ? (this.inputStopwatch + Time.deltaTime) : 0f);
			if (this.inputStopwatch >= HeadstompersCharge.maxChargeDuration)
			{
				this.outer.SetNextState(new HeadstompersFall());
				return;
			}
			if (!base.slamButtonDown)
			{
				this.outer.SetNextState(new HeadstompersIdle());
				return;
			}
			if (this.bodyMotor)
			{
				Vector3 velocity = this.bodyMotor.velocity;
				if (velocity.y < HeadstompersCharge.minVelocityY)
				{
					velocity.y = Mathf.MoveTowards(velocity.y, HeadstompersCharge.minVelocityY, HeadstompersCharge.accelerationY * Time.deltaTime);
					this.bodyMotor.velocity = velocity;
				}
			}
		}

		// Token: 0x04001257 RID: 4695
		private float inputStopwatch;

		// Token: 0x04001258 RID: 4696
		public static float maxChargeDuration = 0.5f;

		// Token: 0x04001259 RID: 4697
		public static float minVelocityY = 1f;

		// Token: 0x0400125A RID: 4698
		public static float accelerationY = 10f;
	}
}
