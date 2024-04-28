using System;

namespace EntityStates.Captain.Weapon
{
	// Token: 0x0200041E RID: 1054
	public class CallAirstrikeEnter : BaseSkillState
	{
		// Token: 0x060012FD RID: 4861 RVA: 0x00054B8C File Offset: 0x00052D8C
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				switch (base.activatorSkillSlot.stock)
				{
				case 0:
					this.outer.SetNextState(new CallAirstrike3());
					return;
				case 1:
					this.outer.SetNextState(new CallAirstrike2());
					return;
				case 2:
					this.outer.SetNextState(new CallAirstrike1());
					return;
				default:
					this.outer.SetNextState(new CallAirstrike1());
					break;
				}
			}
		}
	}
}
