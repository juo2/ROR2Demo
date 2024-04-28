using System;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000C8 RID: 200
	public class HoverState : BaseState
	{
		// Token: 0x060003AF RID: 943 RVA: 0x0000F32D File Offset: 0x0000D52D
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			this.PlayAnimation("Body", "Idle");
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000F354 File Offset: 0x0000D554
		public override void Update()
		{
			base.Update();
			if (base.inputBank)
			{
				this.skill1InputReceived = base.inputBank.skill1.down;
				this.skill2InputReceived = base.inputBank.skill2.down;
				this.skill3InputReceived = base.inputBank.skill3.down;
				this.skill4InputReceived = base.inputBank.skill4.down;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000F3CC File Offset: 0x0000D5CC
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
							this.modelAnimator.SetFloat("fly.rate", Vector3.Magnitude(base.rigidbodyMotor.rigid.velocity));
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

		// Token: 0x040003BE RID: 958
		private Animator modelAnimator;

		// Token: 0x040003BF RID: 959
		private bool skill1InputReceived;

		// Token: 0x040003C0 RID: 960
		private bool skill2InputReceived;

		// Token: 0x040003C1 RID: 961
		private bool skill3InputReceived;

		// Token: 0x040003C2 RID: 962
		private bool skill4InputReceived;
	}
}
