using System;

namespace EntityStates.Captain.Weapon
{
	// Token: 0x0200041F RID: 1055
	public class CallAirstrikeAltEnter : BaseSkillState
	{
		// Token: 0x060012FF RID: 4863 RVA: 0x00054C0A File Offset: 0x00052E0A
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				this.outer.SetNextState(new CallAirstrikeAlt());
			}
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}
	}
}
