using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Headstompers
{
	// Token: 0x0200032E RID: 814
	public class HeadstompersIdle : BaseHeadstompersState
	{
		// Token: 0x06000E9F RID: 3743 RVA: 0x0003F0EF File Offset: 0x0003D2EF
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.FixedUpdateAuthority();
			}
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0003F108 File Offset: 0x0003D308
		private void FixedUpdateAuthority()
		{
			this.inputStopwatch = (base.slamButtonDown ? (this.inputStopwatch + Time.fixedDeltaTime) : 0f);
			if (base.isGrounded)
			{
				this.jumpBoostOk = true;
			}
			else if (this.jumpBoostOk && base.jumpButtonDown && this.bodyMotor)
			{
				Vector3 velocity = this.bodyMotor.velocity;
				if (velocity.y > 0f)
				{
					velocity.y *= 2f;
					this.bodyMotor.velocity = velocity;
					this.jumpBoostOk = false;
				}
				EffectManager.SimpleImpactEffect(HeadstompersIdle.jumpEffect, this.bodyGameObject.transform.position, Vector3.up, true);
			}
			if (this.inputStopwatch >= HeadstompersIdle.inputConfirmationDelay && !base.isGrounded)
			{
				this.outer.SetNextState(new HeadstompersCharge());
				return;
			}
		}

		// Token: 0x04001253 RID: 4691
		private float inputStopwatch;

		// Token: 0x04001254 RID: 4692
		public static float inputConfirmationDelay = 0.1f;

		// Token: 0x04001255 RID: 4693
		private bool jumpBoostOk;

		// Token: 0x04001256 RID: 4694
		public static GameObject jumpEffect;
	}
}
