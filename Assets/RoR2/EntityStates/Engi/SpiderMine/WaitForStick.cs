using System;

namespace EntityStates.Engi.SpiderMine
{
	// Token: 0x0200038D RID: 909
	public class WaitForStick : BaseSpiderMineState
	{
		// Token: 0x06001050 RID: 4176 RVA: 0x00047BB5 File Offset: 0x00045DB5
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Base", "Idle");
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00047BCD File Offset: 0x00045DCD
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.projectileStickOnImpact.stuck)
			{
				this.outer.SetNextState(new Burrow());
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldStick
		{
			get
			{
				return true;
			}
		}
	}
}
