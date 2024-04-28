using System;

namespace EntityStates.Merc
{
	// Token: 0x02000281 RID: 641
	public class WhirlwindEntry : BaseState
	{
		// Token: 0x06000B51 RID: 2897 RVA: 0x0002F5B8 File Offset: 0x0002D7B8
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				EntityState nextState = (base.characterMotor && base.characterMotor.isGrounded) ? new WhirlwindGround() : new WhirlwindAir();
				this.outer.SetNextState(nextState);
				return;
			}
		}
	}
}
