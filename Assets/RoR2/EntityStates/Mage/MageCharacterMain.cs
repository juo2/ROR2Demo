using System;
using RoR2;

namespace EntityStates.Mage
{
	// Token: 0x02000296 RID: 662
	public class MageCharacterMain : GenericCharacterMain
	{
		// Token: 0x06000BB2 RID: 2994 RVA: 0x00030A58 File Offset: 0x0002EC58
		public override void OnEnter()
		{
			base.OnEnter();
			this.jetpackStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Jet");
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00030A78 File Offset: 0x0002EC78
		public override void ProcessJump()
		{
			base.ProcessJump();
			if (this.hasCharacterMotor && this.hasInputBank && base.isAuthority)
			{
				object obj = base.inputBank.jump.down && base.characterMotor.velocity.y < 0f && !base.characterMotor.isGrounded;
				bool flag = this.jetpackStateMachine.state.GetType() == typeof(JetpackOn);
				object obj2 = obj;
				if (obj2 != null && !flag)
				{
					this.jetpackStateMachine.SetNextState(new JetpackOn());
				}
				if (obj2 == 0 && flag)
				{
					this.jetpackStateMachine.SetNextState(new Idle());
				}
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00030B33 File Offset: 0x0002ED33
		public override void OnExit()
		{
			if (base.isAuthority && this.jetpackStateMachine)
			{
				this.jetpackStateMachine.SetNextState(new Idle());
			}
			base.OnExit();
		}

		// Token: 0x04000DE2 RID: 3554
		private EntityStateMachine jetpackStateMachine;
	}
}
