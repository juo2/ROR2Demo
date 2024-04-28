using System;
using EntityStates.MajorConstruct.Stance;
using RoR2;

namespace EntityStates.MajorConstruct
{
	// Token: 0x0200028D RID: 653
	public class SwitchStance : BaseState
	{
		// Token: 0x06000B88 RID: 2952 RVA: 0x0003022C File Offset: 0x0002E42C
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				EntityStateMachine entityStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Stance");
				EntityState state = entityStateMachine.state;
				if (state != null)
				{
					if (!(state is Raised))
					{
						if (state is Lowered)
						{
							entityStateMachine.SetNextState(new LoweredToRaised());
						}
					}
					else
					{
						entityStateMachine.SetNextState(new RaisedToLowered());
					}
				}
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x04000DA4 RID: 3492
		private const string stanceStateMachineName = "Stance";
	}
}
