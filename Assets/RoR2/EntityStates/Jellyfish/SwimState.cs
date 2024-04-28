using System;
using UnityEngine;

namespace EntityStates.Jellyfish
{
	// Token: 0x020002EE RID: 750
	public class SwimState : BaseState
	{
		// Token: 0x06000D67 RID: 3431 RVA: 0x00038851 File Offset: 0x00036A51
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00038868 File Offset: 0x00036A68
		public override void Update()
		{
			base.Update();
			if (base.inputBank)
			{
				this.skill1InputReceived = base.inputBank.skill1.down;
				this.skill2InputReceived |= base.inputBank.skill2.down;
				this.skill3InputReceived |= base.inputBank.skill3.down;
				this.skill4InputReceived |= base.inputBank.skill4.down;
				this.jumpInputReceived |= base.inputBank.jump.down;
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00038918 File Offset: 0x00036B18
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				if (base.inputBank)
				{
					if (base.rigidbodyMotor)
					{
						base.rigidbodyMotor.moveVector = base.inputBank.moveVector * base.characterBody.moveSpeed;
						if (this.modelAnimator)
						{
							this.modelAnimator.SetFloat("swim.rate", Vector3.Magnitude(base.rigidbodyMotor.rigid.velocity));
						}
					}
					if (base.rigidbodyDirection)
					{
						base.rigidbodyDirection.aimDirection = base.GetAimRay().direction;
					}
				}
				if (base.skillLocator)
				{
					if (base.skillLocator.primary && this.skill1InputReceived)
					{
						base.skillLocator.primary.ExecuteIfReady();
					}
					if (base.skillLocator.secondary && this.skill2InputReceived)
					{
						base.skillLocator.secondary.ExecuteIfReady();
					}
					if (base.skillLocator.utility && this.skill3InputReceived)
					{
						base.skillLocator.utility.ExecuteIfReady();
					}
					if (base.skillLocator.special && this.skill4InputReceived)
					{
						base.skillLocator.special.ExecuteIfReady();
					}
				}
			}
		}

		// Token: 0x04001067 RID: 4199
		private Animator modelAnimator;

		// Token: 0x04001068 RID: 4200
		private bool skill1InputReceived;

		// Token: 0x04001069 RID: 4201
		private bool skill2InputReceived;

		// Token: 0x0400106A RID: 4202
		private bool skill3InputReceived;

		// Token: 0x0400106B RID: 4203
		private bool skill4InputReceived;

		// Token: 0x0400106C RID: 4204
		private bool jumpInputReceived;
	}
}
