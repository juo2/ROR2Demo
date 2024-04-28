using System;
using System.Linq;
using RoR2;

namespace EntityStates.Toolbot
{
	// Token: 0x020001AE RID: 430
	public class StartToolbotStanceSwap : BaseState
	{
		// Token: 0x060007BD RID: 1981 RVA: 0x000211A8 File Offset: 0x0001F3A8
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				this.stanceStateMachine = base.gameObject.GetComponents<EntityStateMachine>().FirstOrDefault((EntityStateMachine c) => c.customName == "Stance");
				EntityStateMachine entityStateMachine = this.stanceStateMachine;
				ToolbotStanceBase toolbotStanceBase = ((entityStateMachine != null) ? entityStateMachine.state : null) as ToolbotStanceBase;
				if (toolbotStanceBase != null && toolbotStanceBase.swapStateType != null)
				{
					this.stanceStateMachine.SetNextState(new ToolbotStanceSwap
					{
						previousStanceState = toolbotStanceBase.GetType(),
						nextStanceState = toolbotStanceBase.swapStateType
					});
				}
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0002124C File Offset: 0x0001F44C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.outer.SetNextStateToMain();
		}

		// Token: 0x04000950 RID: 2384
		private EntityStateMachine stanceStateMachine;
	}
}
