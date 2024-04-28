using System;
using UnityEngine;

namespace EntityStates.Jellyfish
{
	// Token: 0x020002ED RID: 749
	public class Dash : BaseState
	{
		// Token: 0x06000D63 RID: 3427 RVA: 0x00038764 File Offset: 0x00036964
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			if (base.rigidbodyMotor)
			{
				base.rigidbodyMotor.moveVector = base.rigidbodyMotor.rigid.transform.forward * base.characterBody.moveSpeed * Dash.speedCoefficient;
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000387CC File Offset: 0x000369CC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.rigidbodyMotor && this.modelAnimator)
			{
				this.modelAnimator.SetFloat("swim.rate", Vector3.Magnitude(base.rigidbodyMotor.rigid.velocity));
			}
			if (base.fixedAge >= Dash.duration)
			{
				this.outer.SetNextState(new SwimState());
			}
		}

		// Token: 0x04001064 RID: 4196
		public static float duration = 1.8f;

		// Token: 0x04001065 RID: 4197
		public static float speedCoefficient = 2f;

		// Token: 0x04001066 RID: 4198
		private Animator modelAnimator;
	}
}
